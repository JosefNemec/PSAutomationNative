using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace PSNativeAutomation.Commands
{
    public class ControlActionBase : PSCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AutomationElement Element
        {
            get; set;
        }
    }
}
