using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        Contact, //平台接触告警 - level 1
        ExpulsionArea, //驱逐区告警 - level 2
        AlertArea, //警戒区告警 - level 3
        EarlyWarningArea, //预警区告警 - level 4
        ForbiddenZone, //多边形保护区告警 - level 4
        Pipeline, //管道告警 - level 4
        Checked  //已确认（不再显示）
    }

    public enum AlarmAction
    {
        None = 0,
        Into = 1,
        Out = 2,
        Resident = 3
    }

    public enum TargetType
    {
        Suspicious, //可疑
        Normal, //一般
        Unknow //未知
    }

    public class Target : INotifyPropertyChanged
    {
        public TargetSource Source { get; set; }
        public TargetType Type { get; set; }
        public List<TrackPoint> Track = new List<TrackPoint>();
        private float speed = 0;
        public float Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                NotifyPropertyChanged("Speed");
            }
        }
        private double course = 0;
        public double Course
        {
            get { return course; }
            set
            {
                course = value;
                NotifyPropertyChanged("Course");
            }
        }
        //距离
        private int distance;
        public int Distance
        {
            get { return distance; }
            set
            {
                distance = value;
                NotifyPropertyChanged("Distance");
            }
        }

        private double north;
        public double North
        {
            get { return north; }
            set
            {
                north = value;
                NotifyPropertyChanged("North");
            }
        }

        public int ID { get; set; }
        private bool isSelected = false;
        //目标是否被选中
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }
        public short ShowSignTime = 0;
        public bool IsApproach = false;
        public bool ShowTrack = true;
        public bool ShowStatus { get; set; }

        private string name;
        public string Name
        {
            get
            {
                if (Source == TargetSource.Radar && name != null)
                {
                    return RadarID + "-" + ID;
                }
                else
                {
                    if (name != null)
                        return name;
                    else
                        return ID.ToString();
                }
            }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private double longitude;
        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                NotifyPropertyChanged("Longitude");
            }
        }

        private double latitude;
        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                NotifyPropertyChanged("Latitude");
            }
        }

        #region 雷达数据
        //雷达ID
        public int RadarID { get; set; }
        public int RadarBatchNum { get; set; }
        #endregion

        #region AIS数据
        public bool ShowSpeedLine = false;
        public uint IMO { get; set; }
        public string MIMSI { get; set; }
        public string CallSign { get; set; }
        //国籍
        public string Nationality { get; set; }
        //public string Date;
        private string updateTime;
        public string UpdateTime
        {
            get { return updateTime; }
            set
            {
                updateTime = value;
                NotifyPropertyChanged("UpdateTime");
            }
        }

        /// <summary>
        /// 到达目的地的时间
        /// </summary>
        private string arriveTime;
        public string ArriveTime
        {
            get { return arriveTime; }
            set
            {
                arriveTime = value;
                NotifyPropertyChanged("ArriveTime");
            }
        }
        /// <summary>
        /// 到达平台的时间
        /// </summary>
        private string arrivePlatformTime;
        public string ArrivePlatformTime
        {
            get { return arrivePlatformTime; }
            set
            {
                arrivePlatformTime = value;
                NotifyPropertyChanged("ArrivePlatformTime");
            }
        }
        //航行状态
        public byte SailStatus { get; set; }
        //最大吃水深度
        public float MaxDeep { get; set; }
        //船载人数
        public uint Capacity { get; set; }
        //目的地
        public string Destination { get; set; }
        public int AISType { get; set; }
        #endregion

        #region  融合数据
        //融合数据类型
        public byte DataType { get; set; }
        //子源个数
        public byte SrcNum { get; set; }
        //告警类型
        private AlarmType alarm = AlarmType.None;
        public AlarmType Alarm
        {
            get { return alarm; }
            set
            {
                alarm = value;
                NotifyPropertyChanged("Alarm");
            }
        }

        private bool alarmCheck;
        public bool AlarmCheck
        {
            get { return alarmCheck; }
            set
            {
                alarmCheck = value;
                NotifyPropertyChanged("AlarmCheck");
            }
        }

        private AlarmAction action = AlarmAction.None;
        public AlarmAction Action
        {
            get { return action; }
            set
            {
                action = value;
                NotifyPropertyChanged("Action");
            }
        }

        //告警时间
        private string alarmTime;
        public string AlarmTime
        {
            get { return alarmTime; }
            set
            {
                alarmTime = value;
                NotifyPropertyChanged("AlarmTime");
            }
        }
        public int AlarmID { get; set; }
        #endregion



        public Target(int id, double course, float speed, TargetSource source = TargetSource.Radar)
        {
            Source = source;
            ID = id;
            Course = course;
            Speed = speed;
            Type = TargetType.Unknow;
            ShowStatus = true;
        }

        public bool Equals(Target t)
        {
            if (t != null)
                if (this.Source == t.Source && this.ID == t.ID)
                {
                    if (t.Source == TargetSource.Radar)
                    {
                        if (this.RadarID != t.RadarID)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            return false;
        }


        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
