using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using static WPF_TEST.NativeMethods;

namespace WPF_TEST.Views
{
    /// <summary>
    /// Interaction logic for BlurWindow.xaml
    /// </summary>
    public partial class BlurWindow : Window
    {
        public BlurWindow()
        {
            InitializeComponent();
            // DataContext = new BlurHelper(this, AccentState.ACCENT_ENABLE_BLURBEHIND) { BlurOpacity = 100 };

            Loaded += BlurWindow_Loaded;
        }

        private void BlurWindow_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();            
        }

        internal void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(this);

            var accent = new AccentPolicy
            {
                AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND
            };

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;
            

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);

            IntPtr hWnd = new WindowInteropHelper(GetWindow(this)).EnsureHandle();            
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            DwmSetWindowAttribute(hWnd, DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE, ref preference, sizeof(uint));
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
