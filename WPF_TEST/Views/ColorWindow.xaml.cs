using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static WPF_TEST.NativeMethods;

namespace WPF_TEST.Views
{
    /// <summary>
    /// Interaction logic for ColorWindow.xaml
    /// </summary>
    public partial class ColorWindow : Window
    {
        public ColorWindow()
        {

            InitializeComponent();
            Loaded += ColorWindow_Loaded;
        }

        private void ColorWindow_Loaded(object sender, RoutedEventArgs e)
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
