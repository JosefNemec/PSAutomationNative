using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace PSNativeAutomation.Commands
{
    [Cmdlet(VerbsCommon.Set, "UIValue")]
    public class SetValueCommand : ControlActionBase
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Value
        {
            get; set;
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var pattern = Element.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
            pattern.SetValue(Value);
        }
    }
}
