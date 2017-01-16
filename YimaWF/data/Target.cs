using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YimaEncCtrl;

namespace YimaWF.data
{
    public enum TargetSource
    {
        AIS,
        Radar, 
        Merge
    }
    //告警类型
    public enum AlarmType
    {
        None, //无告警
        ProtectZone, //圆形保护区告警
        ForbiddenZone, //多边形保护区告警
        Pipeline //管道告警
    }

    public enum TargetType
    {
        WorkBoat, //工作船
        FishingBoat,//渔船
        VietnamFishingBoat, //越南渔船
        MerChantBoat,//商船
        Yacht, //游艇
        Unknow //未知
    }

    public class Target
    {
        public TargetSource Source;
        public TargetType Type = TargetType.Unknow;
        public List<TrackPoint> Track = new List<TrackPoint>();
        public float Speed = 0;
        public float Course = 0;
        //距离
        public int Distance;
        public int ID = 0;
        public bool IsCheck = false;
        public short ShowSignTime = 0;
        public bool IsApproach = false;
        public bool ShowTrack = true;
        #region 雷达数据
        public int RadarID = 0;
        #endregion

        #region AIS数据
        public int IMO;
        public string MIMSI;
        public string CallSign;
        //国籍
        public string Nationality;
        //public string Date;
        public string UpdateTime;
        public string Name = "未知";
        public string ArriveTime;
        //航行状态
        public byte SailStatus;
        //最大吃水深度
        public float MaxDeep;
        //船载人数
        public int Capacity;
        //目的地
        public string Destination;
        public int AISType;
        #endregion

        #region  融合数据
        //融合数据类型
        public byte DataType;
        //子源个数
        public byte SrcNum;
        public AlarmType Alarm = AlarmType.None;
        public int AlarmID = -1;
        #endregion



        public Target(int id, float course, float speed, TargetSource source = TargetSource.Radar)
        {
            Source = source;
            ID = id;
            Course = course;
            Speed = speed;
        }
    }
}
