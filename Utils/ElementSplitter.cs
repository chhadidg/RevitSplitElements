using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EditElements.Utils
{
    public class ElementSplitter
    {
        public static List<Curve> Do(Curve c, List<XYZ> p)
        {
            List<double> t = new List<double>();
            List<Curve> curves = new List<Curve>();


            XYZ P = new XYZ(c.GetEndPoint(0).X, c.GetEndPoint(0).Y, c.GetEndPoint(0).Z);    // Start point of curve as reference point
            XYZ Q = new XYZ(c.GetEndPoint(1).X, c.GetEndPoint(1).Y, c.GetEndPoint(1).Z);    // End point of curve as reference point

            XYZ D = new XYZ(Q.X - P.X, Q.Y - P.Y, Q.Z - P.Z);

            double DD = D.DotProduct(D);    // Dotproduct D
            double t_aux = 0.0;
            double WW = 0.0;

            double tol_WW = Math.Pow(GlobalConstants.tolerance, 2.0);

            if (DD < GlobalConstants.tolerance)
            {
                MessageBox.Show("Member of zero lenght detected!");
                return null;
            }

            for (int i = 0; i < p.Count; i++)
            {
                XYZ N = new XYZ(p[i].X - P.X, p[i].Y - P.Y, p[i].Z - P.Z);
                t_aux = N.DotProduct(D) / DD;

                // W is the vector perpendicular to the line to point A
                XYZ W = new XYZ(N.X - t_aux * D.X, N.Y - t_aux * D.Y, N.Z - t_aux * D.Z);

                WW = W.DotProduct(W);

                if (WW < tol_WW && t_aux > GlobalConstants.tolerance && t_aux < (1 - GlobalConstants.tolerance))
                {
                    t.Add(t_aux);
                }
            }

            t.Sort();

            XYZ Qnew = P;

            for (int i = 0; i <= t.Count; i++)
            {
                XYZ Pnew = Qnew;

                if (i == t.Count)
                {
                    Qnew = Q;
                }
                else
                {
                    Qnew = P + t[i] * D;
                }
                
                Line line = Line.CreateBound(Pnew, Qnew);
                Curve curve = line as Curve;
                curves.Add(curve);
            }

            return curves;
        }
    }
}
