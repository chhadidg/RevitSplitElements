using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditElements.RevitModel
{
    public class Walls
    {
        public static Curve Get(Document doc, Dictionary<ElementId, double> level, Wall w)
        {
            LocationCurve locationCurve = w.Location as LocationCurve;

            if (locationCurve != null)
            {
                return locationCurve.Curve;
            }
            else
            {
                return null;
            }
        }
    }
}
