using System.Configuration;
using System.Data;
using System.Windows;
using SimpleInjector;
using StockApp.Data;
using StockApp.Models;
using StockApp.Repositories;
using StockApp.Repositories.Base;

namespace StockApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
 public partial class App : Application
    {
        public static Container ServiceContainer { get; set; } = new Container();

        public static string connectionString = $"Server=localhost;Database=StockApp;User Id=admin;Password=admin;TrustServerCertificate=True;";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ConfigureContainer();

            //var startView = new MainWindow();
            //startView.ShowDialog();

        }

        private void ConfigureContainer()
        {
            ServiceContainer.RegisterSingleton<IProductsRepository<Product>, ProductRepository>();

            ServiceContainer.RegisterSingleton<StockAppDbContext>();

            ServiceContainer.Verify();
        }


    }