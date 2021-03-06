﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Windows.Automation;

namespace PSNativeAutomation.Commands
{
    [Cmdlet(VerbsCommon.Get, "UIWindow")]
    public class GetWindowCommand : GetControlBase
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public override AutomationElement Parent
        {
            get; set;
        }

        [Parameter()]
        public string ProcessName
        {
            get; set;
        }

        [Parameter()]
        public uint PID
        {
            get; set;
        } = uint.MaxValue;

        public GetWindowCommand()
        {
            ControlType = "window";
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Parent == null)
            {
                Parent = AutomationElement.RootElement;
            }

            try
            {
                if (Parent == AutomationElement.RootElement)
                {
                    Scope = TreeScope.Children;
                }

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
}
