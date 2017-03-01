using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YimaWF.data
{
    //保存项目所有的配置信息
    public class Config
    {
        public Dictionary<TargetType, Color> TartgetColor = new Dictionary<TargetType, Color>();
        public Font TargetStatusFont;
        public Font RangFont;
        public Color ProtectZonePen;
        public Pen TargetSelectPen;
        public Color ApproachRadarTarget;
        public Color AloofRadarTarget;
        public Pen RadarPen;
        public int RadarCirclesNum = 0;
        public int RadarIntervalLineCount = 0;
    }
}
