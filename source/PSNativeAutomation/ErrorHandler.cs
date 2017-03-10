using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PSNativeAutomation
{
    class ErrorHandler
    {

        public static void ThrowTerminatingError(ErrorRecord error, PSCmdlet cmdlet)
        {
            if (cmdlet.MyInvocation.BoundParameters.ContainsKey("ErrorAction"))
            {
                var action = (ActionPreference)cmdlet.MyInvocation.BoundParameters["ErrorAction"];
                if (action == ActionPreference.Ignore || action == ActionPreference.SilentlyContinue)
                {
                    return;
                }
            }

            cmdlet.ThrowTerminatingError(error);
        }
    }
}
