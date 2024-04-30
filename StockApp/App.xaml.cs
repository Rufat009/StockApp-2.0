using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using StockApp.Data;
using StockApp.Models;
using StockApp.Repositories;
using StockApp.Repositories.Base;
using StockApp.Services;
using StockApp.Services.Base;

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
            ServiceContainer.RegisterSingleton<StockAppDbContext>();

            ServiceContainer.RegisterSingleton<IProductsRepository, ProductRepository>();
            ServiceContainer.RegisterSingleton<ICategoryRepository, CategoryRepository>();

            ServiceContainer.RegisterSingleton<IProductService, ProductService>();
            ServiceContainer.RegisterSingleton<ICategoryService, CategoryService>();
          
            ServiceContainer.Verify();

        }


    }