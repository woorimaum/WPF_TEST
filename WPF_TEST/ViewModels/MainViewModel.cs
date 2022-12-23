using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_TEST.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string? result;

        #endregion

        #region Commands



        #endregion

        #region Methods

        [RelayCommand]
        private void OnClick()
        {
            if (Name == "Clicked")
            {
                Name = "just Click";
            }
            else
            {
                Name = "Clicked";
            }
        }

        public MainViewModel()
        {
            name = "초기화";            
        }

        partial void OnNameChanged(string value)
        {
            Result = "변경 완료";
        }

        #endregion
    }
}
