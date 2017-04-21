using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace PSNativeAutomation.Commands
{
    [Cmdlet(VerbsCommon.Get, "UIToggleState")]
    public class GetToggleStateCommand : ControlActionBase
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var pattern = Element.GetCurrentPattern(TogglePattern.Pattern) as TogglePattern;
            WriteObject(pattern.Current.ToggleState);
        }
    }
}
