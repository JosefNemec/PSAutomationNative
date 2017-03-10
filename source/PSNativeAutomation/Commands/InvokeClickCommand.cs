using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace PSNativeAutomation.Commands
{
    [Cmdlet(VerbsLifecycle.Invoke, "UIClick")]
    public class InvokeClickCommand : InvokeBaseCommand
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var pattern = (InvokePattern)Element.GetCurrentPattern(InvokePattern.Pattern);

            try
            {
                pattern.Invoke();
            }
            catch
            {
                IntPtr ctrlHandle = (IntPtr)Element.Current.NativeWindowHandle;
                NativeMethods.SetForegroundWindow(ctrlHandle);
                NativeMethods.PostMessage(ctrlHandle, (NativeTypes.WM)(NativeTypes.WM.LeftButtonDown), (int)NativeTypes.MK.LButton, NativeMethods.MakeLong(1, 1));
                NativeMethods.PostMessage(ctrlHandle, NativeTypes.WM.LeftButtonUp, 0, NativeMethods.MakeLong(1, 1));
            }
        }
    }
}
