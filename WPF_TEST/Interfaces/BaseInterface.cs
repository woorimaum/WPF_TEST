using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_TEST.Interfaces
{
    public interface IUserService
    {
        Task SaveUserDataAsync();
    }

    public interface IMainViewModel
    {
        ICommand ClickCommand { get; }
    }
}
