using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace PSNativeAutomation.Commands
{
    public enum WindowPatten
    {
        Maximize,
        Minimize,
        Restore,
        Close
    }

    [Cmdlet(VerbsLifecycle.Invoke, "UIWindowPattern")]
    public class InvokeWindowPatternCommand : ControlActionBase
    {
        [Parameter(Mandatory = true, Position = 0)]
        public WindowPatten Pattern
        {
            get; set;
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var windowPattern = Element.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern;
            switch (Pattern)
            {
                case WindowPatten.Maximize:
                    windowPattern.SetWindowVisualState(WindowVisualState.Maximized);
                    break;

                case WindowPatten.Minimize:
                    windowPattern.SetWindowVisualState(WindowVisualState.Minimized);
                    break;

                case WindowPatten.Restore:
                    windowPattern.SetWindowVisualState(WindowVisualState.Normal);
                    break;

                case WindowPatten.Close:
                    windowPattern.Close();
                    break;
            }
        }
    }
}
