using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using PSNativeAutomation.NativeTypes;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace PSNativeAutomation.Commands
{
    [Cmdlet(VerbsLifecycle.Invoke, "UIClick")]
    public class InvokeClickCommand : ControlActionBase
    {
        [Parameter(Position = 1)]
        [Alias("X")]
        public int XOffset
        {
            get; set;
        } = 0;

        [Parameter(Position = 2)]
        [Alias("Y")]
        public int YOffset
        {
            get; set;
        } = 0;

        [Parameter(Mandatory = false)]
        public SwitchParameter RightClick;

        [Parameter(Mandatory = false)]
        public SwitchParameter MiddleClick;

        [Parameter(Mandatory = false)]
        public SwitchParameter DoubleClick;

        [Parameter(Mandatory = false)]
        public SwitchParameter Alt
        {
            get; set;
        }

        [Parameter(Mandatory = false)]
        public SwitchParameter Shift
        {
            get; set;
        }

        [Parameter(Mandatory = false)]
        public SwitchParameter Ctrl
        {
            get; set;
        }

        [Parameter(Mandatory = false)]
        public SwitchParameter Alternative
        {
            get; set;
        }

        int CalculateAbsoluteCoordinateX(int x)
        {
            return (x * 65536) / NativeMethods.GetSystemMetrics(SystemMetric.SM_CXSCREEN);
        }

        int CalculateAbsoluteCoordinateY(int y)
        {
            return (y * 65536) / NativeMethods.GetSystemMetrics(SystemMetric.SM_CYSCREEN);
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var handle = (IntPtr)Element.Current.NativeWindowHandle;

            if (Ctrl)
            {
                var keyInput = new INPUT()
                {
                    type = SendInputEventType.InputKeyboard
                };

                keyInput.mkhi.ki.wScan = (ushort)ScanCodeShort.LCONTROL;
                keyInput.mkhi.ki.wVk = (ushort)VirtualKeyShort.LCONTROL;
                NativeMethods.SendInput(1, ref keyInput, Marshal.SizeOf(new INPUT()));
            }

            if (Shift)
            {
                var keyInput = new INPUT()
                {
                    type = SendInputEventType.InputKeyboard
                };

                keyInput.mkhi.ki.wScan = (ushort)ScanCodeShort.LSHIFT;
                keyInput.mkhi.ki.wVk = (ushort)VirtualKeyShort.LSHIFT;
                NativeMethods.SendInput(1, ref keyInput, Marshal.SizeOf(new INPUT()));
            }

            if (Alt)
            {
                var keyInput = new INPUT()
                {
                    type = SendInputEventType.InputKeyboard
                };

                keyInput.mkhi.ki.wScan = (ushort)ScanCodeShort.LMENU;
                keyInput.mkhi.ki.wVk = (ushort)VirtualKeyShort.LMENU;
                NativeMethods.SendInput(1, ref keyInput, Marshal.SizeOf(new INPUT()));
            }

            try
            {
                if (handle.ToInt32() != 0x0)
                {
                    NativeMethods.SetForegroundWindow(handle);
                }
                else
                {
                    Element.SetFocus();
                }
            }
            catch
            {
                // TODO: log this
            }

            if (Alternative)
            {
                INPUT mouseInput = new INPUT()
                {
                    type = SendInputEventType.InputMouse
                };

                mouseInput.mkhi.mi.mouseData = 0;
                mouseInput.mkhi.mi.dx = CalculateAbsoluteCoordinateX(0);
                mouseInput.mkhi.mi.dy = CalculateAbsoluteCoordinateY(0);
                mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
                NativeMethods.SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

                mouseInput.mkhi.mi.dx = CalculateAbsoluteCoordinateX((int)Element.Current.BoundingRectangle.Left + XOffset);
                mouseInput.mkhi.mi.dy = CalculateAbsoluteCoordinateY((int)Element.Current.BoundingRectangle.Top + YOffset);
                mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
                NativeMethods.SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

                if (RightClick)
                {
                    mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTDOWN;
                    NativeMethods.SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                    mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTUP;
                    NativeMethods.SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                }
                else if (MiddleClick)
                {
                    mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MIDDLEDOWN;
                    NativeMethods.SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                    mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MIDDLEUP;
                    NativeMethods.SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                }
                else if (DoubleClick)
                {
                    mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
                    NativeMethods.SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                    mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
                    NativeMethods.SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                    mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
                    NativeMethods.SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                    mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
                    NativeMethods.SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                }
                else
                {
                    mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
                    NativeMethods.SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                    mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
                    NativeMethods.SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
                }
            }
            else
            {
                Cursor.Position = new Point((0), (0));
                Cursor.Position = new Point(((int)Element.Current.BoundingRectangle.Left + XOffset), ((int)Element.Current.BoundingRectangle.Top + YOffset));

                if (RightClick)
                {
                    NativeMethods.mouse_event((int)MouseEventFlags.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                    NativeMethods.mouse_event((int)MouseEventFlags.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                }
                else if (MiddleClick)
                {
                    NativeMethods.mouse_event((int)MouseEventFlags.MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
                    NativeMethods.mouse_event((int)MouseEventFlags.MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                }
                else if (DoubleClick)
                {
                    NativeMethods.mouse_event((int)MouseEventFlags.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    NativeMethods.mouse_event((int)MouseEventFlags.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    NativeMethods.mouse_event((int)MouseEventFlags.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    NativeMethods.mouse_event((int)MouseEventFlags.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                }
                else
                {
                    NativeMethods.mouse_event((int)MouseEventFlags.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    NativeMethods.mouse_event((int)MouseEventFlags.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                }
            }

            if (Ctrl)
            {
                var keyInput = new INPUT()
                {
                    type = SendInputEventType.InputKeyboard
                };

                keyInput.mkhi.ki.wScan = (ushort)ScanCodeShort.LCONTROL;
                keyInput.mkhi.ki.wVk = (ushort)VirtualKeyShort.LCONTROL;
                keyInput.mkhi.ki.dwFlags = (uint)(KEYEVENTF.KEYUP);
                NativeMethods.SendInput(1, ref keyInput, Marshal.SizeOf(new INPUT()));
            }

            if (Shift)
            {
                var keyInput = new INPUT()
                {
                    type = SendInputEventType.InputKeyboard
                };

                keyInput.mkhi.ki.wScan = (ushort)ScanCodeShort.LSHIFT;
                keyInput.mkhi.ki.wVk = (ushort)VirtualKeyShort.LSHIFT;
                keyInput.mkhi.ki.dwFlags = (uint)(KEYEVENTF.KEYUP);
                NativeMethods.SendInput(1, ref keyInput, Marshal.SizeOf(new INPUT()));
            }

            if (Alt)
            {
                var keyInput = new INPUT()
                {
                    type = SendInputEventType.InputKeyboard
                };

                keyInput.mkhi.ki.wScan = (ushort)ScanCodeShort.LMENU;
                keyInput.mkhi.ki.wVk = (ushort)VirtualKeyShort.LMENU;
                keyInput.mkhi.ki.dwFlags = (uint)(KEYEVENTF.KEYUP);
                NativeMethods.SendInput(1, ref keyInput, Marshal.SizeOf(new INPUT()));
            }
        }
    }
}
