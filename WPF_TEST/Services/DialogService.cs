using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_TEST.Services
{
    public class DialogService : IDialogService
    {
        public Task ShowMessageDialogAsync(string title, string message)
        {
            void ShowDialog()
            {
                var owner = Application.Current?.MainWindow;

                if (owner is not null)
                {
                    MessageBox.Show(owner, message, title, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            var dispatcher = Application.Current?.Dispatcher;

            if (dispatcher is null || dispatcher.CheckAccess())
            {
                ShowDialog();
                return Task.CompletedTask;
            }

            return dispatcher.InvokeAsync(ShowDialog).Task;
        }
    }
}
