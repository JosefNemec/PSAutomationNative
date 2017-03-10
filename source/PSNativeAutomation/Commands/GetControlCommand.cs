using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Windows.Automation;

namespace PSNativeAutomation.Commands
{
    [Cmdlet(VerbsCommon.Get, "UIControl")]
    public class GetControlCommand : GetBaseControlCommand
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public override AutomationElement Parent
        {
            get; set;
        }

        [Parameter()]
        public string Value
        {
            get; set;
        }
        
        public GetControlCommand()
        {
            Scope = TreeScope.Descendants;
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                var controls = ObjectFinder.FindControls(this);
                if (controls.Count != 0)
                {
                    WriteObject(controls, true);
                }
                else
                {
                    ErrorHandler.ThrowTerminatingError(new ErrorRecord(new Exception("No control found."), "SearchError", ErrorCategory.ObjectNotFound, this), this);
                }
            }
            catch (Exception e)
            {
                ErrorHandler.ThrowTerminatingError(new ErrorRecord(e, "SearchError", ErrorCategory.NotSpecified, this), this);
                return;
            }
        }
    }

    [Cmdlet(VerbsCommon.Get, "UIButton")]
    public class GetButtonCommand : GetControlCommand
    {
        public GetButtonCommand()
        {
            ControlType = "button";
        }
    }
}
