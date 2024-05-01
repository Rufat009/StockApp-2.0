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
    /// Interaction logic for UpdateCategoryWindow.xaml
    /// </summary>
    public partial class UpdateCategoryWindow : Window
    {
        private readonly ICategoryService categoryService;

        public Category Category { get; set; }

        public UpdateCategoryWindow( )
        {
            this.categoryService = App.ServiceContainer.GetInstance<ICategoryService>();
            
            InitializeComponent();

            Category = MainWindow.SelectedCategory;
            NameTextBox.Text = Category.Name;
            DataContext = this;
        }

        private async void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        {
            Category.Name = NameTextBox.Text;

            await categoryService.UpdateCategoryAsync(Category);

            MessageBox.Show("Category updated successfully!");
            this.Close();
        }
    }
}
