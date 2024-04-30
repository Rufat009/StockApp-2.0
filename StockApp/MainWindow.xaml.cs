using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StockApp.Models;
using StockApp.Services.Base;

namespace StockApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IProductService productService;
    private readonly ICategoryService categoryService;

    public MainWindow(IProductService productService, ICategoryService categoryService)
    {
        InitializeComponent();
        this.productService = productService;
        this.categoryService = categoryService;
        DataContext = this;
        LoadData();
    }

    public MainWindow()
    {
        InitializeComponent();
    }
    private async void LoadData()
    {
        ProductsDataGrid.ItemsSource = await productService.GetAllProducts();
        CategoriesDataGrid.ItemsSource = await categoryService.GetAllCategoriesAsync();
    }

    private void AddProduct_Click(object sender, RoutedEventArgs e)
    {
        // Implement your Add Product logic here
    }

    private void UpdateProduct_Click(object sender, RoutedEventArgs e)
    {
        if (ProductsDataGrid.SelectedItem is Product selectedProduct)
        {
            // Implement your Update Product logic here, using selectedProduct
        }
    }

    private async void DeleteProduct_Click(object sender, RoutedEventArgs e)
    {
        if (ProductsDataGrid.SelectedItem is Product selectedProduct)
        {
            await productService.DeleteProductAsync(selectedProduct.Id);
            LoadData();
        }
    }

    private void AddCategory_Click(object sender, RoutedEventArgs e)
    {
        // Implement your Add Category logic here
    }

    private void UpdateCategory_Click(object sender, RoutedEventArgs e)
    {
        if (CategoriesDataGrid.SelectedItem is Category selectedCategory)
        {
            // Implement your Update Category logic here, using selectedCategory
        }
    }

    private async void DeleteCategory_Click(object sender, RoutedEventArgs e)
    {
        if (CategoriesDataGrid.SelectedItem is Category selectedCategory)
        {
            await categoryService.DeleteCategoryAsync(selectedCategory.Id);
            LoadData();
        }
    }
}