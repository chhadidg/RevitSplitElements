using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditElements.Utils
{
    public class RevitLevelId
    {
        public static ElementId SearchForClosestLevel(Document doc, Dictionary<ElementId, double> level, double _z, List<double> _elevations)
        {
            double closest = _elevations.Aggregate((x, y) => Math.Abs(x - _z) < Math.Abs(y - _z) ? x : y);

            ElementId levelId  = level.FirstOrDefault(x => x.Value == closest).Key;

            return levelId;
        }
    }
}
