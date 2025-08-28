using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
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
                using (var path = new GraphicsPath())
                {
                    // DPI 스케일을 고려한 위치와 크기 계산
                    float x = 0;
                    float y = 0;
                    float width = (float)(500 * scaleX);
                    float height = (float)(500 * scaleY);

                    path.AddEllipse(x, y, width, height);

                    using (var region = new Region(path))
                    using (var graphics = Graphics.FromHwnd(hwnd))
                    {
                        var hRgn = region.GetHrgn(graphics);

                        var blurBehind = new DWM_BLURBEHIND
                        {
                            dwFlags = DwmBlurBehindDwFlags.DWM_BB_ENABLE | DwmBlurBehindDwFlags.DWM_BB_BLURREGION | DwmBlurBehindDwFlags.DWM_BB_TRANSITIONONMAXIMIZED,
                            fEnable = true,
                            hRgnBlur = hRgn,
                            fTransitionOnMaximized = true
                        };

                        DwmEnableBlurBehindWindow(hwnd, ref blurBehind);

                        // 리전 핸들 해제
                        region.ReleaseHrgn(hRgn);
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
