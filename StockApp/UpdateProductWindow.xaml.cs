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
    /// Interaction logic for UpdateProductWindow.xaml
    /// </summary>
    public partial class UpdateProductWindow : Window
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private Product productToUpdate;

        public UpdateProductWindow()
        {
            InitializeComponent();

            this.productService = App.ServiceContainer.GetInstance<IProductService>();
            this.categoryService = App.ServiceContainer.GetInstance<ICategoryService>();
            this.productToUpdate = MainWindow.SelectedProduct;

            LoadProductDetails();
            LoadCategories();
        }
    


        private void LoadProductDetails()
        {
            NameTextBox.Text = productToUpdate.Name;
            DescriptionTextBox.Text = productToUpdate.Description;
            CountTextBox.Text = productToUpdate.Count.ToString();
        }

        private async void LoadCategories()
        {
            var categories = await categoryService.GetAllCategoriesAsync();
            CategoriesListBox.ItemsSource = categories;
            CategoriesListBox.DisplayMemberPath = "Name";
            CategoriesListBox.SelectedValuePath = "Id";


            foreach (var category in productToUpdate.ProductCategories.Select(pc => pc.Category))
            {
                CategoriesListBox.SelectedItems.Add(category);
            }
        }

        private async void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        {
            productToUpdate.Name = NameTextBox.Text;
            productToUpdate.Description = DescriptionTextBox.Text;
            productToUpdate.Count = int.TryParse(CountTextBox.Text, out var count) ? count : (int?)null;
            productToUpdate.ProductCategories = CategoriesListBox.SelectedItems.Cast<Category>().Select(c => new ProductCategory { CategoryId = c.Id, ProductId = productToUpdate.Id }).ToList();

           await  productService.UpdateProductAsync(productToUpdate);

            MessageBox.Show("Product updated successfully!");
            this.Close();
        }
    }
}
