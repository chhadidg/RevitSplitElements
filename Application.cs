//
// (C) Copyright 2003-2013 by Autodesk, Inc. All rights reserved.
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.

//
// AUTODESK PROVIDES THIS PROGRAM 'AS IS' AND WITH ALL ITS FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE. AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is subject to
// restrictions set forth in FAR 52.227-19 (Commercial Computer
// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
// (Rights in Technical Data and Computer Software), as applicable. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Autodesk;
using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using System.Reflection;

using adWin = Autodesk.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;
using EditElements.Utils;

namespace EditElements
{
    /// <summary>
    /// Implements the Revit add-in interface IExternalApplication
    /// </summary>
    public class Application : IExternalApplication
    {
        String SystemTabId = "\u03C03d";
        String SystemPanelId = "\u03C03d";

        String ApiTabName = "New Tab";
        String ApiPanelName = "New Panel";
        String ApiButtonName = "New Button";

        // class instance
        internal static Application thisApp = null;
        // ModelessForm instance

        #region IExternalApplication Members
        /// <summary>
        /// Implements the OnShutdown event
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        /// 

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        /// <summary>
        /// Implements the OnStartup event
        /// </summary>
        /// <param name="uiConApp"></param>
        /// <returns></returns>
        public Result OnStartup(UIControlledApplication uiConApp)
        {
            thisApp = this;  // static access to this application instance

            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            string bitmapFolder = Path.GetDirectoryName(thisAssemblyPath) + "\\icons";

            // Create New Ribbon Tab - SOM
            // If exists, throw Exception
            try
            {
                uiConApp.CreateRibbonTab(SystemPanelId);
            }
            catch
            {
                //MessageBox.Show(e.ToString());
            }

            try
            {
                Autodesk.Revit.UI.RibbonPanel Edit_RibbonPanel = uiConApp.CreateRibbonPanel(SystemPanelId, "Edit");
                // Create Buttons for Ribbon Panel
                PushButtonData IntersectButtonData = new PushButtonData("Intersect", "Intersect\n Elements ", thisAssemblyPath, "EditElements.IntersectDialog");
                
                // Bind Buttons to Ribbon Panel
                PushButton IntersectButton = Edit_RibbonPanel.AddItem(IntersectButtonData) as PushButton;

                IntersectButton.ToolTip = "Intersect Elements.";
                IntersectButton.Image = new BitmapImage(new Uri(Path.Combine(bitmapFolder, "EasyRevit_Intersect_24_new.png"), UriKind.Absolute));
                IntersectButton.LargeImage = new BitmapImage(new Uri(Path.Combine(bitmapFolder, "EasyRevit_Intersect_24_new.png"), UriKind.Absolute));
                IntersectButton.ToolTipImage = new BitmapImage(new Uri(Path.Combine(bitmapFolder, "EasyRevit_Intersect_24_new.png"), UriKind.Absolute));

                Edit_RibbonPanel.AddSeparator();

                // Create Buttons for Ribbon Panel
                PushButtonData SplitButtonData = new PushButtonData("Split", "Split\n Elements ", thisAssemblyPath, "EditElements.SplitDialog");

                // Bind Buttons to Ribbon Panel
                PushButton SplitButton = Edit_RibbonPanel.AddItem(SplitButtonData) as PushButton;
                SplitButton.ToolTip = "Split Elements by cutting elements.";
                SplitButton.Image = new BitmapImage(new Uri(Path.Combine(bitmapFolder, "EasyRevit_Split_24_new.png"), UriKind.Absolute));
                SplitButton.LargeImage = new BitmapImage(new Uri(Path.Combine(bitmapFolder, "EasyRevit_Split_24_new.png"), UriKind.Absolute));
                SplitButton.ToolTipImage = new BitmapImage(new Uri(Path.Combine(bitmapFolder, "EasyRevit_Split_24_new.png"), UriKind.Absolute));

                // Subscribe to the "ApplicationInitialized" event 
                // then continue from there once it is fired.
                // This is to ensure that the ribbon is fully 
                // populated before we mess with it.
                uiConApp.ControlledApplication.ApplicationInitialized += OnApplicationInitialized;

                return Result.Succeeded;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return Result.Succeeded;
        }

        void OnApplicationInitialized(object sender, Autodesk.Revit.DB.Events.ApplicationInitializedEventArgs e)
        {
            // Find a system ribbon tab and panel to house 
            // our API items
            // Also find our API tab, panel and button within 
            // the Autodesk.Windows.RibbonControl context

            adWin.RibbonControl adWinRibbon = adWin.ComponentManager.Ribbon;

            adWin.RibbonTab adWinSysTab = null;
            adWin.RibbonPanel adWinSysPanel = null;

            adWin.RibbonTab adWinApiTab = null;
            adWin.RibbonPanel adWinApiPanel = null;
            adWin.RibbonItem adWinApiItem = null;

            foreach (adWin.RibbonTab ribbonTab in adWinRibbon.Tabs)
            {
                // Look for the specified system tab

                if (ribbonTab.Id == SystemTabId)
                {
                    adWinSysTab = ribbonTab;

                    foreach (adWin.RibbonPanel ribbonPanel in ribbonTab.Panels)
                    {
                        // Look for the specified panel 
                        // within the system tab

                        if (ribbonPanel.Source.Id == SystemPanelId)
                        {
                            adWinSysPanel = ribbonPanel;
                        }
                    }
                }
                else
                {
                    // Look for our API tab

                    if (ribbonTab.Id == ApiTabName)
                    {
                        adWinApiTab = ribbonTab;

                        foreach (adWin.RibbonPanel ribbonPanel in ribbonTab.Panels)
                        {
                            // Look for our API panel.              

                            // The Source.Id property of an API created 
                            // ribbon panel has the following format: 
                            // CustomCtrl_%[TabName]%[PanelName]
                            // Where PanelName correlates with the string 
                            // entered as the name of the panel at creation
                            // The Source.AutomationName property can also 
                            // be used as it is also a direct correlation 
                            // of the panel name, but without all the cruft
                            // Be sure to include any new line characters 
                            // (\n) used for the panel name at creation as 
                            // they still form part of the Id & AutomationName

                            //if(ribbonPanel.Source.AutomationName 
                            //  == ApiPanelName) // Alternative method

                            if (ribbonPanel.Source.Id == "CustomCtrl_%" + ApiTabName + "%" + ApiPanelName)
                            {
                                adWinApiPanel = ribbonPanel;

                                foreach (adWin.RibbonItem ribbonItem in ribbonPanel.Source.Items)
                                {
                                    // Look for our command button

                                    // The Id property of an API created ribbon 
                                    // item has the following format: 
                                    // CustomCtrl_%CustomCtrl_%[TabName]%[PanelName]%[ItemName]
                                    // Where ItemName correlates with the string 
                                    // entered as the first parameter (name) 
                                    // of the PushButtonData() constructor
                                    // While AutomationName correlates with 
                                    // the string entered as the second 
                                    // parameter (text) of the PushButtonData() 
                                    // constructor
                                    // Be sure to include any new line 
                                    // characters (\n) used for the button 
                                    // name and text at creation as they 
                                    // still form part of the ItemName 
                                    // & AutomationName

                                    //if(ribbonItem.AutomationName 
                                    //  == ApiButtonText) // alternative method

                                    if (ribbonItem.Id == "CustomCtrl_%CustomCtrl_%" + ApiTabName + "%" + ApiPanelName + "%" + ApiButtonName)
                                    {
                                        adWinApiItem = ribbonItem;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Make sure we got everything we need

            if (adWinSysTab != null
              && adWinSysPanel != null
              && adWinApiTab != null
              && adWinApiPanel != null
              && adWinApiItem != null)
            {
                // First we'll add the whole panel including 
                // the button to the system tab

                adWinSysTab.Panels.Add(adWinApiPanel);

                // now lets also add the button itself 
                // to a system panel

                adWinSysPanel.Source.Items.Add(adWinApiItem);

                // Remove panel from original API tab
                // It can also be left there if needed, 
                // there doesn't seem to be any problems with
                // duplicate panels / buttons on seperate tabs 
                // / panels respectively

                adWinApiTab.Panels.Remove(adWinApiPanel);

                // Remove our original API tab from the ribbon

                adWinRibbon.Tabs.Remove(adWinApiTab);
            }

            // A new panel should now be added to the 
            // specified system tab. Its command buttons 
            // will behave as they normally would, including 
            // API access and ExternalCommandAvailability tests. 
            // There will also be a second copy of the command 
            // button from the panel added to the specified 
            // system panel.

        }
        #endregion
    }
}
