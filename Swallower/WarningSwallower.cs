using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditElements.Swallower
{
    public class WarningSwallower : IFailuresPreprocessor
    {
        public FailureProcessingResult PreprocessFailures(FailuresAccessor a)
        {
            // inside event handler, get all warnings

            IList<FailureMessageAccessor> failures = a.GetFailureMessages();

            foreach (FailureMessageAccessor f in failures)
            {
                switch (f.GetDescriptionText())
                {
                    // Slightly off axis error gets ignored
                    case "Element is slightly off axis and may cause inaccuracies.":

                        break;

                    case "Beam or Brace is slightly off axis and may cause inaccuracies.":
                        break;

                    default:

                        //GlobalVariables.WarningList.Add(new WarningEntry(a.GetTransactionName(), f.GetDescriptionText(), f.GetFailingElementIds()));
                        break;
                }
                a.DeleteWarning(f);
            }
            return FailureProcessingResult.Continue;
        }
    }
}
