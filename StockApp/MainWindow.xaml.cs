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
    public static Product SelectedProduct { get; set; }
    public static Category SelectedCategory { get; set; }
    private readonly IProductService productService;
    private readonly ICategoryService categoryService;

    public MainWindow()
    {
        InitializeComponent();
        this.productService = App.ServiceContainer.GetInstance<IProductService>();
        this.categoryService = App.ServiceContainer.GetInstance<ICategoryService>();
        DataContext = this;
        LoadData();
    }


    private async void LoadData()
    {
        ProductsDataGrid.ItemsSource = await productService.GetAllProducts();
        CategoriesDataGrid.ItemsSource = await categoryService.GetAllCategoriesAsync();
    }

    private void AddProduct_Click(object sender, RoutedEventArgs e)
    {
        var addProductWindow = new AddProductWindow();
        addProductWindow.ShowDialog();
        LoadData();
    }

    private void UpdateProduct_Click(object sender, RoutedEventArgs e)
    {
        if (ProductsDataGrid.SelectedItem is Product selectedProduct)
        {
            SelectedProduct = selectedProduct;
            var updateProductWindow = new UpdateProductWindow();
            updateProductWindow.ShowDialog();
            LoadData();
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
        var addCategoryWindow = new AddCategoryWindow();
        addCategoryWindow.ShowDialog();
        LoadData();
    }

    private void UpdateCategory_Click(object sender, RoutedEventArgs e)
    {
        if (CategoriesDataGrid.SelectedItem is Category selectedCategory)
        {
            SelectedCategory = selectedCategory;
            var updateCategoryWindow = new UpdateCategoryWindow();
            updateCategoryWindow.ShowDialog();
            LoadData();
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