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
        public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
        {
            // inside event handler, get all warnings

            IList<FailureMessageAccessor> fmas = failuresAccessor.GetFailureMessages();

            if (fmas.Count == 0)
            { return FailureProcessingResult.Continue; }

            foreach (FailureMessageAccessor fma in fmas)
            {
                FailureSeverity s = fma.GetSeverity();
                if (s == FailureSeverity.Warning)
                {
                    failuresAccessor.DeleteWarning(fma);
                }
                else if (s == FailureSeverity.Error)
                {
                    //FailureDefinitionId id = fma.GetFailureDefinitionId();

                    //if (id == BuiltInFailures.JoinElementsFailures.)
                    //{
                    // only default option being choosen, 
                    // not good enough!
                    //failuresAccessor.ResolveFailure(fma);
                    //}
                    //return FailureProcessingResult.ProceedWithRollBack;
                }
                /*
                switch (s)
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
                */
            }
            return FailureProcessingResult.Continue;
        }
    }
}
