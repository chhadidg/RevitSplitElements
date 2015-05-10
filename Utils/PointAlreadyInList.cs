using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditElements.Utils
{
    public class PointAlreadyInList
    {
        public static bool Check(List<XYZ> points, XYZ P)
        {
            foreach (XYZ p in points)
            {
                if (Math.Round(p.X, 6) == Math.Round(P.X, 6) && Math.Round(p.Y, 6) == Math.Round(P.Y, 6) && Math.Round(p.Z, 6) == Math.Round(P.Z, 6))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
