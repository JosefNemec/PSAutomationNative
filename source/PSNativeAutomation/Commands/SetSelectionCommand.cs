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
    [Cmdlet(VerbsCommon.Set, "UISelection")]
    public class SetSelectionCommand : ControlActionBase
    {
        [Parameter()]
        public SwitchParameter Add
        {
            get; set;
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var pattern = Element.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;

            if (Add.IsPresent)
            {
                pattern.AddToSelection();
            }
            else
            {
                pattern.Select();
            }
        }
    }
}
