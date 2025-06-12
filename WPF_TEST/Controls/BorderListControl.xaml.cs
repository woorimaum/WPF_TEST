using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF_TEST.Controls
{
    public partial class BorderListControl : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                nameof(ItemsSource),
                typeof(IEnumerable),
                typeof(BorderListControl),
                new PropertyMetadata(null, OnItemsSourceChanged));

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public BorderListControl()
        {
            InitializeComponent();
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BorderListControl control)
            {
                control.BorderItemsControl.ItemsSource = e.NewValue as IEnumerable;
            }
        }
    }
} 