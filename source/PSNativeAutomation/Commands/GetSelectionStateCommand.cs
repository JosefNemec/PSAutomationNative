using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace PSNativeAutomation.Commands
{
    [Cmdlet(VerbsCommon.Get, "UISelectionState")]
    public class GetSelectionStateCommand : ControlActionBase
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var pattern = Element.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
            WriteObject(pattern.Current.IsSelected);
        }
    }
}
