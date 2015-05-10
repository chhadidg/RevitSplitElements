using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EditElements.RevitModel
{
    public class Beams
    {
        public static Curve Get(Document uidoc, Dictionary<ElementId, double> level, FamilyInstance c)
        {
            try
            {
                LocationCurve locationCurve = c.Location as LocationCurve;

                if (locationCurve != null)
                {
                    return locationCurve.Curve;
                }
                else
                {
                    MessageBox.Show("Beams: no Curve.");
                    return null;
                }         
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }
    }
}
