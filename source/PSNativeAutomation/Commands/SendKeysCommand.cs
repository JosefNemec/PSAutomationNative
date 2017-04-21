using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Management.Automation;
using System.Threading;
using System.Windows.Forms;

namespace PSNativeAutomation.Commands
{
    [Cmdlet(VerbsCommunications.Send, "UIKeys")]
    public class SendKeysCommand : ControlActionBase
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Keys
        {
            get; set;
        }

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

            // Focus sometimes takes a while to register properly
            Thread.Sleep(200);

            // TODO: keys doc https://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys(v=vs.110).aspx
            SendKeys.SendWait(Keys);
        }
    }
}
