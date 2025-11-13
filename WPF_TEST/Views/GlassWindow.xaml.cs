using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shell;
using WPF_TEST.ViewModels;
using static WPF_TEST.NativeMethods;

namespace WPF_TEST.Views
{
    /// <summary>
    /// Interaction logic for GlassWindow.xaml
    /// </summary>
    public partial class GlassWindow : Window
    {
        public GlassWindow()
        {
            InitializeComponent();

            DataContext = App.Current.Services.GetService(typeof(MainViewModel));

            this.SourceInitialized += MainWindow_SourceInitialized;
            this.KeyDown += MainWindow_KeyDown;
            this.MouseLeftButtonDown += Grid_MouseLeftButtonDown;            
        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) this.Close();
        }

        void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            WindowInteropHelper helper = new(this);
            nint hwnd = helper.Handle;
            HwndSource src = HwndSource.FromHwnd(hwnd);

            src.CompositionTarget.BackgroundColor = Colors.Transparent;

            WindowChrome.SetWindowChrome(this, new WindowChrome
            {
                CaptionHeight = 0,
                CornerRadius = new CornerRadius(0),
                GlassFrameThickness = new Thickness(0),
                NonClientFrameEdges = NonClientFrameEdges.None,
                ResizeBorderThickness = new Thickness(0),
                UseAeroCaptionButtons = false
            });

            if (!DwmIsCompositionEnabled())
            {
                return;
            }

            try
            {
                // DPI 스케일 고려
                var dpi = VisualTreeHelper.GetDpi(this);
                var scaleX = dpi.DpiScaleX;
                var scaleY = dpi.DpiScaleY;

                // 원형 리전 생성 - DPI 스케일 적용
                int x = 0;
                int y = 0;
                int width = (int)Math.Round(500 * scaleX);
                int height = (int)Math.Round(500 * scaleY);

                IntPtr hRgn = IntPtr.Zero;

                try
                {
                    hRgn = CreateEllipticRgn(x, y, x + width, y + height);
                    if (hRgn == IntPtr.Zero)
                    {
                        throw new InvalidOperationException("CreateEllipticRgn failed.");
                    }

                    var blurBehind = new DWM_BLURBEHIND
                    {
                        dwFlags = DwmBlurBehindDwFlags.DWM_BB_ENABLE | DwmBlurBehindDwFlags.DWM_BB_BLURREGION | DwmBlurBehindDwFlags.DWM_BB_TRANSITIONONMAXIMIZED,
                        fEnable = true,
                        hRgnBlur = hRgn,
                        fTransitionOnMaximized = true
                    };

                    DwmEnableBlurBehindWindow(hwnd, ref blurBehind);
                }
                finally
                {
                    if (hRgn != IntPtr.Zero)
                    {
                        DeleteObject(hRgn);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Blur effect error: {ex.Message}");
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
