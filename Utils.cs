using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace EditElements.Utils
{
    public class GlobalVariables
    {
        static ICollection<ElementId> i_SplitObjects;
        static ICollection<ElementId> i_CutObjects;
        static int _MessageVerticalColumn = 0;
        static bool _RememberColumns = true;

        public static ICollection<ElementId> SplitObjects
        {
            get { return i_SplitObjects; }
            set { i_SplitObjects = value; }
        }
        public static ICollection<ElementId> CutObjects
        {
            get { return i_CutObjects; }
            set { i_CutObjects = value; }
        }
        public static int MessageVerticalColumn
        {
            get { return _MessageVerticalColumn; }
            set { _MessageVerticalColumn = value; }
        }
        public static bool RememberColumns
        {
            get { return _RememberColumns; }
            set { _RememberColumns = value; }
        }
    }

    public class FamilySymbols
    {
        private string aSectionName;
        private FamilySymbol aFamilySymbolName;

        public FamilySymbols(string sectionName, FamilySymbol familySymbolName)
        {
            this.aSectionName = sectionName;
            this.aFamilySymbolName = familySymbolName;
        }

        public string SectionName
        {
            get { return this.aSectionName; }
            set { this.aSectionName = value; }
        }

        public FamilySymbol FamilySymbol
        {
            get { return this.aFamilySymbolName; }
            set { this.aFamilySymbolName = value; }
        }
    }
}
