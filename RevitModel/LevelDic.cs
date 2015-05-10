using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditElements.RevitModel
{
    public class LevelElevation
    {
        public static Dictionary<ElementId, double> CreateDic(UIApplication uiapp)
        {
            UIDocument active_uidoc = uiapp.ActiveUIDocument;
            Document uidoc = active_uidoc.Document;
            FilteredElementCollector level_collector = new FilteredElementCollector(uidoc);
            ICollection<Element> collection = level_collector.OfClass(typeof(Level)).ToElements();

            Dictionary<ElementId, double> LevelList = new Dictionary<ElementId, double>();

            foreach (Element e in collection)
            {
                Level level = e as Level;

                LevelList.Add(level.Id, level.Elevation);
            }
            return LevelList;
        }

        public static List<double> GetElevations(Document uidoc)
        {
            FilteredElementCollector level_collector = new FilteredElementCollector(uidoc);
            ICollection<Element> collection = level_collector.OfClass(typeof(Level)).ToElements();

            List<double> LevelList = new List<double>();

            foreach (Element e in collection)
            {
                Level level = e as Level;
                LevelList.Add(level.Elevation);
            }
            return LevelList;
        }
    }
}
