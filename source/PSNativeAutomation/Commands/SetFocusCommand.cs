using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Threading;

namespace PSNativeAutomation.Commands
{
    [Cmdlet(VerbsCommon.Set, "UIFocus")]
    public class SetFocusCommand : ControlActionBase
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Element.Current.NativeWindowHandle != 0x0)
            {
                NativeMethods.SetForegroundWindow((IntPtr)Element.Current.NativeWindowHandle);
            }
            else
            {
                Element.SetFocus();
            }
        }
    }
}
