using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Windows;

namespace WPF_TEST
{
    internal static class NativeMethods
    {
        public delegate bool MonitorEnumProc(IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lParam);

        public enum DpiType
        {
            EFFECTIVE = 0,
            ANGULAR = 1,
            RAW = 2
        }

        public enum SystemMetric
        {
            SM_CXSCREEN = 0,
            SM_CYSCREEN = 1,
            SM_XVIRTUALSCREEN = 76,
            SM_YVIRTUALSCREEN = 77,
            SM_CXVIRTUALSCREEN = 78,
            SM_CYVIRTUALSCREEN = 79,
            SM_CMONITORS = 80
        }

        public enum SPI : uint
        {
            /// <summary>
            /// Retrieves the size of the work area on the primary display monitor. The work area is the portion of the screen not obscured
            /// by the system taskbar or by application desktop toolbars. The pvParam parameter must point to a RECT structure that receives
            /// the coordinates of the work area, expressed in virtual screen coordinates.
            /// To get the work area of a monitor other than the primary display monitor, call the GetMonitorInfo function.
            /// </summary>
            SPI_GETWORKAREA = 0x0030
        }

        [Flags]
        public enum SPIF
        {
            None = 0x00,

            /// <summary>Writes the new system-wide parameter setting to the user profile.</summary>
            SPIF_UPDATEINIFILE = 0x01,

            /// <summary>Broadcasts the WM_SETTINGCHANGE message after updating the user profile.</summary>
            SPIF_SENDCHANGE = 0x02,

            /// <summary>Same as SPIF_SENDCHANGE.</summary>
            SPIF_SENDWININICHANGE = 0x02
        }

        public enum MonitorDefault
        {
            /// <summary>If the point is not contained within any display monitor, return a handle to the display monitor that is nearest to the point.</summary>
            MONITOR_DEFAULTTONEAREST = 0x00000002,

            /// <summary>If the point is not contained within any display monitor, return NULL.</summary>
            MONITOR_DEFAULTTONULL = 0x00000000,

            /// <summary>If the point is not contained within any display monitor, return a handle to the primary display monitor.</summary>
            MONITOR_DEFAULTTOPRIMARY = 0x00000001
        }

        public enum D2D1_FACTORY_TYPE
        {
            D2D1_FACTORY_TYPE_SINGLE_THREADED = 0,
            D2D1_FACTORY_TYPE_MULTI_THREADED = 1,
        }

        public static readonly HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);

