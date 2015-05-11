using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using EditElements.RevitModel;
using EditElements.Swallower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EditElements.Utils
{
    public class Elements
    {
        public static ICollection<ElementId> Transform(Document uidoc, ElementId id, XYZ trans)
        {
            ICollection<ElementId> newElements = ElementTransformUtils.CopyElement(uidoc, id, trans);
            return newElements;
        }

        public static Result ChangeEndPoint(Document uidoc, Curve newCurve, FamilyInstance f, Dictionary<ElementId, double> level, List<double> _elevations)
        {
            LocationCurve location = f.Location as LocationCurve;

            if (location != null)
            {
                try
                {
                    location.Curve = newCurve;

                    ElementId b_Id = RevitLevelId.SearchForClosestLevel(uidoc, level, newCurve.GetEndPoint(0).Z, _elevations);
                    ElementId t_Id = RevitLevelId.SearchForClosestLevel(uidoc, level, newCurve.GetEndPoint(1).Z, _elevations);

                    Parameter bottomLevelParameter = f.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_PARAM);
                    if (null != bottomLevelParameter)
                    {
                        bottomLevelParameter.Set(b_Id);
                    }

                    Parameter topLevelParameter = f.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_PARAM);
                    if (null != topLevelParameter)
                    {
                        topLevelParameter.Set(t_Id);
                    }
                    return Result.Succeeded;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return Result.Failed;
                }
            }
            else
            {
                LocationPoint locationPoint = f.Location as LocationPoint;

                if (locationPoint != null)
                {
                    if (GlobalVariables.MessageVerticalColumn == 0)
                    {
                        ColumnForm cf = new ColumnForm();
                        cf.ShowDialog(); 
                    }

                    if (GlobalVariables.MessageVerticalColumn == 1)
                    {
                        ElementId b_Id = RevitLevelId.SearchForClosestLevel(uidoc, level, newCurve.GetEndPoint(0).Z, _elevations);
                        ElementId t_Id = RevitLevelId.SearchForClosestLevel(uidoc, level, newCurve.GetEndPoint(1).Z, _elevations);
                        locationPoint.Point = newCurve.GetEndPoint(0);

                        Parameter topLevelParameter = f.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_PARAM);
                        if (null != topLevelParameter)
                        {
                            topLevelParameter.Set(t_Id);
                        }
                        Parameter baseLevelParameter = f.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_PARAM);
                        if (null != baseLevelParameter)
                        {
                            baseLevelParameter.Set(b_Id);
                        }
                    }
                    else if (GlobalVariables.MessageVerticalColumn == 2)
                    {
                        // Slanted column
                        Parameter p = f.get_Parameter(BuiltInParameter.SLANTED_COLUMN_TYPE_PARAM);
                        if (p != null)
                        {
                            p.Set(Convert.ToInt32(SlantedOrVerticalColumnType.CT_EndPoint));
                        }
                        location = f.Location as LocationCurve;

                        if (location != null)
                        {
                            try
                            {
                                location.Curve = newCurve;

                                ElementId b_Id = RevitLevelId.SearchForClosestLevel(uidoc, level, newCurve.GetEndPoint(0).Z, _elevations);
                                ElementId t_Id = RevitLevelId.SearchForClosestLevel(uidoc, level, newCurve.GetEndPoint(1).Z, _elevations);
                                
                                Parameter bottomLevelParameter = f.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_PARAM);
                                if (null != bottomLevelParameter)
                                {
                                    bottomLevelParameter.Set(b_Id);
                                }
                                
                                Parameter topLevelParameter = f.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_PARAM);
                                if (null != topLevelParameter)
                                {
                                    topLevelParameter.Set(t_Id);
                                }
                                return Result.Succeeded;
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.ToString());
                                return Result.Failed;
                            }
                        }
                    }
                    else
                    {
                        return Result.Failed;
                    }

                    return Result.Succeeded;
                }
                else
                {
                    return Result.Failed;
                }
            }
        }

        public static Curve GetCurve(Document uidoc, Dictionary<ElementId, double> level, ElementId id)
        {
            Curve curve = null;

            Options opt = new Options();

            try
            {
                Element g1 = uidoc.GetElement(id);
                GeometryElement geomElem = g1.get_Geometry(opt);

                List<ElementId> selelementsid = new List<ElementId>();
                selelementsid.Add(g1.Id);

                FilteredElementCollector collector = new FilteredElementCollector(uidoc, selelementsid);
                ElementClassFilter famInstFilter = new ElementClassFilter(typeof(FamilyInstance));
                ICollection<Element> elems = collector.WherePasses(famInstFilter).ToElements();

                if (elems.Count > 0)
                {
                    foreach (FamilyInstance f in elems)
                    {
                        switch (f.StructuralType)
                        {

                            case StructuralType.Column:

                                curve = RevitModel.Columns.Get(uidoc, level, f);
                                break;

                            case StructuralType.Beam:

                                curve = RevitModel.Beams.Get(uidoc, level, f);
                                break;

                            case StructuralType.Brace:

                                curve = RevitModel.Braces.Get(uidoc, level, f);
                                break;
                        }

                    }
                }
                else
                {
                    FilteredElementCollector w_collector = new FilteredElementCollector(uidoc, selelementsid);
                    ICollection<Element> collection = w_collector.OfClass(typeof(Wall)).ToElements();

                    foreach (Element e in collection)
                    {
                        Wall wall = e as Wall;

                        if (null != wall)
                        {
                            try
                            {
                                curve = RevitModel.Walls.Get(uidoc, level, wall);
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return curve;
        }



        public static FamilyInstance GetFamilyInstance(Document uidoc, Dictionary<ElementId, double> level, ElementId id)
        {
            Options opt = new Options();

            try
            {
                Element g1 = uidoc.GetElement(id);
                GeometryElement geomElem = g1.get_Geometry(opt);

                List<ElementId> selelementsid = new List<ElementId>();
                selelementsid.Add(g1.Id);

                FilteredElementCollector collector = new FilteredElementCollector(uidoc, selelementsid);
                ElementClassFilter famInstFilter = new ElementClassFilter(typeof(FamilyInstance));
                ICollection<Element> elems = collector.WherePasses(famInstFilter).ToElements();

                if (elems.Count > 0)
                {
                    foreach (FamilyInstance f in elems)
                    {
                        return f;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return null;
        }
    }
}
