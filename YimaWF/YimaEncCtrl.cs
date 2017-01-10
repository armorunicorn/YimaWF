using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YimaEncCtrl;
using AxYIMAENCLib;
using YimaWF.data;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Resources;
using YimaWF.Properties;

namespace YimaWF
{
    public delegate void TargetSelectDelegate(Target t);
    public delegate void ShowTargetDetailDelegate(Target t);
    public delegate void TargetOptLinkageDelegate(Target t);
    public delegate void AddedForbiddenZoneDelegate(ForbiddenZone f);
    public delegate void AddedPipelineDelegate(Pipeline pl);
    public delegate void ShowRangingResultDelegate(int len);

    public partial class YimaEncCtrl: UserControl
    {
        private static int MaxForbiddenZoneNum = 5;
        private static int MaxProtectZoneNum = 5;
        private static int MaxPipelineZoneNum = 5;
        private static short MaxShowTargetTime = 30;

        public AxYimaEnc YimaEnc;

        private Rectangle ClientRect;

        public GeoPoint platformGeoPo = new GeoPoint(1075553840, 187558130);

        private int statusStripHeight = 22;

        public Dictionary<int, Target> AISTargetDic = new Dictionary<int, Target>();
        //public Dictionary<int, Target> RadarTargetDic = new Dictionary<int, Target>();
        public Dictionary<int, Target> MergeTargetDic = new Dictionary<int, Target>();

        public List<Target> RadarTargetList = new List<Target>();

        public List<ProtectZone> ProtectZoneList = new List<ProtectZone>();
        private Dictionary<int, ProtectZone> ProtectZoneMap = new Dictionary<int, ProtectZone>();

        public List<ForbiddenZone> ForbiddenZoneList = new List<ForbiddenZone>();
        //key为ID,用来确保ID只能为0到4
        private Dictionary<int, ForbiddenZone> ForbiddenZoneMap = new Dictionary<int, ForbiddenZone>(5);

        public List<Pipeline> PipelineList = new List<Pipeline>();
        //key为ID,用来确保ID只能为0到4
        private Dictionary<int, Pipeline> PipelineMap = new Dictionary<int, Pipeline>(5);

        public List<Alarm> AlarmList = new List<Alarm>();

        public Config AppConfig;

        public Target CurSelectedTarget;

        public Target CurShowingTrackTarget;

        public int TargetRectFactor = 10;


        //一些海图内部使用的全局变量
        private int TargetRectJudgeFactor = 15;
        private Image plantformImg;
        private Image largeTargetImg;
        private Image alarmImg;
        private ForbiddenZone curForbiddenZone;
        private Pipeline curPipeline;
        //目标图片切换的比例尺
        private int targetChangeScale = 2107260;

        //测距使用的内部全局变量
        private GeoPoint startingRangingPoint;
        private GeoPoint terminalRangingPoint;

        #region 回调
        public event TargetSelectDelegate TargetSelect;
        public event ShowTargetDetailDelegate ShowTargetDetail;
        public event TargetOptLinkageDelegate TargetOptLinkage;
        public event AddedForbiddenZoneDelegate AddedForbiddenZone;
        public event AddedPipelineDelegate AddedPipeline;
        public event ShowRangingResultDelegate ShowRangingResult;
        #endregion

        CURRENT_SUB_OPERATION m_curOperation = CURRENT_SUB_OPERATION.NO_OPERATION;

        List<GeoPoint> m_editingLineGeoPoints = new List<GeoPoint>();
        bool m_bHasPressedDragStartPo = false;
        Point m_mouseDragFirstPo;

        #region 雷达相关
        //双timer中用来存储海图绘制出来的图片（没有雷达和光电）
        private Bitmap backgroundBtm;
        private Radar radar1;
        private Radar radar2;

        private Color cScanColor = Color.FromArgb(0, 255, 0);
        private int startAngle = 0;
        //雷达圆在图上的半径（像素）
        private float radius;
        //雷达的物理半径（毫米）
        private int geoRadius;
        //天线点value的下限值
        private float min = 0;
        //天线点value的上限值
        private float max = 100;
        private bool isUpdate = true;
        #endregion

        #region 光电相关
        private Color oScanColor = Color.FromArgb(100, 227, 207, 87);
        //光电设备当前的角度
        private int optStartAngle = 50;
        private int optStep = 1;
        //光电设备扫描的
        private int optMaxAngle = 170;
        //海图直径
        private float optDiameter;
        //海图半径（像素）
        private float optRadius;
        //物理半径（毫米）
        private int optGeoRadius;
        //光电扫描扇形宽度
        private int optScanAngle = 10;
        #endregion

        #region 切换显示
        private bool showRadarTarget = true;
        private bool showMergeTarget = true;
        private bool showAISTarget = true;
        private bool showRadar = true;
        private bool showOpt = true;
        private bool showTrack = true;
        #endregion

        #region 数据回放
        public Dictionary<int, Target> AISTargetPlaybackDic = new Dictionary<int, Target>();
        public Dictionary<int, Target> RadarTargetPlaybackDic = new Dictionary<int, Target>();
        public Dictionary<int, Target> MergeTargetPlaybackDic = new Dictionary<int, Target>();
        #endregion

        public YimaEncCtrl()
        {
            InitializeComponent();
            InitConfig();
            
            YimaEnc = axYimaEnc;

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true);
            axYimaEnc.Init(System.IO.Directory.GetCurrentDirectory());
            axYimaEnc.SetIfShowMapFrame(true);
            axYimaEnc.SetIfYmcFileNeedEncrypt(false);
            axYimaEnc.SetYMCFileEncryptKey(4567);

            axYimaEnc.SetIfOnDrawRadarMode(false);
            axYimaEnc.SetIfUseGDIPlus(true);
            axYimaEnc.SetLoadMapScaleFactor(5);

            axYimaEnc.SetCurrentScale(123333);
            axYimaEnc.CenterMap(platformGeoPo.x, platformGeoPo.y);
            Init();
            //雷达范围默认20km
            geoRadius = 20000000;
            radius = axYimaEnc.GetScrnLenFromGeoLen(geoRadius);
            //Console.WriteLine(radius);
            optGeoRadius = 5000000;
            optRadius = axYimaEnc.GetScrnLenFromGeoLen(optGeoRadius);
            //初始化控件显示区大小
            ClientRect.X = 0;
            ClientRect.Y = 0;
            ClientRect.Width = axYimaEnc.Width;
            ClientRect.Height = axYimaEnc.Height;
            

            axYimaEnc.RefreshDrawer(axYimaEnc.Handle.ToInt32(), axYimaEnc.Width, axYimaEnc.Height, 0, 0);
            axYimaEnc.AfterDrawMap += AxYimaEnc_AfterDrawMap;
            RefreshScaleStatusBar();
            //axYimaEnc.DrawRadar += AxYimaEnc_DrawRadar;
            RadarTimer.Enabled = false;

            //加载平台图片
            plantformImg = Resources.PlantformImg;
            //加载目标大图标
            largeTargetImg = Resources.LargeTargetImg;
            //加载告警图标
            alarmImg = Resources.AlarmImg;

            //测试代码
        }

        private void AxYimaEnc_DrawRadar(object sender, EventArgs e)
        {
            //Console.WriteLine("1");
        }

        private void Init()
        {
            //雷达1初始化
            radar1 = new Radar();
            radar1.ID = 1;
            radar1.CurAngle = 0;
            radar1.GeoRadius = 20000000;
            radar1.ScanColor = Color.FromArgb(0, 255, 0);
            radar1.Radius = axYimaEnc.GetScrnLenFromGeoLen(radar1.GeoRadius);
            //雷达2初始化
            radar2 = new Radar();
            radar2.ID = 2;
            radar2.CurAngle = 180;
            radar2.GeoRadius = 20000000;
            radar2.ScanColor = Color.FromArgb(0, 255, 255);
            radar2.Radius = axYimaEnc.GetScrnLenFromGeoLen(radar2.GeoRadius);
        }

        private void InitConfig()
        {
            AppConfig = new Config();
            //添加默认配置
            AppConfig.TartgetColor.Add(TargetType.Unknow, Color.FromArgb(139, 37, 00));
            AppConfig.TartgetColor.Add(TargetType.Yacht, Color.FromArgb(139, 37, 00));
            AppConfig.TartgetColor.Add(TargetType.WorkBoat, Color.FromArgb(0, 100, 0));
            AppConfig.TartgetColor.Add(TargetType.FishingBoat, Color.FromArgb(0, 0, 170));
            AppConfig.TartgetColor.Add(TargetType.MerChantBoat, Color.FromArgb(205, 0, 205));
            AppConfig.TartgetColor.Add(TargetType.VietnamFishingBoat, Color.FromArgb(255, 0, 0));
            AppConfig.TargetStatusFont = new Font("宋体", 10);
            AppConfig.ProtectZonePen = Color.Red;
            AppConfig.TargetSelectPen = Pens.Red;
            AppConfig.ApproachRadarTarget = Color.Red;
            AppConfig.AloofRadarTarget = Color.Green;
        }


