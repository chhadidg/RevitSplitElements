//
// (C) Copyright 2003-2013 by Autodesk, Inc.
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE. AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is subject to
// restrictions set forth in FAR 52.227-19 (Commercial Computer
// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
// (Rights in Technical Data and Computer Software), as applicable.
//

using System;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using EditElements.Utils;


namespace EditElements
{
    /// <summary>
    /// Implements the Revit add-in interface IExternalCommand
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]

    public class SplitDialog : IExternalCommand
    {
        /// <summary>
        /// Implement this method as an external command for Revit.
        /// </summary>
        /// <param name="commandData">An object that is passed to the external application 
        /// which contains data related to the command, 
        /// such as the application object and active view.</param>
        /// <param name="message">A message that can be set by the external application 
        /// which will be displayed if a failure or cancellation is returned by 
        /// the external command.</param>
        /// <param name="elements">A set of elements to which the external application 
        /// can add elements that are to be highlighted in case of failure or cancellation.</param>
        /// <returns>Return the status of the external command. 
        /// A result of Succeeded means that the API external method functioned as expected. 
        /// Cancelled can be used to signify that the user cancelled the external operation 
        /// at some point. Failure should be returned if the application is unable to proceed with 
        /// the operation.</returns>
        public virtual Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            GlobalVariables.MessageVerticalColumn = 0;

            try
            {
                if (GlobalVariables.SplitObjects != null)
                {
                    GlobalVariables.SplitObjects.Clear();
                }
                GlobalVariables.SplitObjects = RevitSelection.Get(uiapp, "Select objects to split");

                if (GlobalVariables.CutObjects != null)
                {
                    GlobalVariables.CutObjects.Clear();
                }
                GlobalVariables.CutObjects = RevitSelection.Get(uiapp, "Select cutting objects");

                if (GlobalVariables.CutObjects.Count > 0 && GlobalVariables.SplitObjects.Count > 0)
                {
                    Dictionary<ElementId, double> level = RevitModel.LevelElevation.CreateDic(uiapp);

                    Result r = RevitIntersect.Do(uiapp, level);
                    return r;
                }

                return Result.Failed;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }


    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]

    public class IntersectDialog : IExternalCommand
    {
        /// <summary>
        /// Implement this method as an external command for Revit.
        /// </summary>
        /// <param name="commandData">An object that is passed to the external application 
        /// which contains data related to the command, 
        /// such as the application object and active view.</param>
        /// <param name="message">A message that can be set by the external application 
        /// which will be displayed if a failure or cancellation is returned by 
        /// the external command.</param>
        /// <param name="elements">A set of elements to which the external application 
        /// can add elements that are to be highlighted in case of failure or cancellation.</param>
        /// <returns>Return the status of the external command. 
        /// A result of Succeeded means that the API external method functioned as expected. 
        /// Cancelled can be used to signify that the user cancelled the external operation 
        /// at some point. Failure should be returned if the application is unable to proceed with 
        /// the operation.</returns>
        public virtual Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            GlobalVariables.MessageVerticalColumn = 0;
            try
            {
                if (GlobalVariables.SplitObjects != null)
                {
                    GlobalVariables.SplitObjects.Clear();
                }

                if (GlobalVariables.CutObjects != null)
                {
                    GlobalVariables.CutObjects.Clear();
                }
                GlobalVariables.CutObjects = RevitSelection.Get(uiapp, "Select objects to intersect.");
                GlobalVariables.SplitObjects = GlobalVariables.CutObjects;

                if (GlobalVariables.CutObjects != null && GlobalVariables.SplitObjects != null && GlobalVariables.CutObjects.Count > 0 && GlobalVariables.SplitObjects.Count > 0)
                {
                    Dictionary<ElementId, double> level = RevitModel.LevelElevation.CreateDic(uiapp);

                    Result r = RevitIntersect.Do(uiapp, level);
                    return r;
                }

                return Result.Failed;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}

