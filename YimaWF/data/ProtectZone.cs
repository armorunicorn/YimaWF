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
        private Color contentColor;
        public Color ContentColor
        {
            get { return contentColor; }
            set
            {
                contentColor = Color.FromArgb(50, value);
            }
        }

        public ProtectZone(GeoPoint p, float r, Color c)
        {
            Center = p;
            Radius = r;
            ContentColor = c;
        }

        public int ID;
        public string Name;

        public int AlarmLevel = 200;
    }
}
