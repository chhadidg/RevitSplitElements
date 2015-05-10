using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditElements.RevitModel
{
    public class Columns
    {
        public static Curve Get(Document uidoc, Dictionary<ElementId, double> level, FamilyInstance c)
        {
            XYZ endPoint0 = null;
            XYZ endPoint1 = null;

            LocationCurve locationCurve = c.Location as LocationCurve;
            ElementId levelId = c.LevelId;

            ElementId baseLevelId = Parameters.get_ParameterByElementId(c, "Base Level");
            ElementId topLevelId = Parameters.get_ParameterByElementId(c, "Top Level");

            double base_Z = 0.0;
            double top_Z = 0.0;

            if (baseLevelId != null)
            {
                level.TryGetValue(baseLevelId, out base_Z);
            }

            if (topLevelId != null)
            {
                level.TryGetValue(topLevelId, out top_Z);
            }

            if (locationCurve == null)
            {
                LocationPoint locationPoint = c.Location as LocationPoint;

                endPoint0 = new XYZ(locationPoint.Point.X, locationPoint.Point.Y, base_Z);
                endPoint1 = new XYZ(locationPoint.Point.X, locationPoint.Point.Y, top_Z);
            }
            else
            {
                endPoint0 = locationCurve.Curve.GetEndPoint(0);
                endPoint1 = locationCurve.Curve.GetEndPoint(1);
            }

            Line line = Line.CreateBound(endPoint0, endPoint1);
            Curve curve = line as Curve;

            return curve;
        }
    }
}
