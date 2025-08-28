using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_TEST.Models;

namespace WPF_TEST.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string? result;

        [ObservableProperty]
        private string userPW;

        [ObservableProperty]
        private ObservableCollection<MyDataGrid> dataGridCollection;

        [ObservableProperty]
        private MyDataGrid selectedDataGridCollection;

        [ObservableProperty]
        private int selectedDataGridIndex;

        [ObservableProperty]
        private ObservableCollection<ColorBorder> colorBorderList;

        [ObservableProperty]
        private bool _isSelectorVisible;


        public string ToggleButtonText => IsSelectorVisible ? "레이아웃 닫기" : "레이아웃 열기";

        public int SelectedRows { get; set; } = 2;
        public int SelectedColumns { get; set; } = 2;

        #endregion

        #region Commands

        #endregion

        #region Methods

        [RelayCommand]
        private void OnClick()
        {
            Views.BlurWindow blurWindow = new Views.BlurWindow();

            blurWindow.Owner = App.Current.MainWindow;
            blurWindow.Show();
        }

        [RelayCommand]
        private void OnClick2()
        {
            Views.GlassWindow glassWindow = new Views.GlassWindow();
            glassWindow.Owner = App.Current.MainWindow;
            glassWindow.Show();
        }

        [RelayCommand]
        private void OnClick3()
        {
            Views.ColorWindow colorWindow = new Views.ColorWindow();
            colorWindow.Owner = App.Current.MainWindow;
            colorWindow.DataContext = this;
            colorWindow.Show();
        }

        [RelayCommand]
        private void Maximize()
        {
            //SystemCommands.MaximizeWindow((Window)this);
        }

        [RelayCommand]
        private void Restore() 
        {
            //SystemCommands.RestoreWindow((Window)this);
        }

        [RelayCommand]
        private void OnUp()
        {
            if (DataGridCollection.First() != SelectedDataGridCollection)
            {
                SelectedDataGridCollection = DataGridCollection[DataGridCollection.IndexOf(SelectedDataGridCollection) - 1];
            }   
        }

        [RelayCommand]
        private void OnDown()
        {
            if (DataGridCollection.Last() != SelectedDataGridCollection)
            {
                SelectedDataGridCollection = DataGridCollection[DataGridCollection.IndexOf(SelectedDataGridCollection) + 1];
            }
            
        }

        [RelayCommand]
        private void OnToggleSelector()
        {
            IsSelectorVisible = !IsSelectorVisible;
        }

        public MainViewModel()
        {
            name = "초기화";            

            DataGridCollection =
            [
                new MyDataGrid() { Index = 0, Name = "aaaa", Description = "just" },
                new MyDataGrid() { Index = 1, Name = "bbbb", Description = "test" },
                new MyDataGrid() { Index = 2, Name = "cccc", Description = "this" },
                new MyDataGrid() { Index = 3, Name = "dddd", Description = "it" },

                //new MyDataGrid() { Index = 0 },
                //new MyDataGrid() { Index = 1 },
                //new MyDataGrid() { Index = 2 },
                //new MyDataGrid() { Index = 3 },
            ];

            SelectedDataGridCollection = DataGridCollection.First();
            SelectedDataGridIndex = 0;

            // 샘플 데이터 추가
            ColorBorderList = new ObservableCollection<ColorBorder>
            {
                new ColorBorder { ColorName = "춘유록색", ColorValue = "#DCEAA2" },
                new ColorBorder { ColorName = "취람색", ColorValue = "#68C7C1" },
                new ColorBorder { ColorName = "벽자색", ColorValue = "#8C9ED9" },
                new ColorBorder { ColorName = "설백색", ColorValue = "#E2E8E4" },
                new ColorBorder { ColorName = "행황색", ColorValue = "#F1A862" },
                new ColorBorder { ColorName = "청현색", ColorValue = "#566A8E" },
                new ColorBorder { ColorName = "장단색", ColorValue = "#E16350" },
                new ColorBorder { ColorName = "추향색", ColorValue = "#C19287" },
                new ColorBorder { ColorName = "백청색", ColorValue = "#4F90CC" },
            };
        }

        partial void OnNameChanged(string value)
        {
            Result = "변경 완료";
        }

        #endregion
    }
}
