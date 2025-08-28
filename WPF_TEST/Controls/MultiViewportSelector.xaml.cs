using System;
using System.Collections.Generic;
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

namespace WPF_TEST.Controls
{
    /// <summary>
    /// Interaction logic for MultiViewportSelector.xaml
    /// </summary>
    public partial class MultiViewportSelector : UserControl
    {
        public MultiViewportSelector()
        {
            InitializeComponent();
            CreateLayout(4, 4); // 기본은 4x4 그리드
            SelectLayout(2, 2); // 기본 선택은 2x2
        }

        public int SelectedRows { get; private set; } = 2;
        public int SelectedColumns { get; private set; } = 2;

        private Border[,] _cells;

        public event Action<int, int>? LayoutSelected;

        private void CreateLayout(int rows, int columns)
        {
            GridLayout.Rows = rows;
            GridLayout.Columns = columns;
            GridLayout.Children.Clear();

            GridLayout.LostFocus += (s, e) =>
            {
                GridLayout.Visibility = Visibility.Collapsed;
            };

            _cells = new Border[rows, columns];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    var cell = new Border
                    {
                        Width = 20,
                        Height = 20,
                        BorderBrush = Brushes.Gray,
                        BorderThickness = new Thickness(0.5),
                        Margin = new Thickness(1),
                        Background = Brushes.Gray
                    };

                    int row = r, col = c;

                    cell.MouseEnter += (s, e) =>
                    {
                        SelectLayout(row + 1, col + 1);
                        LayoutSelected?.Invoke(SelectedRows, SelectedColumns);
                    };

                    cell.MouseLeftButtonDown += (s, e) =>
                    {
                        SelectLayout(row + 1, col + 1);
                        LayoutSelected?.Invoke(SelectedRows, SelectedColumns);
                        GridLayout.Visibility = Visibility.Collapsed;
                    };



                    _cells[r, c] = cell;
                    GridLayout.Children.Add(cell);
                }
            }
        }

        private void SelectLayout(int rows, int cols)
        {
            SelectedRows = rows;
            SelectedColumns = cols;

            for (int r = 0; r < _cells.GetLength(0); r++)
            {
                for (int c = 0; c < _cells.GetLength(1); c++)
                {
                    var cell = _cells[r, c];
                    if (r < rows && c < cols)
                    {
                        cell.BorderBrush = Brushes.Orange;
                        cell.BorderThickness = new Thickness(1);
                        cell.Background = Brushes.Orange;
                    }
                    else
                    {
                        cell.BorderBrush = Brushes.Gray;
                        cell.BorderThickness = new Thickness(0.5);
                        cell.Background = Brushes.Gray;
                    }
                }
            }

            LabelLayout.Text = $"{rows} X {cols} Layout";
        }

        // 외부에서 호출: 원하는 최대 Rows, Columns 생성
        public void ConfigureGrid(int maxRows, int maxCols)
        {
            CreateLayout(maxRows, maxCols);
            SelectLayout(SelectedRows, SelectedColumns); // 유지
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (GridLayout.Visibility == Visibility.Collapsed)
            {
                GridLayout.Visibility = Visibility.Visible;
            }
            else
            {
                GridLayout.Visibility = Visibility.Collapsed;
            }
        }
    }
}
