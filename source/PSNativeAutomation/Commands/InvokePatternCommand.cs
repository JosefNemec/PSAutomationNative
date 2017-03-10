using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace PSNativeAutomation.Commands
{
    [Cmdlet(VerbsLifecycle.Invoke, "UIInvokePattern")]
    public class InvokeInvokePatternCommand : InvokeBaseCommand
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var pattern = (InvokePattern)Element.GetCurrentPattern(InvokePattern.Pattern);
            pattern.Invoke();
        }
    }
}
