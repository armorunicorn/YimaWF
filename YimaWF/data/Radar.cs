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
        public double CurAngle { get; set; }
        //雷达圆在图上的半径（像素）
        public float Radius;
        //雷达的物理半径（毫米）
        public int GeoRadius;
        //每100毫秒所走的角度
        public double AnglePer100ms { get; set; }

        public Dictionary<int, Target> TargetMap = new Dictionary<int, Target>();
    }
}
