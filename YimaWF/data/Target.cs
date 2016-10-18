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
        public float Heading = 0;
        public int ID = 0;
        public bool IsCheck = false;
        public string IMO;
        public string MIMSI;
        public string CallSign;
        public string Nationality;
        public string Destination;
        public string Date;
        public string Time;
        public string Name;
        public string ArriveTime;
        public short ShowSignTime = 0;
        public bool IsApproach = false;
        public bool ShowTrack = true;

        public Target(int id, float heading, float speed)
        {
            ID = id;
            Heading = heading;
            Speed = speed;
        }
    }
}
