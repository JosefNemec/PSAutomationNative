using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Threading;
using System.Windows.Automation;

namespace PSNativeAutomation.Commands
{
    [Cmdlet(VerbsCommon.Remove, "UISelection")]
    public class RemoveSelectionCommand : ControlActionBase
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var pattern = Element.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
            pattern.RemoveFromSelection();
        }
    }
}
