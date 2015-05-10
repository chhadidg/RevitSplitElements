using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditElements.RevitModel
{
    public class Braces
    {
        public static Curve Get(Document uidoc, Dictionary<ElementId, double> level, FamilyInstance c)
        {
            LocationCurve locationCurve = c.Location as LocationCurve;

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
