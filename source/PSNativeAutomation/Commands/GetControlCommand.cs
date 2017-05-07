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
    public class GetControlCommand : GetControlBase
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
            Class = "Button";
        }
    }

    [Cmdlet(VerbsCommon.Get, "UIRadioButton")]
    public class GetRadioButtonCommand : GetControlCommand
    {
        public GetRadioButtonCommand()
        {
            Class = "RadioButton";
        }
    }

    [Cmdlet(VerbsCommon.Get, "UICheckBox")]
    public class GetCheckBoxCommand : GetControlCommand
    {
        public GetCheckBoxCommand()
        {
            Class = "CheckBox";
        }
    }

    [Cmdlet(VerbsCommon.Get, "UIEdit")]
    public class GetEditCommand : GetControlCommand
    {
        public GetEditCommand()
        {
            Class = "Edit";
        }
    }

    [Cmdlet(VerbsCommon.Get, "UILabel")]
    public class GetLabelCommand : GetControlCommand
    {
        public GetLabelCommand()
        {
            Class = "Label";
        }
    }

    [Cmdlet(VerbsCommon.Get, "UITextBlock")]
    public class GetTextBlockCommand : GetControlCommand
    {
        public GetTextBlockCommand()
        {
            Class = "TextBlock";
        }
    }

    [Cmdlet(VerbsCommon.Get, "UITextBox")]
    public class GetTextBoxCommand : GetControlCommand
    {
        public GetTextBoxCommand()
        {
            Class = "TextBox";
        }
    }

    [Cmdlet(VerbsCommon.Get, "UIListView")]
    public class GetListViewCommand : GetControlCommand
    {
        public GetListViewCommand()
        {
            Class = "ListView";
        }
    }

    [Cmdlet(VerbsCommon.Get, "UIListViewItem")]
    public class GetListViewItemCommand : GetControlCommand
    {
        public GetListViewItemCommand()
        {
            Class = "ListViewItem";
        }
    }

    [Cmdlet(VerbsCommon.Get, "UIListBox")]
    public class GetListBoxCommand : GetControlCommand
    {
        public GetListBoxCommand()
        {
            Class = "ListBox";
        }
    }

    [Cmdlet(VerbsCommon.Get, "UIListBoxItem")]
    public class GetListBoxItemCommand : GetControlCommand
    {
        public GetListBoxItemCommand()
        {
            Class = "ListBoxItem";
        }
    }

    [Cmdlet(VerbsCommon.Get, "UIMenuItem")]
    public class GetMenuItemCommand : GetControlCommand
    {
        public GetMenuItemCommand()
        {
            Class = "MenuItem";
        }
    }

    [Cmdlet(VerbsCommon.Get, "UIComboBox")]
    public class GetComboBoxCommand : GetControlCommand
    {
        public GetComboBoxCommand()
        {
            Class = "ComboBox";
        }
    }

    [Cmdlet(VerbsCommon.Get, "UIImage")]
    public class GetImageCommand : GetControlCommand
    {
        public GetImageCommand()
        {
            Class = "Image";
        }
    }

    [Cmdlet(VerbsCommon.Get, "UIContextMenu")]
    public class GetContextMenuCommand : GetControlCommand
    {
        public GetContextMenuCommand()
        {
            Class = "ContextMenu";
        }
    }

    [Cmdlet(VerbsCommon.Get, "UITabControl")]
    public class GetTabControlCommand : GetControlCommand
    {
        public GetTabControlCommand()
        {
            Class = "TabControl";
        }
    }

    [Cmdlet(VerbsCommon.Get, "UITabItem")]
    public class GetTabItemCommand : GetControlCommand
    {
        public GetTabItemCommand()
        {
            Class = "TabItem";
        }
    }

    [Cmdlet(VerbsCommon.Get, "UIText")]
    public class GetTextCommand : GetControlCommand
    {
        public GetTextCommand()
        {
            Class = "Text";
        }
    }
}
