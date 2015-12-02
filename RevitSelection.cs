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
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            ICollection<ElementId> _elements;

            try
            {
                _elements = uidoc.Selection.GetElementIds();

                if (_elements.Count > 0)
                {
                    //MessageBox.Show(string.Format("{0} objects have been picked back.", _elements.Count));
                    return _elements;
                }
                else
                {
                    Selection selection = uidoc.Selection;
                    IList<Reference> picked = selection.PickObjects(ObjectType.Element, commandline);

                    if (picked.Count > 0)
                    {
                        foreach (Reference r in picked)
                        {
                            //int eId = Convert.ToInt32();
                            _elements.Add(r.ElementId);
                        }
                        return _elements;
                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
