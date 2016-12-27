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
        public TargetSource Source = TargetSource.Radar;
        public TargetType Type = TargetType.Unknow;
        public List<TrackPoint> Track = new List<TrackPoint>();
        public float Speed = 0;
        public float Course = 0;
        public int ID = 0;
        public bool IsCheck = false;
        public string IMO;
        public string MIMSI;
        public string CallSign;
        //国籍
        public string Nationality;
        //距离
        public int Destination;
        //public string Date;
        public string UpdateTime;
        public string Name = "未知";
        public string ArriveTime;
        public short ShowSignTime = 0;
        public bool IsApproach = false;
        public bool ShowTrack = true;
        public int RadarID = 0;

        public Target(int radarID, int id, float course, float speed)
        {
            RadarID = radarID;
            ID = id;
            Course = course;
            Speed = speed;
        }
    }
}
