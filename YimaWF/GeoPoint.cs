using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YimaEncCtrl
{
    public class GeoPoint
    {
        public int x = 0;
        public int y = 0;
        public GeoPoint()
        {

        }
        public GeoPoint(long _x, long _y)
        {
            x = (int)_x;
            y = (int)_y;
        }

        public GeoPoint(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public GeoPoint(GeoPoint geoPo)
        {
            x = geoPo.x;
            y = geoPo.y;
        }
    }
}
