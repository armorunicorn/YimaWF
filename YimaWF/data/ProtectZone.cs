using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YimaEncCtrl;

namespace YimaWF.data
{
    public class ProtectZone
    {
        public GeoPoint Center;
        /// <summary>
        /// 半径，单位米
        /// </summary>
        public float Radius;
        public Color ContentColor;

        public ProtectZone(GeoPoint p, float r, Color c)
        {
            Center = p;
            Radius = r;
            ContentColor = Color.FromArgb(50, c);
        }

        public int ID;
        public string Name;

        public int AlarmLevel = 200;
    }
}