        #region 画图相关函数
        private void AxYimaEnc_AfterDrawMap(object sender, EventArgs e)
        {
            int dc = axYimaEnc.GetDrawerHDC();
            IntPtr dcPtr = new IntPtr(dc);
            var g = Graphics.FromHdc(dcPtr);
            do
            {
                try
                {
                    DrawPlatform(g);
                    ProtectZone lastZ = null;
                    foreach (var z in ProtectZoneList)
                    {
                        DrawProtectZone(g, z, lastZ);
                        lastZ = z;
                    }

                    foreach (var fz in ForbiddenZoneList)
                        DrawForbiddenZone(g, fz, true);

                    foreach (var p in PipelineList)
                        DrawPipeline(g, p);

                    //多边形保护区绘制模式
                    if(IsOnOperation(CURRENT_SUB_OPERATION.ADD_FORBIDDEN_ZONE))
                    {
                        //绘制当前正在添加的多边形禁止驶入区
                        DrawForbiddenZone(g, curForbiddenZone, false);
                        break;
                    }
                    //管道绘制模式
                    if(IsOnOperation(CURRENT_SUB_OPERATION.ADD_PIPELINE))
                    {
                        DrawPipeline(g, curPipeline);
                        break;
                    }
                    if(IsOnOperation(CURRENT_SUB_OPERATION.RANGING))
                    {
                        DrawRangingPoint(g, startingRangingPoint, terminalRangingPoint);
                        break;
                    }
                    //航迹跟踪模式，只显示一条船的航迹
                    if (IsOnOperation(CURRENT_SUB_OPERATION.SHOWING_TRACK))
                    {
                        DrawTargetTrack(g, CurShowingTrackTarget);
                        break;
                    }
                    //数据回放模式，现在只显示ais回放
                    if (IsOnOperation(CURRENT_SUB_OPERATION.PLAYBACK))
                    {
                        foreach (var t in AISTargetPlaybackDic.Values)
                        {
                            DrawTarget(g, t);
                        }
                        break;
                    }
                    //以上模式都不会绘制当前目标，只有当不在以上模式的时候，才开始绘制当前目标
                    Target selectedTarget = null;
                    if (showAISTarget)
                        foreach (var t in AISTargetDic.Values)
                        {
                            if(t.IsCheck == true)
                            {
                                selectedTarget = t;
                                continue;
                            }
                            DrawTarget(g, t);
                        }
                    if (showRadarTarget)
                    {
                        foreach (var t in RadarTargetList)
                        {
                            if (t.IsCheck == true)
                            {
                                selectedTarget = t;
                                continue;
                            }
                            DrawTarget(g, t);
                        }
                    }

                    if (showMergeTarget)
                        foreach (var t in MergeTargetDic.Values)
                        {
                            if (t.IsCheck == true)
                            {
                                selectedTarget = t;
                                continue;
                            }
                            DrawTarget(g, t);
                        }
                    if (selectedTarget != null)
                        DrawTarget(g, selectedTarget);
                    //自动设置雷达扫描的角度
                    radar1.CurAngle++;
                    if (radar1.CurAngle > 360)
                        radar1.CurAngle = 0;
                    radar2.CurAngle++;
                    if (radar2.CurAngle > 360)
                        radar2.CurAngle = 0;
                    /*
                     * 自动增加光电扫描扇形的角度，这里取消自动增加，以后都通过数据来控制当前角度
                    optRate++;
                    if (optRate == 3)
                    {
                        if (optStartAngle + optStep > optMaxAngle || optStartAngle + optStep < 0)
                        {
                            optStep = -optStep;
                        }
                        optStartAngle += optStep;
                        optRate = 0;
                    }
                    */

                    if (showRadar)
                    {
                        drawScan(g, radar1);
                        drawScan(g, radar2);
                    }
                    if (showOpt)
                        drawOptScan(g, optStartAngle);
                }
                catch
                {
                    Invalidate();
                }
            } while (false);
            g.Dispose();
            isUpdate = true;
        }

        private void DrawTarget(Graphics g, Target t)
        {
            if (t.Track.Count == 0)
                return;
            Color c = AppConfig.TartgetColor[t.Type];
            if (t.Source != TargetSource.AIS)
            {
                c = Color.FromArgb(100, c);

            }
            Brush brush = new SolidBrush(c);
            Pen pen = new Pen(brush, 1);

            //初始化需要画点
            var curGeoPoint = t.Track.Last();

            int curX = 0, curY = 0;
            axYimaEnc.GetScrnPoFromGeoPo(curGeoPoint.Point.x, curGeoPoint.Point.y, ref curX, ref curY);
            if (axYimaEnc.GetCurrentScale() < targetChangeScale)
            {
                //只有当比例尺较小时才绘制复杂信息（三角、标牌等）
                Point A = new Point(), B = new Point(), C = new Point(), D = new Point();
                if (t.Source == TargetSource.AIS)
                {
                    //三角形
                    //Point A = new Point(), B = new Point(), C = new Point(), D = new Point();
                    A.X = curX;
                    A.Y = curY - TargetRectFactor;
                    B.X = curX - TargetRectFactor;
                    B.Y = curY + TargetRectFactor;
                    C.X = curX + TargetRectFactor;
                    C.Y = curY + TargetRectFactor;
                    D.X = A.X;
                    D.Y = A.Y - Convert.ToInt32(t.Speed * 2);
                    //根据船的航向旋转三角形
                    Rotate(t.Course, ref A, ref B, ref C, ref D, new Point(curX, curY));
                    Point[] points = { A, B, C };
                    //画出船的图标
                    g.FillPolygon(brush, points);
                    g.DrawLine(pen, A, D);
                }
                else if (t.Source == TargetSource.Merge)
                {
                    //矩形
                    A.X = curX - TargetRectFactor;
                    A.Y = curY - TargetRectFactor;
                    B.X = curX + TargetRectFactor;
                    B.Y = curY - TargetRectFactor;
                    C.X = curX + TargetRectFactor;
                    C.Y = curY + TargetRectFactor;
                    D.X = curX - TargetRectFactor;
                    D.Y = curY + TargetRectFactor;
                    //根据船的航向旋转图形
                    Rotate(t.Course, ref A, ref B, ref C, ref D, new Point(curX, curY));
                    Point[] points = { A, B, C, D };
                    //画出船的图标
                    g.FillPolygon(brush, points);
                }
                else if (t.Source == TargetSource.Radar)
                {
                    //圆形
                    A.X = curX - TargetRectFactor / 2;
                    A.Y = curY - TargetRectFactor / 2;
                    B = A;
                    var rect = new Rectangle(A.X, A.Y,
                        TargetRectFactor, TargetRectFactor);
                    if (t.IsApproach)
                    {
                        brush = new SolidBrush(AppConfig.ApproachRadarTarget);
                    }
                    else
                    {
                        brush = new SolidBrush(AppConfig.AloofRadarTarget);
                    }
                    g.FillEllipse(brush, rect);
                }


                //画出船的状态
                string statusStr;
                Brush statusBrush;
                Rectangle statusRect;
                if (t.ShowSignTime == 0)
                {
                    //显示简略信息（船名、航向角、速度、到达时间）
                    statusStr = string.Format("{0}\n{1:F2}°\n{2:F2} m/s\n{3}", t.Name, t.Course, t.Speed, t.ArriveTime);
                    if (t.IsCheck)
                    {

                        statusBrush = Brushes.Green;
                    }
                    else
                    {
                        //statusStr = string.Format("{0}\n{1:F2}°\n{2:F2} kts", t.CallSign, 360 - t.Heading, t.Speed);
                        statusBrush = Brushes.Black;
                    }

                    if (t.Source == TargetSource.Merge)
                    {
                        statusRect = new Rectangle(B.X + TargetRectFactor, B.Y, 80, 60);
                    }
                    else
                    {
                        statusRect = new Rectangle(B.X - 80, A.Y - 8 - 10, 80, 60);
                    }
                    g.DrawString(statusStr, AppConfig.TargetStatusFont, statusBrush, statusRect);
                }
                else
                {
                    //显示基础信息（小标牌）——目标来源、船名、国籍、呼号、MIMSI、IMO、航向角、速度
                    statusRect = new Rectangle(B.X - 130 - 20, A.Y - 8 - 10, 140, 90);
                    statusStr = string.Format("{0} {1} {2} {3}\nMMSI:{4}\nIMO:{5}\n{6:F2}° {7:F2} m/s",
                        t.Source.ToString(), t.Name, t.Nationality, t.CallSign,
                        t.MIMSI,
                        t.IMO,
                        t.Course, t.Speed);
                    statusBrush = new SolidBrush(Color.FromArgb(255, Color.BurlyWood));
                    g.FillRectangle(statusBrush, statusRect);
                    g.DrawString(statusStr, AppConfig.TargetStatusFont, Brushes.Black, statusRect);
                }
                if (t.IsCheck)
                {
                    var factor = TargetRectFactor + 4;
                    A.X = curX - factor;
                    A.Y = curY - factor;
                    B.X = curX + factor;
                    B.Y = curY - factor;
                    C.X = curX + factor;
                    C.Y = curY + factor;
                    D.X = curX - factor;
                    D.Y = curY + factor;
                    //根据船的航向旋转图形
                    Rotate(t.Course, ref A, ref B, ref C, ref D, new Point(curX, curY));
                    //画出船的图标
                    g.DrawPolygon(AppConfig.TargetSelectPen, new Point[] { A, B, C, D });
                }
                //画出尾迹
                if (t.Track.Count > 1)
                {
                    if (t.ShowTrack)
                    {
                        List<Point> list = new List<Point>();
                        foreach (var p in t.Track)
                        {
                            axYimaEnc.GetScrnPoFromGeoPo(p.Point.x, p.Point.y, ref curX, ref curY);
                            //Console.WriteLine("{0}, {1}", curX, curY);
                            list.Add(new Point(curX, curY));
                        }
                        //Console.WriteLine("------------------------");
                        pen.Width = 3;
                        pen.DashStyle = DashStyle.Dot;
                        g.DrawCurve(pen, list.ToArray());
                    }
                    else
                    {
                        pen.Width = 3;
                        pen.DashStyle = DashStyle.Dot;
                        var currentGeo = t.Track.Last().Point;
                        axYimaEnc.GetScrnPoFromGeoPo(currentGeo.x, currentGeo.y, ref curX, ref curY);
                        C.X = curX; C.Y = curY;
                        currentGeo = t.Track[t.Track.Count - 2].Point;
                        axYimaEnc.GetScrnPoFromGeoPo(currentGeo.x, currentGeo.y, ref curX, ref curY);
                        D.X = curX; D.Y = curY;
                        g.DrawLine(pen, C, D);
                    }
                }
            }
            else
            {
                //大比例尺时则只绘制目标大图标（图片）
                var rect = new Rectangle(curX - 16, curY - 16, 32, 32);
                g.DrawImage(largeTargetImg, rect);
            }
        }

