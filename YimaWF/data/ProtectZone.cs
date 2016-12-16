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
        public float Radius;
        public Color ContentColor;

        public ProtectZone(GeoPoint p, float r, Color c)
        {
            Center = p;
            Radius = r;
            ContentColor = Color.FromArgb(50, c);
        }

        public string Name;
    }
}