        [DllImport("shcore.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr GetDpiForMonitor([In] IntPtr hmonitor, [In] DpiType dpiType, [Out] out uint dpiX, [Out] out uint dpiY);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool GetMonitorInfo(HandleRef hmonitor, [In][Out] MONITORINFOEX info);

        [DllImport("user32.dll", ExactSpelling = true)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool EnumDisplayMonitors(HandleRef hdc, COMRECT rcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

        [DllImport("user32.dll", ExactSpelling = true)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr MonitorFromWindow(HandleRef handle, int flags);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern int GetSystemMetrics(SystemMetric nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool SystemParametersInfo(SPI nAction, int nParam, ref RECT rc, SPIF nUpdate);

        [DllImport("user32.dll", ExactSpelling = true)]
        [ResourceExposure(ResourceScope.None)]
        public static extern IntPtr MonitorFromPoint(POINTSTRUCT pt, MonitorDefault flags);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [ResourceExposure(ResourceScope.None)]
        public static extern bool GetCursorPos([In][Out] POINT pt);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool IsProcessDPIAware();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("d2d1.dll")]
        public static extern int D2D1CreateFactory(D2D1_FACTORY_TYPE factoryType, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, IntPtr pFactoryOptions, out ID2D1Factory ppIFactory);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

        public static class WS
        {
            public static readonly long
            WS_BORDER = 0x00800000L,
            WS_CAPTION = 0x00C00000L,
            WS_CHILD = 0x40000000L,
            WS_CHILDWINDOW = 0x40000000L,
            WS_CLIPCHILDREN = 0x02000000L,
            WS_CLIPSIBLINGS = 0x04000000L,
            WS_DISABLED = 0x08000000L,
            WS_DLGFRAME = 0x00400000L,
            WS_GROUP = 0x00020000L,
            WS_HSCROLL = 0x00100000L,
            WS_ICONIC = 0x20000000L,
            WS_MAXIMIZE = 0x01000000L,
            WS_MAXIMIZEBOX = 0x00010000L,
            WS_MINIMIZE = 0x20000000L,
            WS_MINIMIZEBOX = 0x00020000L,
            WS_OVERLAPPED = 0x00000000L,
            WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
            WS_POPUP = 0x80000000L,
            WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,
            WS_SIZEBOX = 0x00040000L,
            WS_SYSMENU = 0x00080000L,
            WS_TABSTOP = 0x00010000L,
            WS_THICKFRAME = 0x00040000L,
            WS_TILED = 0x00000000L,
            WS_TILEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
            WS_VISIBLE = 0x10000000L,
            WS_VSCROLL = 0x00200000L;
        }

        public static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
            {
                return NativeMethods.SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            }
            else
            {
                return new IntPtr(NativeMethods.SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public RECT(Rect r)
            {
                left = (int)r.Left;
                top = (int)r.Top;
                right = (int)r.Right;
                bottom = (int)r.Bottom;
            }

            public static RECT FromXYWH(int x, int y, int width, int height)
            {
                return new RECT(x, y, x + width, y + height);
            }

            public System.Windows.Size Size => new System.Windows.Size(right - left, bottom - top);
        }

        // use this in cases where the Native API takes a POINT not a POINT*
        // classes marshal by ref.
        [StructLayout(LayoutKind.Sequential)]
        public struct POINTSTRUCT
        {
            public int x;
            public int y;

            public POINTSTRUCT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int x;
            public int y;

            public POINT()
            {
            }

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

#if DEBUG

            public override string ToString()
            {
                return "{x=" + x + ", y=" + y + "}";
            }

#endif
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        public class MONITORINFOEX
        {
            internal int cbSize = Marshal.SizeOf(typeof(MONITORINFOEX));

            internal RECT rcMonitor = new RECT();
            internal RECT rcWork = new RECT();
            internal int dwFlags = 0;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            internal char[] szDevice = new char[32];
        }

        [StructLayout(LayoutKind.Sequential)]
        public class COMRECT
        {
            public int bottom;
            public int left;
            public int right;
            public int top;

            public COMRECT()
            {
            }

            public COMRECT(Rect r)
            {
                left = (int)r.X;
                top = (int)r.Y;
                right = (int)r.Right;
                bottom = (int)r.Bottom;
            }

            public COMRECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public static COMRECT FromXYWH(int x, int y, int width, int height)
            {
                return new COMRECT(x, y, x + width, y + height);
            }

            public override string ToString()
            {
                return "Left = " + left + " Top " + top + " Right = " + right + " Bottom = " + bottom;
            }
        }

        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("06152247-6f50-465a-9245-118bfd3b6007")]
        internal interface ID2D1Factory
        {
            int ReloadSystemMetrics();

            [PreserveSig]
            void GetDesktopDpi(out float dpiX, out float dpiY);

            // the rest is not implemented as we don't need it
        }


        #region BlurEffect

        internal enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
            ACCENT_INVALID_STATE = 5
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct AccentPolicy
        {
            public AccentState AccentState;
            public uint AccentFlags;
            public uint GradientColor;
            public uint AnimationId;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }
        internal enum WindowCompositionAttribute
        {
            WCA_ACCENT_POLICY = 19
        }

        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        #endregion

        #region GlassWindow

        [StructLayout(LayoutKind.Sequential)]
        public struct DWM_BLURBEHIND
        {
            public DwmBlurBehindDwFlags dwFlags;
            public bool fEnable;
            public IntPtr hRgnBlur;
            public bool fTransitionOnMaximized;
        }

        [Flags]
        public enum DwmBlurBehindDwFlags
        {
            DWM_BB_ENABLE = 1,
            DWM_BB_BLURREGION = 2,
            DWM_BB_TRANSITIONONMAXIMIZED = 4
        }

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmEnableBlurBehindWindow(IntPtr hwnd, ref DWM_BLURBEHIND blurBehind);

        #endregion

        #region Windows 11 CornerRadius

        // The enum flag for DwmSetWindowAttribute's second parameter, which tells the function what attribute to set.
        // Copied from dwmapi.h
        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33
        }

        // The DWM_WINDOW_CORNER_PREFERENCE enum for DwmSetWindowAttribute's third parameter, which tells the function
        // what value of the enum to set.
        // Copied from dwmapi.h
        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0, // 창 모서리를 둥글게 처리할지 여부를 시스템에서 결정하도록 합니다. ??
            DWMWCP_DONOTROUND = 1, 
            DWMWCP_ROUND = 2, 
            DWMWCP_ROUNDSMALL = 3 // 창 모서리를 작게 둥글게 처리하도록 합니다.
        }

        // Import dwmapi.dll and define DwmSetWindowAttribute in C# corresponding to the native function.
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern void DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute, uint cbAttribute);

        #endregion
    }
}
