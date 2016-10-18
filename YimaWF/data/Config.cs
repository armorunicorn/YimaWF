using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YimaWF.data
{
    public class Config
    {
        public Dictionary<TargetType, Color> TartgetColor = new Dictionary<TargetType, Color>();
        public Font TargetStatusFont;
        public Color ProtectZonePen;
        public Pen TargetSelectPen;
        public Color ApproachRadarTarget;
        public Color AloofRadarTarget;
    }
}
