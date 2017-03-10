using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PSNativeAutomation.NativeTypes
{
    [Flags]
    public enum MK : uint
    {
        LButton = 0x1,
        RButton = 0x2,
        Shift = 0x4,
        Control = 0x8,
        MButton = 0x10,
        XButton1 = 0x20,
        XButton2 = 0x40
    }

    [Flags]
    public enum WM : uint
    {
        LeftButtonDown = 0x201,
        LeftButtonUp = 0x202,
        LeftButtonDoubleClick = 0x203,

        RightButtonDown = 0x204,
        RightButtonUp = 0x205,
        RightButtonDoubleClick = 0x206,

        MiddleButtonDown = 0x207,
        MiddleButtonUp = 0x208,
        MiddleButtonDoubleClick = 0x209,

        XButtonDoubleClick = 0x20D,
        XButtonDown = 0x20B,
        XButtonUp = 0x20C,

        KeyDown = 0x100,
        KeyFirst = 0x100,
        KeyLast = 0x108,
        KeyUp = 0x101,

        NonClientHitTest = 0x084,
        NonClientLeftButtonDown = 0x0A1,
        NonClientLeftButtonUp = 0x0A2,
        NonClientLeftButtonDoubleClick = 0x0A3,

        NonClientRightButtonDown = 0x0A4,
        NonClientRightButtonUp = 0x0A5,
        NonClientRightButtonDoubleClick = 0x0A6,

        NonClientMiddleButtonDown = 0x0A7,
        NonClientMiddleButtonUp = 0x0A8,
        NonClientMiddleButtonDoubleClick = 0x0A9,

        NonClientXButtonDown = 0x0AB,
        NonClientXButtonUp = 0x0AC,
        NonClientXButtonDoubleClick = 0x0AD,

        Activate = 0x006,
        ActivateApp = 0x01C,
        SysCommand = 0x112,
        GetText = 0x00D,
        GetTextLength = 0x00E,
    }
}
