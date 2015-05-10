using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using EditElements.RevitModel;
using EditElements.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EditElements
{
    public class RevitIntersect
    {
        public static Result Do(UIApplication uiapp, Dictionary<ElementId, double> level)
        {
            UIDocument active_uidoc = uiapp.ActiveUIDocument;
            Document uidoc = active_uidoc.Document;

            List<XYZ> points = new List<XYZ>();
            List<Curve> SplitCurves = new List<Curve>();

            List<double> _elevations = RevitModel.LevelElevation.GetElevations(uidoc);

            foreach (ElementId e1 in GlobalVariables.SplitObjects)
            {
                Curve c1 = Elements.GetCurve(uidoc, level, e1);

                if (null != c1)
                {
                    SplitCurves.Add(c1);

                    foreach (ElementId e2 in GlobalVariables.CutObjects)
                    {
                        Curve c2 = Elements.GetCurve(uidoc, level, e2);

                        if (null != c2)
                        {
                            XYZ intersection = GetIntersection(c1, c2);

                            if (intersection != null && !PointAlreadyInList.Check (points, intersection))
                            {
                                points.Add(intersection);
                            }                            
                        }
                    }
                }
            }
            
            foreach (ElementId e1 in GlobalVariables.SplitObjects)
            {
                Curve c = Elements.GetCurve(uidoc, level, e1);
                FamilyInstance f = Elements.GetFamilyInstance(uidoc, level, e1);
                List<Curve> newCurves = ElementSplitter.Do(c, points);

                ICollection<ElementId> newElements = null;

                for (int i = 0;i<newCurves.Count;i++)
                {

                    if (i != 0)     // First Element different - just change endpoint
                                    // All other make copy first
                    {
                        Transaction move_element = new Transaction(uidoc, "Copy element.");
                        move_element.Start();

                        XYZ trans = newCurves[i].GetEndPoint(0) - c.GetEndPoint(0);
                        newElements = Elements.Transform(uidoc, e1, trans);

                        if (TransactionStatus.Committed != move_element.Commit())
                        {
                            move_element.RollBack();
                        }

                        foreach (ElementId id in newElements)
                        {

                            Transaction trans_element = new Transaction(uidoc, "Transform element.");
                            trans_element.Start();

                            FamilyInstance newf = Elements.GetFamilyInstance(uidoc, level, id);
                            Elements.ChangeEndPoint(uidoc, newCurves[i], newf, level, _elevations);

                            if (TransactionStatus.Committed != trans_element.Commit())
                            {
                                trans_element.RollBack();
                            }
                        }    
                    }
                    else
                    {
                        Transaction trans_element = new Transaction(uidoc, "Transform element.");
                        trans_element.Start();

                        Elements.ChangeEndPoint(uidoc, newCurves[i], f, level, _elevations);

                        if (TransactionStatus.Committed != trans_element.Commit())
                        {
                            trans_element.RollBack();
                        }
                    }
                }
            }

            return Result.Succeeded;
        }


        public static XYZ GetIntersection(Curve c1, Curve c2)
        {
            IntersectionResultArray results;

            SetComparisonResult result = c1.Intersect(c2, out results);

            if (result != SetComparisonResult.Overlap)

                return null;
            //    throw new InvalidOperationException(
            //      "Input lines did not intersect.");
            
            if (results == null || results.Size != 1)

                return null;
                //throw new InvalidOperationException(
                 // "Could not extract line intersection point.");

            IntersectionResult iResult = results.get_Item(0);

            return iResult.XYZPoint;
        }
    }
}
