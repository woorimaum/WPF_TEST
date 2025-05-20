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
        }

        partial void OnNameChanged(string value)
        {
            Result = "변경 완료";
        }

        #endregion
    }
}
