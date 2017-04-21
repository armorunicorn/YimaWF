using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YimaEncCtrl;

namespace YimaWF.data
{
    public class Pipeline
    {
        public List<GeoPoint> PointList = new List<GeoPoint>();

        public int ID = 0;

        public string Name;
        //管道宽度，单位毫米
        public int width = 1000;

        public int AlarmLevel = 200;

        public string Remark = "";
    }
}
