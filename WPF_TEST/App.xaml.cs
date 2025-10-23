using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_TEST.Services;
using WPF_TEST.ViewModels;

namespace WPF_TEST
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices;
            this.InitializeComponent();
        }

        public static new App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices
        {
            get
            {
                var services = new ServiceCollection();

                // Services //
                // services.AddSingleton<IContactsService, ContactsService>();
                services.AddSingleton<IDialogService, DialogService>();

                // ViewModels //
                services.AddTransient<MainViewModel>();

                return services.BuildServiceProvider();
            }
        }
    }
}