        private void DrawProtectZone(Graphics g, ProtectZone z, ProtectZone lastZ)
        {
            /*var pen = new Pen(z.ContentColor);
            pen.DashStyle = DashStyle.Custom;
            pen.DashPattern = new float[] { 5, 5 };

            int curX = 0, curY = 0;
            //axYimaEnc.GetScrnPoFromGeoPo(z.Center.x, z.Center.y, ref curX, ref curY);
            axYimaEnc.GetScrnPoFromGeoPo(platformGeoPo.x, platformGeoPo.y, ref curX, ref curY);
            float len = axYimaEnc.GetScrnLenFromGeoLen(z.Radius * 1000);
            g.DrawEllipse(pen, curX - len, curY - len, len * 2, len * 2);*/

            int curX = 0, curY = 0;
            var brush = new SolidBrush(z.ContentColor);
            float len = 0;


            axYimaEnc.GetScrnPoFromGeoPo(platformGeoPo.x, platformGeoPo.y, ref curX, ref curY);
            len = axYimaEnc.GetScrnLenFromGeoLen(z.Radius * 1000);
            if (lastZ == null)
            {
                g.FillEllipse(brush, curX - len, curY - len, len * 2, len * 2);
                
            }
            else
            {
                //Console.WriteLine(lastZ.Radius);
                GraphicsPath gp = new GraphicsPath(FillMode.Alternate);
                gp.AddEllipse(curX - len, curY - len, len * 2, len * 2);
                len = axYimaEnc.GetScrnLenFromGeoLen(lastZ.Radius * 1000);
                gp.AddEllipse(curX - len, curY - len, len * 2, len * 2);
                g.FillPath(brush, gp);
            }
        }

        private void DrawPlatform(Graphics g)
        {
            if(platformGeoPo != null)
            {
                int curX = 0, curY = 0;
                axYimaEnc.GetScrnPoFromGeoPo(platformGeoPo.x, platformGeoPo.y, ref curX, ref curY);
                var rect = new Rectangle(curX - 16, curY - 16, 32, 32);
                g.DrawImage(plantformImg, rect);
            }
        }

        private void DrawForbiddenZone(Graphics g, ForbiddenZone fz, bool drawPolygon)
        {
            var pen = new Pen(AppConfig.ProtectZonePen);
            pen.DashStyle = DashStyle.Custom;
            pen.DashPattern = new float[] { 5, 5 };

            int curX = 0, curY = 0;
            List<Point> list = new List<Point>();
            foreach (var p in fz.PointList)
            {
                axYimaEnc.GetScrnPoFromGeoPo(p.x, p.y, ref curX, ref curY);
                list.Add(new Point(curX, curY));
                Rectangle rect = new Rectangle(curX - 4, curY - 4, 8, 8);
                g.FillEllipse(new SolidBrush(pen.Color), rect);
            }
            if (drawPolygon)
                g.DrawPolygon(pen, list.ToArray());
            else
                g.DrawLines(pen, list.ToArray());
        }

        private void DrawPipeline(Graphics g, Pipeline p)
        {
            var pen = new Pen(Color.Black);
            if (axYimaEnc.GetCurrentScale() > 1000000)
            {
                pen.Width = 2;
            }
            else
            {
                pen.Width = 5;
            }

            int curX = 0, curY = 0;
            List<Point> list = new List<Point>();
            foreach (var a in p.PointList)
            {
                axYimaEnc.GetScrnPoFromGeoPo(a.x, a.y, ref curX, ref curY);
                list.Add(new Point(curX, curY));
                Rectangle rect = new Rectangle(curX - 4, curY - 4, 8, 8);
                g.FillEllipse(new SolidBrush(pen.Color), rect);
            }
            g.DrawLines(pen, list.ToArray());
        }

        private void DrawAlarm(Graphics g, Alarm a)
        {
            if (a.Location != null)
            {
                int curX = 0, curY = 0;
                axYimaEnc.GetScrnPoFromGeoPo(a.Location.x, a.Location.y, ref curX, ref curY);
                var rect = new Rectangle(curX - 16, curY - 16, 32, 32);
                g.DrawImage(alarmImg, rect);
            }
        }

        private void DrawRangingPoint(Graphics g, GeoPoint startingPoint, GeoPoint terminalPoint)
        {
            var pen = new Pen(Color.Red);
            var brush = new SolidBrush(pen.Color);
            int startingX = 0, startingY = 0;
            int terminalX = 0, terminalY = 0;
            if (startingPoint == null)
                return;
            axYimaEnc.GetScrnPoFromGeoPo(startingPoint.x, startingPoint.y, ref startingX, ref startingY);
            Rectangle rect = new Rectangle(startingX - 6, startingY - 6, 12, 12);
            g.FillEllipse(brush, rect);
            if (terminalPoint == null)
                return;
            axYimaEnc.GetScrnPoFromGeoPo(terminalPoint.x, terminalPoint.y, ref terminalX, ref terminalY);
            rect = new Rectangle(terminalX - 6, terminalY - 6, 12, 12);
            g.FillEllipse(brush, rect);
            g.DrawLine(pen, startingX, startingY, terminalX, terminalY);
        }

        private void Rotate(float heading, ref Point A, ref Point B, ref Point C, ref Point D, Point O)
        {
            double angle = (double)(heading  * Math.PI / 180);
            double newX = (A.X - O.X) * Math.Cos(angle) + (A.Y - O.Y) * Math.Sin(angle) + O.X;
            double newY = -(A.X - O.X) * Math.Sin(angle) + (A.Y - O.Y) * Math.Cos(angle) + O.Y;

            A.X = Convert.ToInt32(newX);
            A.Y = Convert.ToInt32(newY);

            newX = (B.X - O.X) * Math.Cos(angle) + (B.Y - O.Y) * Math.Sin(angle) + O.X;
            newY = -(B.X - O.X) * Math.Sin(angle) + (B.Y - O.Y) * Math.Cos(angle) + O.Y;

            B.X = Convert.ToInt32(newX);
            B.Y = Convert.ToInt32(newY);

            newX = (C.X - O.X) * Math.Cos(angle) + (C.Y - O.Y) * Math.Sin(angle) + O.X;
            newY = -(C.X - O.X) * Math.Sin(angle) + (C.Y - O.Y) * Math.Cos(angle) + O.Y;

            C.X = Convert.ToInt32(newX);
            C.Y = Convert.ToInt32(newY);

            newX = (D.X - O.X) * Math.Cos(angle) + (D.Y - O.Y) * Math.Sin(angle) + O.X;
            newY = -(D.X - O.X) * Math.Sin(angle) + (D.Y - O.Y) * Math.Cos(angle) + O.Y;

            D.X = Convert.ToInt32(newX);
            D.Y = Convert.ToInt32(newY);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var dc = e.Graphics.GetHdc();
            var c = dc.ToInt32();
            var a = axYimaEnc.DrawMapsInScreen(c);
            e.Graphics.ReleaseHdc(dc);
        }
        #endregion


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            axYimaEnc.Height = ClientRect.Height = ClientSize.Height - statusStripHeight;
            axYimaEnc.Width = ClientRect.Width = ClientSize.Width;

