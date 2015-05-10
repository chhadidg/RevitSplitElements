using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using EditElements.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EditElements
{
    public class RevitSelection
    {
        public static ICollection<ElementId> Get(UIApplication uiapp, string commandline)
        {
            UIDocument active_uidoc = uiapp.ActiveUIDocument;
            Document uidoc = active_uidoc.Document;

            ICollection<ElementId> _elements;

            try
            {
                _elements = active_uidoc.Selection.GetElementIds();

                if (_elements.Count > 0)
                {
                    //MessageBox.Show(string.Format("{0} objects have been picked back.", _elements.Count));
                    return _elements;
                }
                else
                {
                    Selection selection = active_uidoc.Selection;
                    IList<Reference> picked = selection.PickObjects(ObjectType.Element, commandline);
                    
                    foreach (Reference r in picked)
                    {
                        //int eId = Convert.ToInt32();
                        _elements.Add(r.ElementId);
                    }
                    return _elements;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
