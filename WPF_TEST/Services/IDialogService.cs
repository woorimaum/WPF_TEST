using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_TEST.Services
{
    public interface IDialogService
    {
        /// <summary>
        /// Message Dialog 호출
        /// </summary>
        /// <param name="title">Dialog Title</param>
        /// <param name="message">Dialog Message</param>
        /// <returns></returns>
        Task ShowMessageDialogAsync(string title, string message);
    }
}
