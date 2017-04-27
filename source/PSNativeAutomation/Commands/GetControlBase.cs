using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Windows.Automation;

namespace PSNativeAutomation.Commands
{
    public class GetControlBase : PSCmdlet
    {
        [Parameter()]
        public virtual AutomationElement Parent
        {
            get; set;
        }

        [Parameter(Position = 0)]
        [Alias("AutoID")]
        public string AutomationId
        {
            get; set;
        }

        [Parameter()]
        public TreeScope Scope
        {
            get; set;
        } = TreeScope.Descendants;

        [Parameter()]
        public SwitchParameter First
        {
            get; set;
        }

        [Parameter()]
        public SwitchParameter CaseSensitive
        {
            get; set;
        }

        [Parameter()]
        public string Class
        {
            get; set;
        }

        [Parameter()]
        public string Name
        {
            get; set;
        }

        [Parameter()]
        public string ControlType
        {
            get; set;
        }

    }
}
