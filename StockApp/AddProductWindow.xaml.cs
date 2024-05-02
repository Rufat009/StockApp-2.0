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
using StockApp.Services;
using StockApp.Services.Base;

namespace StockApp;

/// <summary>
/// Interaction logic for AddProductWindow.xaml
/// </summary>
public partial class AddProductWindow : Window
{
    private readonly IProductService productService;
    private readonly ICategoryService categoryService;

    public AddProductWindow()
    {
        InitializeComponent();

        this.productService = App.ServiceContainer.GetInstance<IProductService>();
        this.categoryService = App.ServiceContainer.GetInstance<ICategoryService>();
        DataContext = this;
        LoadCategories();
    }
    private async void LoadCategories()
    {
        var categories = await categoryService.GetAllCategoriesAsync();
        CategoriesListBox.ItemsSource = categories ?? new List<Category>();
        CategoriesListBox.DisplayMemberPath = "Name";
        CategoriesListBox.SelectedValuePath = "Id";
    }

    private async void AddProductButton_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(CountTextBox.Text, out var count))
        {
            MessageBox.Show("Please enter a valid number for the count.");
            return;
        }

        var product = new Product
        {
            Name = NameTextBox.Text,
            Description = DescriptionTextBox.Text,
            Count = count,
            ProductCategories = CategoriesListBox.SelectedItems.Cast<Category>().Select(c => new ProductCategory { CategoryId = c.Id }).ToList()
        };

        await productService.AddProductAsync(product);

        MessageBox.Show("Product added successfully!");
        this.Close();
    }
}
