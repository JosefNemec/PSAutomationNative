using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace PSNativeAutomation.Commands
{
    [Cmdlet(VerbsCommon.Get, "UIWindowState")]
    public class GetWindowState : ControlActionBase
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var windowPattern = Element.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern;
            WriteObject(windowPattern.Current.WindowVisualState);
        }
    }
}
