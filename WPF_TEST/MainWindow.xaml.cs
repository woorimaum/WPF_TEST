using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_TEST.ViewModels;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using WPF_TEST.Helpers;
using static WPF_TEST.NativeMethods;
using System.Diagnostics;

namespace WPF_TEST
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string _filename = "App.data";

        public IntPtr myHWND;
        public const int GWL_STYLE = -16;

        const int ENUM_CURRENT_SETTINGS = -1;


        public MainWindow()
        {
            InitializeComponent();            
            DataContext = App.Current.Services.GetService(typeof(MainViewModel));
            Loaded += Window_Loaded;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;

        private void CommandBinding_Executed_Minimize(object sender, ExecutedRoutedEventArgs e) => SystemCommands.MinimizeWindow((Window)this);

        private void CommandBinding_Executed_Maximize(object sender, ExecutedRoutedEventArgs e)
        {
            // MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            //Top = SystemParameters.WorkArea.Height - Height;
            //Left = SystemParameters.WorkArea.Width - Width;

            MaxHeight = SystemParameters.VirtualScreenHeight;


            WindowState = WindowState.Maximized;
        }
        

        private void CommandBinding_Executed_Restore(object sender, ExecutedRoutedEventArgs e) => SystemCommands.RestoreWindow((Window)this);


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            myHWND = new WindowInteropHelper(this).Handle;
            IntPtr myStyle = new IntPtr(WS.WS_CAPTION | WS.WS_CLIPCHILDREN | WS.WS_MINIMIZEBOX | WS.WS_MAXIMIZEBOX | WS.WS_SYSMENU | WS.WS_SIZEBOX);
            SetWindowLongPtr(new HandleRef(null, myHWND), GWL_STYLE, myStyle);


            foreach (var item in ScreenHelper.AllScreens)
            {
                var dm = new DEVMODE();
                dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                EnumDisplaySettings(item.DeviceName, ENUM_CURRENT_SETTINGS, ref dm);

                Debug.WriteLine("");
                Debug.WriteLine($"Device: {item.DeviceName}");
                Debug.WriteLine($"Real Resolution: {dm.dmPelsWidth}x{dm.dmPelsHeight}");
                Debug.WriteLine($"Virtual Resolution: {item.Bounds.Width}x{item.Bounds.Height}");
                Debug.WriteLine($"WpfBounds: {item.WpfBounds.Width}x{item.WpfBounds.Height}");
                Debug.WriteLine($"WpfWorkingArea: {item.WpfWorkingArea.Width}x{item.WpfWorkingArea.Height}");
                Debug.WriteLine($"Dpi: {VisualTreeHelper.GetDpi(this).DpiScaleX}x{VisualTreeHelper.GetDpi(this).DpiScaleY}");
                
                // VisualTreeHelper.GetDpi(this);
                Debug.WriteLine("");
            }
        }
    }
}
