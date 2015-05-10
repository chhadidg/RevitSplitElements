using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditElements.RevitModel
{
    public class Parameters
    {


        public static double get_ParameterByDouble(Element e, string lookup)
        {
            double aDouble = 0;
            bool found = false;

            ParameterSetIterator i = e.Parameters.ForwardIterator();
            i.Reset();
            bool iMoreAttribute = i.MoveNext();

            while (iMoreAttribute)
            {
                object o = i.Current;
                Parameter familyAttribute = o as Parameter;

                //find the parameter whose name is same to the given parameter name 
                Autodesk.Revit.DB.StorageType st = familyAttribute.StorageType;
                switch (st)
                {
                    //get the storage type
                    case StorageType.Double:

                        if (familyAttribute.Definition.Name == lookup)
                        {
                            //make conversion between degrees and radians
                            aDouble = familyAttribute.AsDouble();
                            found = true;
                        }
                        break;
                }

                if (found)
                {
                    break;
                }
                else
                {
                    iMoreAttribute = i.MoveNext();
                }
            }
            return aDouble;
        }


        public static ElementId get_ParameterByElementId(Element e, string lookup)
        {
            ElementId aId = null;
            bool found = false;

            ParameterSetIterator i = e.Parameters.ForwardIterator();
            i.Reset();
            bool iMoreAttribute = i.MoveNext();

            while (iMoreAttribute)
            {
                object o = i.Current;
                Parameter familyAttribute = o as Parameter;

                //find the parameter whose name is same to the given parameter name 
                Autodesk.Revit.DB.StorageType st = familyAttribute.StorageType;
                switch (st)
                {
                    //get the storage type
                    case StorageType.ElementId:

                        if (familyAttribute.Definition.Name == lookup)
                        {
                            //make conversion between degrees and radians
                            aId = familyAttribute.AsElementId();
                            found = true;
                        }
                        break;
                }

                if (found)
                {
                    break;
                }
                else
                {
                    iMoreAttribute = i.MoveNext();
                }
            }
            return aId;
        }

        public static string get_ParameterByString(Element e, string lookup)
        {
            string aString = "";
            bool found = false;

            ParameterSetIterator i = e.Parameters.ForwardIterator();
            i.Reset();
            bool iMoreAttribute = i.MoveNext();

            while (iMoreAttribute)
            {
                object o = i.Current;
                Parameter familyAttribute = o as Parameter;

                //find the parameter whose name is same to the given parameter name 
                Autodesk.Revit.DB.StorageType st = familyAttribute.StorageType;
                switch (st)
                {
                    //get the storage type
                    case StorageType.String:

                        if (familyAttribute.Definition.Name == lookup)
                        {
                            //make conversion between degrees and radians
                            aString = familyAttribute.AsString();
                            found = true;
                        }
                        break;
                }

                if (found)
                {
                    break;
                }
                else
                {
                    iMoreAttribute = i.MoveNext();
                }
            }
            return aString;
        }

    }
}
