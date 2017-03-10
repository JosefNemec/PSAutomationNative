using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Windows.Automation;

namespace PSNativeAutomation.Commands
{
    [Cmdlet(VerbsCommon.Get, "UIDesktop")]
    public class GetUIADesktopCommand : PSCmdlet
    {
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            WriteObject(AutomationElement.RootElement);
        }
    }
}
