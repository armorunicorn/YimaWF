using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YimaEncCtrl;

namespace YimaWF.data
{
    public class TrackPoint
    {
        public GeoPoint Point;
        public string Time;
        public float Heading;

        public TrackPoint(GeoPoint p)
        {
            Point = p;
        }
    }
}
