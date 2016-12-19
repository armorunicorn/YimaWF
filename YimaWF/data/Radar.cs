using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YimaWF.data
{
    public class Radar
    {
        public int ID;
        public Color ScanColor = Color.FromArgb(0, 255, 0);
        public int CurAngle = 0;
        //雷达圆在图上的半径（像素）
        public float Radius;
        //雷达的物理半径（毫米）
        public int GeoRadius;

        public Dictionary<int, Target> TargetMap = new Dictionary<int, Target>();
    }
}
