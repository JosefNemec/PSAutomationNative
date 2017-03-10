using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
using System.Drawing;
using PSNativeAutomation.NativeTypes;

namespace PSNativeAutomation
{
    public static class NativeMethods
    {
        public static int MakeLong(int HiWord, int LoWord)
        {
            return (HiWord << 16) | (LoWord & 0xffff);
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("User32", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, WM Msg, int wParam, int lParam);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("User32", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, WM Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);      
    }
}
