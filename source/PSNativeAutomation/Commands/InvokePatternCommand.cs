﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace PSNativeAutomation.Commands
{
    [Cmdlet(VerbsLifecycle.Invoke, "UIInvokePattern")]
    public class InvokeInvokePatternCommand : ControlActionBase
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var pattern = Element.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            pattern.Invoke();
        }
    }
}
