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
using System.Windows.Shapes;
using StockApp.Models;
using StockApp.Services.Base;

namespace StockApp
{
    /// <summary>
    /// Interaction logic for AddCategoryWindow.xaml
    /// </summary>
    public partial class AddCategoryWindow : Window
    {
        private readonly ICategoryService categoryService;
        public AddCategoryWindow()
        {
            InitializeComponent();


            this.categoryService = App.ServiceContainer.GetInstance<ICategoryService>();
            DataContext = this;

        }
        private async void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var newCategory = new Category
            {
                Name = CategoryNameTextBox.Text,
            };

            await categoryService.AddCategoryAsync(newCategory);
            MessageBox.Show("Category added successfully!");
            this.Close();
        }
    }
}

