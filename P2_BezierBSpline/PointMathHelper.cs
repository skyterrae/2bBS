using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace P2_BezierBSpline
{
    static class PointMathHelper
    {
        //een aantal rekenfuncties met betrekking tot punten
        //die op verschillende plekken nuttig zijn (vandaar statisch)

        public static float Clamp(float value, float min, float max)
        {
            // forceerd waarden tussen twee punten
            if (max <= value)
                return max;
            else if (value <= min)
                return min;
            else return value;
        }
        public static PointF Add(PointF A, PointF B)
        {
            // + operator tussen twee punten:  A+B
            return new PointF(A.X + B.X, A.Y + B.Y);
        }
        public static PointF Mult(float c, PointF P)
        {
            // * operator tussen coeficient en punt:   c*P
            return new PointF(c * P.X, c * P.Y);
        }
    }
}
