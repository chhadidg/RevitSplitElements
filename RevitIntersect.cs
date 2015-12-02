using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using EditElements.Swallower;
using EditElements.Utils;
using System;
using System.Collections.Generic;
using System.Windows;

namespace EditElements
{
    public class RevitIntersect
    {
        public static Result Do(UIApplication uiapp, Dictionary<ElementId, double> level)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            
            List<XYZ> points = new List<XYZ>();
            List<Curve> SplitCurves = new List<Curve>();

            List<double> _elevations = RevitModel.LevelElevation.GetElevations(doc);

            foreach (ElementId e1 in GlobalVariables.SplitObjects)
            {
                Curve c1 = Elements.GetCurve(doc, level, e1);

                if (null != c1)
                {
                    SplitCurves.Add(c1);

                    foreach (ElementId e2 in GlobalVariables.CutObjects)
                    {
                        Curve c2 = Elements.GetCurve(doc, level, e2);

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

            using (TransactionGroup transgroup = new TransactionGroup(doc, "Intersect Geometry"))
            {
                try
                {
                    if (transgroup.Start() != TransactionStatus.Started) return Result.Failed;

                    foreach (ElementId e1 in GlobalVariables.SplitObjects)
                    {
                        Curve c = Elements.GetCurve(doc, level, e1);
                        FamilyInstance f = Elements.GetFamilyInstance(doc, level, e1);
                        List<Curve> newCurves = ElementSplitter.Do(c, points);

                        ICollection<ElementId> newElements = null;

                        for (int i = 0; i < newCurves.Count; i++)
                        {
                            if (i != 0)     // First Element different - just change endpoint
                                            // All other make copy first
                            {
                                using (Transaction trans = new Transaction(doc, "Copy element."))
                                {
                                    try
                                    {
                                        if (trans.Start() != TransactionStatus.Started) return Result.Failed;

                                        XYZ transform = newCurves[i].GetEndPoint(0) - c.GetEndPoint(0);
                                        newElements = Elements.Transform(doc, e1, transform);

                                        if (TransactionStatus.Committed != trans.Commit())
                                        {
                                            trans.RollBack();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.ToString());
                                        trans.RollBack();
                                    }
                                }

                                foreach (ElementId id in newElements)
                                {
                                    // Change Curve
                                    using (Transaction trans = new Transaction(doc, "Transform element."))
                                    {
                                        try
                                        {
                                            if (trans.Start() != TransactionStatus.Started) return Result.Failed;

                                            FamilyInstance newf = Elements.GetFamilyInstance(doc, level, id);
                                            Elements.ChangeEndPoint(doc, newCurves[i], newf, level, _elevations);

                                            FailureHandlingOptions options = trans.GetFailureHandlingOptions();
                                            options.SetFailuresPreprocessor(new WarningSwallower());

                                            if (TransactionStatus.Committed != trans.Commit(options))
                                            {
                                                trans.RollBack();
                                            }
                                        }
                                        catch
                                        {
                                            trans.RollBack();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                using (Transaction trans = new Transaction(doc, "Transform element."))
                                {
                                    try
                                    {
                                        if (trans.Start() != TransactionStatus.Started) return Result.Failed;

                                        foreach (ElementId eId in JoinGeometryUtils.GetJoinedElements(doc, f))
                                        {
                                            JoinGeometryUtils.UnjoinGeometry(doc, doc.GetElement(e1), doc.GetElement(eId));
                                        }

                                        FailureHandlingOptions options = trans.GetFailureHandlingOptions();
                                        options.SetFailuresPreprocessor(new WarningSwallower());

                                        if (TransactionStatus.Committed != trans.Commit(options))
                                        {
                                            trans.RollBack();
                                        }
                                    }
                                    catch
                                    {
                                        trans.RollBack();
                                    }
                                }
                                using (Transaction trans = new Transaction(doc, "Transform element."))
                                {
                                    try
                                    {
                                        if (trans.Start() != TransactionStatus.Started) return Result.Failed;

                                        LocationCurve orig_location = f.Location as LocationCurve;
                                        double orig_len = orig_location.Curve.Length;

                                        double up_len = newCurves[i].Length;

                                        Elements.ChangeEndPoint(doc, newCurves[i], f, level, _elevations);

                                        LocationCurve after_location = f.Location as LocationCurve;
                                        double after_len = after_location.Curve.Length;
                                        doc.Regenerate();

                                        LocationCurve regen_location = f.Location as LocationCurve;
                                        double regen_len = regen_location.Curve.Length;

                                        uidoc.RefreshActiveView();

                                        FailureHandlingOptions options = trans.GetFailureHandlingOptions();
                                        options.SetFailuresPreprocessor(new WarningSwallower());
                                        options.SetClearAfterRollback(true);

                                        if (TransactionStatus.Committed != trans.Commit(options))
                                        {
                                            trans.RollBack();
                                            return Result.Failed;
                                        }
                                    }
                                    catch
                                    {
                                        trans.RollBack();
                                        return Result.Failed;
                                    }
                                }
                            }
                        }
                    }
                    if (transgroup.Assimilate() == TransactionStatus.Committed)
                    {
                        return Result.Succeeded;
                    }
                    else
                    {
                        return Result.Failed;
                    }
                }
                catch
                {
                    transgroup.RollBack();
                    return Result.Failed;
                }
            }            
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