            axYimaEnc.RefreshDrawer(axYimaEnc.Handle.ToInt32(), axYimaEnc.Width, axYimaEnc.Height, 0, 0);
            Invalidate();
        }

        private void RefreshScaleStatusBar()
        {
            CurScale.Text = string.Format("1:{0}", axYimaEnc.GetCurrentScale());
        }

        #region 鼠标事件
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            

            //if (IsOnOperation(CURRENT_SUB_OPERATION.NO_OPERATION) || IsOnOperation(CURRENT_SUB_OPERATION.SHOWING_TRACK)
            //    || IsOnOperation(CURRENT_SUB_OPERATION.PLAYBACK) || IsOnOperation(CURRENT_SUB_OPERATION.RANGING))
           // {
                if (e.Delta > 0)
                {
                    axYimaEnc.SetCurrentScale(axYimaEnc.GetCurrentScale() / (float)1.5);
                }
                else
                {
                    axYimaEnc.SetCurrentScale(axYimaEnc.GetCurrentScale() * (float)1.5);
                }
                RefreshScaleStatusBar();
                //radius = axYimaEnc.GetScrnLenFromGeoLen(geoRadius);
                //更新雷达半径的长度
                radar1.Radius = axYimaEnc.GetScrnLenFromGeoLen(radar1.GeoRadius);
                radar2.Radius = axYimaEnc.GetScrnLenFromGeoLen(radar2.GeoRadius);
                optRadius = axYimaEnc.GetScrnLenFromGeoLen(optGeoRadius);
                Invalidate();
         //   }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            int geoPoX = 0, geoPoY = 0;
            if (e.Button == MouseButtons.Left)
            {
                if (IsOnOperation(CURRENT_SUB_OPERATION.NO_OPERATION) || IsOnOperation(CURRENT_SUB_OPERATION.SHOWING_TRACK)
                    || IsOnOperation(CURRENT_SUB_OPERATION.PLAYBACK))
                {
                    //SetOperation(CURRENT_SUB_OPERATION.HAND_ROAM);
                    //m_bHasPressedDragStartPo = true;
                    //m_mouseDragFirstPo = new Point(e.Location.X, e.Location.Y);
                }
                else if(IsOnOperation(CURRENT_SUB_OPERATION.ADD_FORBIDDEN_ZONE))
                {
                    axYimaEnc.GetGeoPoFromScrnPo(e.Location.X, e.Location.Y, ref geoPoX, ref geoPoY);
                    curForbiddenZone.PointList.Add(new GeoPoint(geoPoX, geoPoY));
                    Invalidate();
                }
                else if(IsOnOperation(CURRENT_SUB_OPERATION.ADD_PIPELINE))
                {
                    axYimaEnc.GetGeoPoFromScrnPo(e.Location.X, e.Location.Y, ref geoPoX, ref geoPoY);
                    curPipeline.PointList.Add(new GeoPoint(geoPoX, geoPoY));
                    Invalidate();
                }
                else if(IsOnOperation(CURRENT_SUB_OPERATION.RANGING))
                {
                    axYimaEnc.GetGeoPoFromScrnPo(e.Location.X, e.Location.Y, ref geoPoX, ref geoPoY);
                    if (startingRangingPoint == null)
                    {
                        startingRangingPoint = new GeoPoint(geoPoX, geoPoY);
                        Invalidate();
                    }
                    else if (terminalRangingPoint == null)
                    {
                        terminalRangingPoint = new GeoPoint(geoPoX, geoPoY);
                        Invalidate();
                        int startingX = 0, startingY = 0;
                        int terminalX = 0, terminalY = 0;
                        axYimaEnc.GetScrnPoFromGeoPo(startingRangingPoint.x, startingRangingPoint.y, ref startingX, ref startingY);
                        axYimaEnc.GetScrnPoFromGeoPo(terminalRangingPoint.x, terminalRangingPoint.y, ref terminalX, ref terminalY);
                        int x = Math.Abs(terminalX - startingX);
                        int y = Math.Abs(terminalY - startingY);
                        int scanLen = Convert.ToInt32(Math.Sqrt(x * x + y * y));
                        int geoLen = Convert.ToInt32(axYimaEnc.GetGeoLenFromScrnLen(scanLen));
                        ShowRangingResult?.Invoke(geoLen);
                    }
                }
            }
        }

        IntPtr DynamicDC = IntPtr.Zero;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            //更新状态栏信息
            CurClientPoint.Text = String.Format("X:{0} Y:{1}", e.X, e.Y);
            int geoX = 0, geoY = 0;
            axYimaEnc.GetGeoPoFromScrnPo(e.X, e.Y, ref geoX, ref geoY);
            CurGeoPoint.Text = String.Format("X:{0} Y:{1}", geoX, geoY);
            
            


            if (IsOnOperation(CURRENT_SUB_OPERATION.HAND_ROAM))
            {
                if (m_bHasPressedDragStartPo)
                {
                    var g = GetGraphics();
                    var dc = g.GetHdc();
                    axYimaEnc.DrawDragingMap(dc.ToInt32(), e.Location.X, e.Location.Y,
                        m_mouseDragFirstPo.X, m_mouseDragFirstPo.Y);
                    g.ReleaseHdc(dc);
                    g.Dispose();
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            bool isInvalidate = false;

            base.OnMouseUp(e);
            var t = GetClickTarget(e.Location);
            //鼠标左键
            if (e.Button == MouseButtons.Left)
            {
                //先将当前目标重置
                if (CurSelectedTarget != null)
                {
                    CurSelectedTarget.IsCheck = false;
                    CurSelectedTarget.ShowSignTime = 0;
                    CurSelectedTarget = null;
                    isInvalidate = true;
                }
                if (t != null)
                {
                    //替换当前选中的目标
                    CurSelectedTarget = t;
                    t.IsCheck = true;
                    t.ShowSignTime = MaxShowTargetTime;
                    isInvalidate = true;
                }
                if (IsOnOperation(CURRENT_SUB_OPERATION.HAND_ROAM))
                {
                    if (m_bHasPressedDragStartPo)
                    {
                        axYimaEnc.SetMapMoreOffset(e.Location.X - m_mouseDragFirstPo.X,
                            e.Location.Y - m_mouseDragFirstPo.Y);
                        m_bHasPressedDragStartPo = false;
                        isInvalidate = true;
                    }
                    //SetOperation(CURRENT_SUB_OPERATION.NO_OPERATION);
                    ClearOperation(CURRENT_SUB_OPERATION.HAND_ROAM);
                }
            }
            //鼠标右键
            else if(e.Button == MouseButtons.Right)
            {
                if (IsOnOperation(CURRENT_SUB_OPERATION.ADD_FORBIDDEN_ZONE) || IsOnOperation(CURRENT_SUB_OPERATION.ADD_PIPELINE))
                {
                    addContextMenu.Show(axYimaEnc, e.Location);
                }
                else
                {
                    if (t != null)
                    {
                        if (CurSelectedTarget != null)
                        {
                            CurSelectedTarget.IsCheck = false;
                            CurSelectedTarget.ShowSignTime = 0;
                        }
                        CurSelectedTarget = t;
                        t.IsCheck = true;
                        isInvalidate = true;
                        ShowTrackMenuItem.Checked = t.ShowTrack;
                        targetContextMenu.Show(axYimaEnc, e.Location);

                    }
                    else
                    {
                        normalContextMenu.Show(axYimaEnc, e.Location);
                    }
                }
            }

            if (isInvalidate)
                Invalidate();

        }
        #endregion

        private Rectangle GetRect(Point point, GeoPoint old_point)
        {
            var r = Convert.ToInt32(Math.Sqrt(Math.Pow((old_point.x - point.X), 2) + Math.Pow((old_point.y - point.Y), 2)));
            return new Rectangle(old_point.x - r, old_point.y - r, r * 2, r * 2);
        }

        private Target GetClickTarget(Point p)
        {
            int x = 0, y = 0;
            List<List<Target>> values = new List<List<Target>>(3);
            if (showAISTarget)
                values.Add(AISTargetDic.Values.ToList());
            if (showRadarTarget)
                values.Add(RadarTargetList);
            if (showMergeTarget)
                values.Add(MergeTargetDic.Values.ToList());
            foreach (var d in values)
                foreach(var t in d)
                {
                    if (t.Track.Count == 0)
                        continue;
                    var geoPoint = t.Track.Last();
                    axYimaEnc.GetScrnPoFromGeoPo(geoPoint.Point.x, geoPoint.Point.y, ref x, ref y);
                    if (p.X > x - TargetRectFactor && p.X < x + TargetRectFactor)
                        if (p.Y > y - TargetRectFactor && p.Y < y + TargetRectFactor)
                            return t;
                }
            return null;

        }

        private Graphics GetGraphics()
        {
            return Graphics.FromHwnd(Handle);
        }


        private bool IsOnOperation(CURRENT_SUB_OPERATION subOperation)
        {
            if (subOperation == CURRENT_SUB_OPERATION.NO_OPERATION)
            {
                return m_curOperation == CURRENT_SUB_OPERATION.NO_OPERATION;
            }
            else
            {
                return (m_curOperation & subOperation) != 0;
            }
        }

        private void SetOperation(CURRENT_SUB_OPERATION subOperation)
        {
            if (subOperation == CURRENT_SUB_OPERATION.NO_OPERATION)
            {
                m_curOperation = CURRENT_SUB_OPERATION.NO_OPERATION;
            }
            else
            {
                m_curOperation |= subOperation;
            }
        }

        private void ClearOperation(CURRENT_SUB_OPERATION subOperation)
        {
            m_curOperation &= ~subOperation;
        }

        private void TargetDataTimer_Tick(object sender, EventArgs e)
        {
            if (CurSelectedTarget != null)
                if(CurSelectedTarget.ShowSignTime > 0)
                    CurSelectedTarget.ShowSignTime--;
            if(IsOnOperation(CURRENT_SUB_OPERATION.NO_OPERATION) || IsOnOperation(CURRENT_SUB_OPERATION.SHOWING_TRACK)
                || IsOnOperation(CURRENT_SUB_OPERATION.PLAYBACK))
                Invalidate();
            //TargetDataTimer.Enabled = false;
        }

        private void ShowDetail_Click(object sender, EventArgs e)
        {
            if(CurSelectedTarget != null)
                ShowTargetDetail?.Invoke(CurSelectedTarget);
        }
        #region 雷达绘图函数
        private int optRate = 0;
        private void RadarTimer_Tick(object sender, EventArgs e)
        {
            if (!IsOnOperation(CURRENT_SUB_OPERATION.NO_OPERATION))
                return;
            startAngle++;
            if (startAngle > 360)
                startAngle = 0;
            optRate++;
            if (optRate == 3)
            {
                if (optStartAngle + optStep > optMaxAngle || optStartAngle + optStep < 0)
                {
                    optStep = -optStep;
                }
                optStartAngle += optStep;
                optRate = 0;
            }

            var g = GetGraphics();
            if(isUpdate)
            {
                if (backgroundBtm != null)
                   backgroundBtm.Dispose();
                Bitmap b = new Bitmap(ClientRect.Width, ClientRect.Height);
                var g3 = Graphics.FromImage(b);
                g3.CopyFromScreen(PointToScreen(new Point(0, 0)), new Point(0, 0), ClientRect.Size);
                g3.Dispose();
                backgroundBtm = b;
                isUpdate = false;
            }

            //var g2 = Graphics.FromImage(b);
            var b2 = new Bitmap(ClientRect.Width, ClientRect.Height);
            var g4 = Graphics.FromImage(b2);
            g4.DrawImage(backgroundBtm, 0, 0);

            //g2.CopyFromScreen(PointToScreen(new Point(0, 0)), new Point(0, 0), ClientRect.Size);
            //g.DrawImage(backgroundBtm, 0, 0);
            //if(showRadar)
            //    drawScan(g4, startAngle);
            if(showOpt)
                drawOptScan(g4, optStartAngle);
            g.DrawImage(b2, 0, 0);
            //Bitmap b = new Bitmap(ClientRect.Width, ClientRect.Height, g);
            //Bitmap b2 = b.Clone(new Rectangle(0, 0, ClientRect.Width, ClientRect.Height), b.PixelFormat);
            //var g3 = Graphics.FromImage(b2);
            //b.Save("test.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //b.Dispose();
            g.Dispose();
            //RadarTimer.Enabled = false;
            g4.Dispose();
            b2.Dispose();
            //RadarTimer.Enabled = false;
        }


        private PointF getMappedPoint(float angle, float value, int x, int y, float scanRadius)
        {
            // 计算映射在坐标图中的半径  
            float r = scanRadius * (value - min) / (max - min);
            
            // 计算GDI+坐标  
            PointF pt = new PointF();
            pt.X = (float)(r * Math.Cos(angle * Math.PI / 180) + x);
            pt.Y = (float)(r * Math.Sin(angle * Math.PI / 180) + y);
            return pt;
        }

        //绘扫描线
        private void drawScan(Graphics g, Radar radar)
        {
            int scanAngle = 30;
            PointF point1 = new PointF();
            PointF point2 = new PointF();
            PointF point3 = new PointF();

            int curX = 0, curY = 0;
            axYimaEnc.GetScrnPoFromGeoPo(platformGeoPo.x, platformGeoPo.y, ref curX, ref curY);
            Point center = new Point(curX, curY);


            point1 = getMappedPoint(radar.CurAngle, max, curX, curY, radar.Radius);
            int angle2 = (radar.CurAngle + scanAngle) > 360 ? (radar.CurAngle + scanAngle - 360) : (radar.CurAngle + scanAngle);
            point2 = getMappedPoint(angle2, max, curX, curY, radar.Radius);
            int angle3 = (angle2 + scanAngle) > 360 ? (angle2 + scanAngle - 360) : (angle2 + scanAngle);
            point3 = getMappedPoint(angle3, max, curX, curY, radar.Radius);
            //g.DrawLine(Pens.Red, center, point1);

            GraphicsPath gp = new GraphicsPath(FillMode.Winding);
            gp.AddLine(center, point1);
            gp.AddCurve(new PointF[] { point1, point2, point3 });
            gp.AddLine(point3, center);

            PathGradientBrush pgb = new PathGradientBrush(gp);
            //SolidBrush pgb = new SolidBrush(gp);

            pgb.CenterPoint = point3;
            pgb.CenterColor = radar.ScanColor;//Color.FromArgb(128, Color.FromArgb(0, 255, 0));
            pgb.SurroundColors = new Color[] { Color.Empty };
            //pgb.SurroundColors = new Color[] { cScanColor };
            // draw the fade path
            g.FillPath(pgb, gp);
            // draw the scanline
            //g.DrawLine(pScanPen, center, point1);
            //g.DrawLine(pScanPen, center, point2);
            g.DrawLine(Pens.White, center, point3);
        }
        #endregion

        #region 光电绘图函数
        private void drawOptScan(Graphics g, int angle)
        {
            int scanAngle = optScanAngle;
            PointF point1 = new PointF();
            PointF point2 = new PointF();
            PointF point3 = new PointF();

            int curX = 0, curY = 0;
            axYimaEnc.GetScrnPoFromGeoPo(platformGeoPo.x, platformGeoPo.y, ref curX, ref curY);
            Point center = new Point(curX, curY);


            point1 = getOptMappedPoint(angle, max, curX, curY);
            int angle2 = (angle + scanAngle) > 360 ? (angle + scanAngle - 360) : (angle + scanAngle);
            point2 = getOptMappedPoint(angle2, max, curX, curY);
            int angle3 = (angle2 + scanAngle) > 360 ? (angle2 + scanAngle - 360) : (angle2 + scanAngle);
            point3 = getOptMappedPoint(angle3, max, curX, curY);
            //g.DrawLine(Pens.Red, center, point1);

            GraphicsPath gp = new GraphicsPath(FillMode.Winding);
            gp.AddLine(center, point1);
            gp.AddCurve(new PointF[] { point1, point2, point3 });
            gp.AddLine(point3, center);
            PathGradientBrush pgb = new PathGradientBrush(gp);
            //SolidBrush pgb = new SolidBrush(gp);

            pgb.CenterPoint = point3;
            pgb.CenterColor = oScanColor;//Color.FromArgb(128, Color.FromArgb(0, 255, 0));
            //pgb.SurroundColors = new Color[] { Color.Empty };
            pgb.SurroundColors = new Color[] { oScanColor };
            // draw the fade path
            g.FillPath(pgb, gp);
            // draw the scanline
            g.DrawLine(Pens.White, center, point1);
            //g.DrawLine(pScanPen, center, point2);
            g.DrawLine(Pens.White, center, point3);
        }

        private PointF getOptMappedPoint(float angle, float value, int x, int y)
        {
            // 计算映射在坐标图中的半径  
            float r = optRadius * (value - min) / (max - min);

            // 计算GDI+坐标  
            PointF pt = new PointF();
            pt.X = (float)(r * Math.Cos(angle * Math.PI / 180) + x);
            pt.Y = (float)(r * Math.Sin(angle * Math.PI / 180) + y);
            return pt;
        }
        #endregion

        #region 菜单相应函数

        #region 雷达光电右键菜单
        private void ShowRadarTargetItem_Click(object sender, EventArgs e)
        {
            ShowRadarTargetItem.Checked = !ShowRadarTargetItem.Checked;
            SetDisplayMode(DISPLAY_MODE.READER, !showRadarTarget);
            Invalidate();
        }

        private void ShowAISTargetItem_Click(object sender, EventArgs e)
        {
            ShowAISTargetItem.Checked = !ShowAISTargetItem.Checked;
            SetDisplayMode(DISPLAY_MODE.AIS, !showAISTarget);
            Invalidate();
        }

        private void ShowMergeTargetItem_Click(object sender, EventArgs e)
        {
            ShowMergeTargetItem.Checked = !ShowMergeTargetItem.Checked;
            SetDisplayMode(DISPLAY_MODE.MERGER, !showMergeTarget);
            Invalidate();
        }

        private void ShowOptMenuItem_Click(object sender, EventArgs e)
        {
            ShowOptMenuItem.Checked = !ShowOptMenuItem.Checked;
            SetDisplayMode(DISPLAY_MODE.OPTLINE, !showOpt);
            Invalidate();
        }

        private void ShowRadarMenuItem_Click(object sender, EventArgs e)
        {
            ShowRadarMenuItem.Checked = !ShowRadarMenuItem.Checked;
            SetDisplayMode(DISPLAY_MODE.RADARLINE, !showRadar);
            Invalidate();
        }

        private void ShowTrackMenuItem_Click(object sender, EventArgs e)
        {
            CurSelectedTarget.ShowTrack = !CurSelectedTarget.ShowTrack;
            Invalidate();
        }

        private void ShowAllTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAllTrackToolStripMenuItem.Checked = !ShowAllTrackToolStripMenuItem.Checked;
            SetShowTrackOrNot(ShowAllTrackToolStripMenuItem.Checked);
        }
        #endregion
        #region 多边形区域绘制右键菜单
        private void CancelAdd_Click(object sender, EventArgs e)
        {
            if (IsOnOperation(CURRENT_SUB_OPERATION.ADD_FORBIDDEN_ZONE))
                CancelAddForbiddenZone();
            else if (IsOnOperation(CURRENT_SUB_OPERATION.ADD_PIPELINE))
                CancelAddPipeline();
        }
        private void CancelPoint_Click(object sender, EventArgs e)
        {
            if (IsOnOperation(CURRENT_SUB_OPERATION.ADD_FORBIDDEN_ZONE))
                ClearLastForbiddenZonePoint();
            else if (IsOnOperation(CURRENT_SUB_OPERATION.ADD_PIPELINE))
                ClearLastPipelinePoint();
        }

        private void EndAdd_Click(object sender, EventArgs e)
        {
            if (IsOnOperation(CURRENT_SUB_OPERATION.ADD_FORBIDDEN_ZONE))
            {
                if (AddedForbiddenZone != null)
                {
                    ForbiddenZone fz = EndAddForbiddenZone();
                    AddedForbiddenZone(fz);
                }
                else
                {
                    CancelAddForbiddenZone();
                }
            }
            else if (IsOnOperation(CURRENT_SUB_OPERATION.ADD_PIPELINE))
            {
                if (AddedPipeline != null)
                {
                    Pipeline pl = EndAddPipeline();
                    AddedPipeline(pl);
                }
                else
                {
                    CancelAddPipeline();
                }
            }
        }
        #endregion

        #endregion

        #region 航迹查询
        public void ShowTargetTrack(Target t)
        {
            if (!IsOnOperation(CURRENT_SUB_OPERATION.NO_OPERATION))
                return;
            SetOperation(CURRENT_SUB_OPERATION.SHOWING_TRACK);
            CurShowingTrackTarget = t;
            Invalidate();
        }
        public void FinishShowTargetTrack()
        {
            if (!IsOnOperation(CURRENT_SUB_OPERATION.SHOWING_TRACK))
                return;
            SetOperation(CURRENT_SUB_OPERATION.NO_OPERATION);
            CurShowingTrackTarget = null;
            Invalidate();
        }
        private void DrawTargetTrack(Graphics g, Target t)
        {
            if (t.Track.Count == 0)
                return;
            Color c = AppConfig.TartgetColor[t.Type];
            Brush brush = new SolidBrush(c);
            Pen pen = new Pen(brush, 1);
            int curX = 0, curY = 0;
            Point A = new Point(), B = new Point(), C = new Point(), D = new Point();

            List<Point> list = new List<Point>();
            foreach (var p in t.Track)
            {
                //画出每个点的三角形
                axYimaEnc.GetScrnPoFromGeoPo(p.Point.x, p.Point.y, ref curX, ref curY);
                //Console.WriteLine("{0}, {1}", curX, curY);
                var curScanPoint = new Point(curX, curY);
                A.X = curX;
                A.Y = curY - TargetRectFactor;
                B.X = curX - TargetRectFactor / 2;
                B.Y = curY + TargetRectFactor;
                C.X = curX + TargetRectFactor / 2;
                C.Y = curY + TargetRectFactor;
                D.X = A.X;
                D.Y = A.Y - Convert.ToInt32(t.Speed * 5);
                //根据船的航向旋转三角形
                Rotate(p.Course, ref A, ref B, ref C, ref D, new Point(curX, curY));
                Point[] points = { A, B, C };
                //画出船的图标
                g.FillPolygon(brush, points);
                list.Add(curScanPoint);
                //显示时间
                Brush statusBrush = Brushes.Black;
                Rectangle statusRect = new Rectangle(curX + TargetRectFactor, curY, 150, 20);


                g.DrawString(p.Time, AppConfig.TargetStatusFont, statusBrush, statusRect);
            }
            //Console.WriteLine("------------------------");
            if (list.Count > 1)
            {
                Console.WriteLine("1");
                pen.Width = 3;
                pen.DashStyle = DashStyle.Dot;
                g.DrawCurve(pen, list.ToArray());
            }
        }
        #endregion

        #region 数据回放
        public void StartPlayback()
        {
            SetOperation(CURRENT_SUB_OPERATION.PLAYBACK);
            Invalidate();
        }
        public void FinishPlayback()
        {
            ClearOperation(CURRENT_SUB_OPERATION.PLAYBACK);
            Invalidate();
            AISTargetPlaybackDic.Clear();
            MergeTargetPlaybackDic.Clear();
            RadarTargetPlaybackDic.Clear();
        }
        #endregion

        private void OptLinkageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TargetOptLinkage?.Invoke(null);
        }

        #region 海图操作接口
        public void SetDisplayCategory(DISPLAY_CATEGORY_NUM displayType)
        {
            axYimaEnc.SetDisplayCategory((short)displayType);
        }

        public void SetPlatform(int x, int y)
        {
            GeoPoint p = new GeoPoint(x, y);
            platformGeoPo = p;
        }

        public void CenterMap(int x, int y)
        {
            axYimaEnc.CenterMap(x, y);
        }

        public void CenterMap()
        {
            axYimaEnc.CenterMap(platformGeoPo.x, platformGeoPo.y);
        }
        public void MoveMap(MovingDirection direction)
        {
            double moveStep = 0.33;
            switch(direction)
            {
                case MovingDirection.Up:
                    axYimaEnc.SetMapMoreOffset(0, (int)(axYimaEnc.GetDrawerScreenHeight() * moveStep));
                    break;
                case MovingDirection.Down:
                    axYimaEnc.SetMapMoreOffset(0, -(int)(axYimaEnc.GetDrawerScreenHeight() * moveStep));
                    break;
                case MovingDirection.Left:
                    axYimaEnc.SetMapMoreOffset((int)(axYimaEnc.GetDrawerScreenWidth() * moveStep), 0);
                    break;
                case MovingDirection.Right:
                    axYimaEnc.SetMapMoreOffset(-(int)(axYimaEnc.GetDrawerScreenWidth() * moveStep), 0);
                    break;
            }
            Invalidate();
        }

        public void SetDisplayMode(DISPLAY_MODE mode, bool isShow)
        {
            switch (mode)
            {
                case DISPLAY_MODE.READER:
                    showRadarTarget = isShow;
                    break;
                case DISPLAY_MODE.AIS:
                    showAISTarget = isShow;
                    break;
                case DISPLAY_MODE.MERGER:
                    showMergeTarget = isShow;
                    break;
                case DISPLAY_MODE.RADARLINE:
                    showRadar = isShow;
                    break;
                case DISPLAY_MODE.OPTLINE:
                    showOpt = isShow;
                    break;
            }
        }

        public void SetShowTrackOrNot(bool show)
        {
            showTrack = show;
            var dicList = new List<Target>[] { AISTargetDic.Values.ToList(), MergeTargetDic.Values.ToList(), RadarTargetList };
            foreach(var list in dicList)
                foreach(var t in list)
                {
                    t.ShowTrack = showTrack;
                }
            Invalidate();
        }

        #region 圆形保护区操作
        public bool AddProtectZone(ProtectZone pz)
        {
            if (ProtectZoneMap.Count >= MaxProtectZoneNum)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < MaxProtectZoneNum; i++)
                {
                    ProtectZone tmp;
                    if (!ProtectZoneMap.TryGetValue(i, out tmp))
                    {
                        pz.ID = i;
                        ProtectZoneMap.Add(i, pz);
                        ProtectZoneList.Add(pz);
                        break;
                    }
                }
                return true;
            }
        }

        public bool AddProtectZone(GeoPoint center, float radius, Color color, string name)
        {
            if (center == null)
                center = new GeoPoint(platformGeoPo);
            ProtectZone pz = new ProtectZone(center, radius, color);
            pz.Name = name;
            return AddProtectZone(pz);
        }

        public void DeleteProtectZoneByName(string name)
        {
            int i = 0;
            foreach (ProtectZone pz in ProtectZoneList)
            {
                if (pz.Name == name)
                {
                    ProtectZoneMap.Remove(pz.ID);
                    ProtectZoneList.RemoveAt(i);
                    break;
                }
                i++;
            }
        }

        public void DeleteProtectZoneByID(int ID)
        {
            int i = 0;
            foreach (ProtectZone pz in ProtectZoneList)
            {
                if (pz.ID == ID)
                {
                    ProtectZoneMap.Remove(pz.ID);
                    ProtectZoneList.RemoveAt(i);
                    break;
                }
                i++;
            }
        }

        public ProtectZone FindProtectZoneByName(string name)
        {
            foreach (ProtectZone pz in ProtectZoneList)
            {
                if (pz.Name == name)
                    return pz;
            }
            return null;
        }
        #endregion

        #region 多边形区域操作
        public bool StartAddForbiddenZone()
        {
            if (IsOnOperation(CURRENT_SUB_OPERATION.NO_OPERATION) && ForbiddenZoneList.Count < YimaEncCtrl.MaxForbiddenZoneNum)
            {
                SetOperation(CURRENT_SUB_OPERATION.ADD_FORBIDDEN_ZONE);
                curForbiddenZone = new ForbiddenZone();
                Invalidate();
                return true;
            }
            else
                return false;
        }

        public void ClearLastForbiddenZonePoint()
        {
            if (IsOnOperation(CURRENT_SUB_OPERATION.ADD_FORBIDDEN_ZONE))
            {
                if(curForbiddenZone.PointList.Count > 0)
                {
                    curForbiddenZone.PointList.RemoveAt(curForbiddenZone.PointList.Count - 1);
                    Invalidate();
                }
            }
        }

        public void CancelAddForbiddenZone()
        {
            if (IsOnOperation(CURRENT_SUB_OPERATION.ADD_FORBIDDEN_ZONE))
            {
                curForbiddenZone = null;
                ClearOperation(CURRENT_SUB_OPERATION.ADD_FORBIDDEN_ZONE);
                Invalidate();
            }
        }

        public ForbiddenZone EndAddForbiddenZone()
        {
            ForbiddenZone tmp = null;
            if (IsOnOperation(CURRENT_SUB_OPERATION.ADD_FORBIDDEN_ZONE))
            {
                AddForbiddenZone(curForbiddenZone);
                tmp = curForbiddenZone;
                curForbiddenZone = null;
                ClearOperation(CURRENT_SUB_OPERATION.ADD_FORBIDDEN_ZONE);
                Invalidate();
            }
            return tmp;
        }

        public bool AddForbiddenZone(ForbiddenZone fz)
        {
            if(ForbiddenZoneMap.Count >= MaxForbiddenZoneNum)
            {
                return false;
            }
            else
            {
                for(int i = 0; i< MaxForbiddenZoneNum; i++)
                {
                    ForbiddenZone tmp;
                    if(!ForbiddenZoneMap.TryGetValue(i, out tmp))
                    {
                        fz.ID = i;
                        ForbiddenZoneMap.Add(i, fz);
                        ForbiddenZoneList.Add(fz);
                        break;
                    }
                }
                return true;
            }
        }

        public void DeleteForbiddenZoneByName(string name)
        {
            int i = 0;
            foreach (ForbiddenZone fz in ForbiddenZoneList)
            {
                if (fz.Name == name)
                {
                    ForbiddenZoneMap.Remove(fz.ID);
                    ForbiddenZoneList.RemoveAt(i);
                    break;
                }
                i++;
            }
        }

        public void DeleteForbiddenZoneByID(int id)
        {
            int i = 0;
            foreach (ForbiddenZone fz in ForbiddenZoneList)
            {
                if (fz.ID == id)
                {
                    ForbiddenZoneMap.Remove(fz.ID);
                    ForbiddenZoneList.RemoveAt(i);
                    break;
                }
                i++;
            }
        }

        public ForbiddenZone FindForbiddenZoneByName(string name)
        {
            foreach(ForbiddenZone fz in ForbiddenZoneList)
            {
                if (fz.Name == name)
                    return fz;
            }
            return null;
        }

        public void PreviewForbiddenZone(string name)
        {
            ForbiddenZone fz = FindForbiddenZoneByName(name);
            if (fz != null && fz.PointList.Count > 0)
            {
                var p = fz.PointList[0];
                axYimaEnc.CenterMap(p.x, p.y);
            }
        }
        #endregion

        #region 管道操作
        public bool StartAddPipeline()
        {
            if (IsOnOperation(CURRENT_SUB_OPERATION.NO_OPERATION) && PipelineList.Count < YimaEncCtrl.MaxPipelineZoneNum)
            {
                SetOperation(CURRENT_SUB_OPERATION.ADD_PIPELINE);
                curPipeline = new Pipeline();
                Invalidate();
                return true;
            }
            else
                return false;
        }

        public void ClearLastPipelinePoint()
        {
            if (IsOnOperation(CURRENT_SUB_OPERATION.ADD_PIPELINE))
            {
                if (curPipeline.PointList.Count > 0)
                {
                    curPipeline.PointList.RemoveAt(curPipeline.PointList.Count - 1);
                    Invalidate();
                }
            }
        }

        public void CancelAddPipeline()
        {
            if (IsOnOperation(CURRENT_SUB_OPERATION.ADD_PIPELINE))
            {
                curPipeline = null;
                ClearOperation(CURRENT_SUB_OPERATION.ADD_PIPELINE);
                Invalidate();
            }
        }

        public Pipeline EndAddPipeline()
        {
            Pipeline tmp = null;
            if (IsOnOperation(CURRENT_SUB_OPERATION.ADD_PIPELINE))
            {
                AddPipeline(curPipeline);
                tmp = curPipeline;
                curPipeline = null;
                ClearOperation(CURRENT_SUB_OPERATION.ADD_PIPELINE);
                Invalidate();
            }
            return tmp;
        }

        public bool AddPipeline(Pipeline pl)
        {
            if (PipelineList.Count >= MaxPipelineZoneNum)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < MaxPipelineZoneNum; i++)
                {
                    Pipeline tmp;
                    if (!PipelineMap.TryGetValue(i, out tmp))
                    {
                        pl.ID = i;
                        PipelineMap.Add(i, pl);
                        PipelineList.Add(pl);
                        break;
                    }
                }
                return true;
            }
        }

        public void DeletePipelineByName(string name)
        {
            int i = 0;
            foreach (Pipeline pl in PipelineList)
            {
                if (pl.Name == name)
                {
                    PipelineMap.Remove(pl.ID);
                    PipelineList.RemoveAt(i);
                    break;
                }
                i++;
            }
        }

        public void DeletePipelineByID(int id)
        {
            int i = 0;
            foreach (Pipeline pl in PipelineList)
            {
                if (pl.ID == id)
                {
                    PipelineMap.Remove(pl.ID);
                    PipelineList.RemoveAt(i);
                    break;
                }
                i++;
            }
        }

        public Pipeline FindPipelineByName(string name)
        {
            foreach (Pipeline pl in PipelineList)
            {
                if (pl.Name == name)
                    return pl;
            }
            return null;
        }

        public void PreviewPipeline(string name)
        {
            Pipeline pl = FindPipelineByName(name);
            if (pl != null && pl.PointList.Count > 0)
            {
                var p = pl.PointList[0];
                axYimaEnc.CenterMap(p.x, p.y);
            }
        }
        #endregion

        #region 测距操作
        public void StartRanging()
        {
            if (IsOnOperation(CURRENT_SUB_OPERATION.NO_OPERATION))
            {
                SetOperation(CURRENT_SUB_OPERATION.RANGING);
                Invalidate();
            }
        }

        public void EndRanging()
        {
            if (IsOnOperation(CURRENT_SUB_OPERATION.RANGING))
            {
                startingRangingPoint = null;
                terminalRangingPoint = null;
                ClearOperation(CURRENT_SUB_OPERATION.RANGING);
                Invalidate();
            }
        }

        #endregion
        #region 图库管理接口
        public List<string> GetMapList()
        {
            List<string> list = new List<string>();
            int libMapCount = axYimaEnc.GetLibMapCount();
            //Console.WriteLine(libMapCount);
            string mapName = "                    ";
            string mapType = null;
            float tmp1 = 0;
            int tmp2 = 0;
            for(int i = 0; i < libMapCount; i++)
            {
                axYimaEnc.GetLibMapInfo(i, ref mapType, ref mapName, ref tmp1, ref tmp2, ref tmp2, 
                    ref tmp2, ref tmp2, ref tmp2, ref tmp2);
                list.Add(mapName);
            }
            return list;
        }


        public MapInfo GetMapInfo(int libMapPos)
        {
            if (libMapPos < 0 || libMapPos >= axYimaEnc.GetLibMapCount())
                return null;
            MapInfo info = new MapInfo();
            float originalScale = 0;
            int left = 0, right = 0, up = 0, down = 0;
            int editionNum = 0, updateEdtNum = 0;
            string tmp = null;
            axYimaEnc.GetLibMapInfo(libMapPos, ref tmp, ref tmp, ref originalScale, ref left, ref right,
                ref up, ref down, ref editionNum, ref updateEdtNum);
            info.OriginalScale = originalScale;
            info.Left = left;
            info.Right = right;
            info.Up = up;
            info.Down = down;
            info.EditionNum = editionNum;
            info.UpdateEdtNum = updateEdtNum;
            int iGeoCoorMultiFactor = axYimaEnc.GetGeoCoorMultiFactor();
            info.LeftStrBndry = GetDegreeStringFromGeoCoor(true, left, iGeoCoorMultiFactor);
            info.RightStrBndry = GetDegreeStringFromGeoCoor(true, right, iGeoCoorMultiFactor);
            info.UpStrBndry = GetDegreeStringFromGeoCoor(false, up, iGeoCoorMultiFactor);
            info.DownStrBndry = GetDegreeStringFromGeoCoor(false, down, iGeoCoorMultiFactor);

            tmp = "         ";
            axYimaEnc.GetLibMapEditionIssueDate(libMapPos, updateEdtNum, ref tmp);
            info.EditDate = tmp;
            return info;
        }

        public bool AddMap(string mapPath)
        {
            return axYimaEnc.AddMapToLib(mapPath);
        }

        public void DeleteMap(int libMapPos)
        {
            if (libMapPos < 0 || libMapPos >= axYimaEnc.GetLibMapCount())
                return;

            axYimaEnc.DeleteLibMap(libMapPos);
        }

        public void OverViewMap(int libMapPos)
        {
            if (libMapPos < 0 || libMapPos >= axYimaEnc.GetLibMapCount())
                return;
            axYimaEnc.OverViewLibMap(libMapPos);
        }
        #endregion

        //告警接口
        public void AddAlarm(int x, int y)
        {
            Alarm a = new Alarm();
            a.Location = new GeoPoint(x, y);
            AlarmList.Add(a);
        }

        public void DeleteAlarmByLocation(int x, int y)
        {
            int i=0;
            foreach (Alarm a in AlarmList)
            {
                if (a.Location == new GeoPoint(x, y))
                    AlarmList.RemoveAt(i);
                i++;
            }
        }

        #region 雷达设置
        //雷达角度设置
        public void SetRadarAngle(int angle, int ID)
        {
            Radar radar = GetRadar(ID);
            if (radar != null)
                radar.CurAngle = angle;
            //startAngle = angle;
        }

        public void SetRadarRadius(int radius, int ID)
        {
            //geoRadius = radius;
            Radar radar = GetRadar(ID);
            if (radar != null)
                radar.GeoRadius = radius;
        }
        #endregion
        #region 光电设置
        //光电角度设置
        public void SetOptAngle(int angle)
        {

        }
        //光电扫描半径设置，单位毫米
        public void SetOptRadius(int radius)
        {
            optGeoRadius = radius;
        }
        //光电扫描宽度设定
        public void SetOptScanAngle(int angle)
        {
            optScanAngle = angle;
        }
        #endregion

        #region 雷达目标操作
        public bool AddRadarTarget(Target t, double longitude, double latitude)
        {
            Radar radar = GetRadar(t.RadarID);
            if (radar == null)
                //雷达ID错误
                return false;
            Target tmp;
            if(radar.TargetMap.TryGetValue(t.ID, out tmp))
            {
                //目标已存在
                return false;
            }
            tmp = new Target(t.ID, t.Course, t.Speed);
            tmp.RadarID = t.RadarID;
            tmp.UpdateTime = t.UpdateTime;
            tmp.Distance = t.Distance;
            AddPointToTargetTrack(tmp, t.UpdateTime, t.Course, longitude, latitude);
            radar.TargetMap.Add(tmp.ID, tmp);
            RadarTargetList.Add(tmp);
            return true;
        }

        public void UpdateRadarTarget(Target t, double longitude, double latitude)
        {
            Target tmp = GetRadarTarget(t.RadarID, t.ID);
            if (tmp == null)
                return;
            tmp.Course = t.Course;
            tmp.Speed = t.Speed;
            tmp.UpdateTime = t.UpdateTime;
            tmp.Distance = t.Distance;
            tmp.IsApproach = CheckTargetApproach(tmp.Track.Last().Point, longitude, latitude);
            AddPointToTargetTrack(tmp, t.UpdateTime, t.Course, longitude, latitude);
        }

        public void DeleteRadarTarget(int radarID, int targetNO)
        {
            Radar radar = GetRadar(radarID);
            if (radar == null)
                //雷达ID错误
                return;
            Target tmp;
            if (!radar.TargetMap.TryGetValue(targetNO, out tmp))
            {
                //目标不存在
                return;
            }
            //删除map和list中的类
            radar.TargetMap.Remove(tmp.ID);
            int count = 0;
            foreach(var t in RadarTargetList)
            {
                if (t.ID == targetNO && t.RadarID == radarID)
                {
                    RadarTargetList.RemoveAt(count);
                    break;
                }
                count++;
            }
        }

        public Target GetRadarTarget(int radarID, int targetNO)
        {
            Target tmp = null;
            Radar radar = GetRadar(radarID);
            if (radar == null)
                //雷达ID错误
                return null;
            if (!radar.TargetMap.TryGetValue(targetNO, out tmp))
            {
                //目标不存在
                return null;
            }
            return tmp;
        }
        #endregion


        #region AIS目标操作
        public bool AddAISTarget(Target t, double longitude, double latitude)
        {
            Target tmp;
            if (AISTargetDic.TryGetValue(t.ID, out tmp))
            {
                //目标已存在
                return false;
            }
            tmp = new Target(t.ID, t.Course, t.Speed, TargetSource.AIS);
            tmp.ArriveTime = t.ArriveTime;
            tmp.AISType = t.AISType;
            tmp.CallSign = t.CallSign;
            tmp.Capacity = t.Capacity;
            tmp.Course = t.Course;
            tmp.Destination = t.Destination;
            tmp.Distance = t.Distance;
            tmp.IMO = t.IMO;
            tmp.MaxDeep = t.MaxDeep;
            tmp.MIMSI = t.MIMSI;
            tmp.Name = t.Name;
            tmp.Nationality = t.Nationality;
            tmp.SailStatus = t.SailStatus;
            tmp.UpdateTime = t.UpdateTime;
            tmp.Type = t.Type;
            AddPointToTargetTrack(tmp, t.UpdateTime, t.Course, longitude, latitude);
            AISTargetDic.Add(t.ID, tmp);

            return true;
        }

        public bool UpdateAISTarget(Target t, double longitude, double latitude)
        {
            Target tmp;
            if (!AISTargetDic.TryGetValue(t.ID, out tmp))
            {
                //目标不存在
                return false;
            }
            tmp.ArriveTime = t.ArriveTime;
            tmp.AISType = t.AISType;
            tmp.CallSign = t.CallSign;
            tmp.Capacity = t.Capacity;
            tmp.Course = t.Course;
            tmp.Speed = t.Speed;
            tmp.Destination = t.Destination;
            tmp.Distance = t.Distance;
            tmp.IMO = t.IMO;
            tmp.MaxDeep = t.MaxDeep;
            tmp.MIMSI = t.MIMSI;
            tmp.Name = t.Name;
            tmp.Nationality = t.Nationality;
            tmp.SailStatus = t.SailStatus;
            tmp.UpdateTime = t.UpdateTime;
            tmp.Type = t.Type;
            tmp.IsApproach = CheckTargetApproach(tmp.Track.Last().Point, longitude, latitude);
            AddPointToTargetTrack(tmp, t.UpdateTime, t.Course, longitude, latitude);

            return true;
        }

        public bool DeleteAISTarget(int id)
        {
            Target tmp;
            if (!AISTargetDic.TryGetValue(id, out tmp))
            {
                //目标不存在
                return false;
            }
            AISTargetDic.Remove(id);
            return true;
        }

        public Target GetAISTarget(int id)
        {
            Target tmp = null;
            if (!AISTargetDic.TryGetValue(id, out tmp))
            {
                //目标不存在
                return null;
            }
            return tmp;
        }
        #endregion

        #region 融合目标操作
        public bool AddMergetTarget(Target t, double longitude, double latitude)
        {
            Target tmp;
            if (MergeTargetDic.TryGetValue(t.ID, out tmp))
            {
                //目标已存在
                return false;
            }
            tmp = new Target(t.ID, t.Course, t.Speed, TargetSource.Merge);
            tmp.ArriveTime = t.ArriveTime;
            tmp.AISType = t.AISType;
            tmp.CallSign = t.CallSign;
            tmp.Capacity = t.Capacity;
            tmp.Course = t.Course;
            tmp.Destination = t.Destination;
            tmp.Distance = t.Distance;
            tmp.IMO = t.IMO;
            tmp.MaxDeep = t.MaxDeep;
            tmp.MIMSI = t.MIMSI;
            tmp.Name = t.Name;
            tmp.Nationality = t.Nationality;
            tmp.SailStatus = t.SailStatus;
            tmp.UpdateTime = t.UpdateTime;
            tmp.Type = t.Type;
            AddPointToTargetTrack(tmp, t.UpdateTime, t.Course, longitude, latitude);
            MergeTargetDic.Add(t.ID, tmp);

            return true;
        }

        public bool UpdateMergeTarget(Target t, double longitude, double latitude)
        {
            Target tmp;
            if (!MergeTargetDic.TryGetValue(t.ID, out tmp))
            {
                //目标不存在
                return false;
            }
            tmp.ArriveTime = t.ArriveTime;
            tmp.AISType = t.AISType;
            tmp.CallSign = t.CallSign;
            tmp.Capacity = t.Capacity;
            tmp.Course = t.Course;
            tmp.Speed = t.Speed;
            tmp.Destination = t.Destination;
            tmp.Distance = t.Distance;
            tmp.IMO = t.IMO;
            tmp.MaxDeep = t.MaxDeep;
            tmp.MIMSI = t.MIMSI;
            tmp.Name = t.Name;
            tmp.Nationality = t.Nationality;
            tmp.SailStatus = t.SailStatus;
            tmp.UpdateTime = t.UpdateTime;
            tmp.Type = t.Type;
            tmp.IsApproach = CheckTargetApproach(tmp.Track.Last().Point, longitude, latitude);
            AddPointToTargetTrack(tmp, t.UpdateTime, t.Course, longitude, latitude);

            return true;
        }

        public bool DeleteMergeTarget(int id)
        {
            Target tmp;
            if (!MergeTargetDic.TryGetValue(id, out tmp))
            {
                //目标不存在
                return false;
            }
            MergeTargetDic.Remove(id);
            return true;
        }

        public Target GetMergeTarget(int id)
        {
            Target tmp = null;
            if (!MergeTargetDic.TryGetValue(id, out tmp))
            {
                //目标不存在
                return null;
            }
            return tmp;
        }
        #endregion

        #endregion
        //通过ID获取radar结构体
        private Radar GetRadar(int ID)
        {
            Radar radar = null;
            switch(ID)
            {
                case 1:
                    radar = radar1;
                    break;
                case 2:
                    radar = radar2;
                    break;
                default:
                    break;
            }
            return radar;
        }


        private string GetDegreeStringFromGeoCoor(bool bLongOrLatiCoor,
                                int coorVal, int coorMultiFactor)
        {
            if (coorMultiFactor == 0)
                return null;

            double fArcByDegree = coorVal / (float)coorMultiFactor;
            string retDegreeString = null;

            if (bLongOrLatiCoor)
            {
                if (fArcByDegree >= 0)
                {
                    //Console.WriteLine(fArcByDegree);
                    retDegreeString = string.Format("{0:D3}度{1:000000.000}分E", (int)fArcByDegree,
                        60 * (fArcByDegree % 1));
                }
                else
                {
                    fArcByDegree = -fArcByDegree;
                    retDegreeString = string.Format("{0:D3}度{1:000000.000}分W", (int)fArcByDegree,
                        60 * (fArcByDegree - (int)fArcByDegree));
                }
            }
            else
            {
                if (fArcByDegree >= 0)
                {
                    retDegreeString = string.Format("{0:D3}度{1:000000.000}分N", (int)fArcByDegree,
                        60 * (fArcByDegree - (int)fArcByDegree));
                }
                else
                {
                    fArcByDegree = -fArcByDegree;
                    retDegreeString = string.Format("{0:D3}度{1:000000.000}分S", (int)fArcByDegree,
                        60 * (fArcByDegree - (int)fArcByDegree));
                }
            }

            return retDegreeString;
        }

        private void AddPointToTargetTrack(Target t,string time, float course, double longitude, double latitude)
        {
            int iGeoCoorMultiFactor = axYimaEnc.GetGeoCoorMultiFactor();
            GeoPoint gp = new GeoPoint(Convert.ToInt32(longitude * iGeoCoorMultiFactor), Convert.ToInt32(latitude * iGeoCoorMultiFactor));
            TrackPoint tp = new TrackPoint(gp);
            tp.Course = course;
            tp.Time = time;
            t.Track.Add(tp);
        }

        private bool CheckTargetApproach(GeoPoint lastPoint, double longitude, double latitude)
        {
            int iGeoCoorMultiFactor = axYimaEnc.GetGeoCoorMultiFactor();
            GeoPoint cp = new GeoPoint(Convert.ToInt32(longitude * iGeoCoorMultiFactor), Convert.ToInt32(latitude * iGeoCoorMultiFactor));
            int ox = 0, oy = 0;
            axYimaEnc.GetScrnPoFromGeoPo(platformGeoPo.x, platformGeoPo.y, ref ox, ref oy);
            int lx = 0, ly = 0;
            axYimaEnc.GetScrnPoFromGeoPo(lastPoint.x, lastPoint.y, ref lx, ref ly);
            int cx = 0, cy = 0;
            axYimaEnc.GetScrnPoFromGeoPo(cp.x, cp.y, ref cx, ref cy);
            double ld = Math.Sqrt(Math.Pow(lx - ox, 2) + Math.Pow(ly - oy, 2));
            double cd = Math.Sqrt(Math.Pow(cx - ox, 2) + Math.Pow(cy - oy, 2));
            if(cd > ld)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
