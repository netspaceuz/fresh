
using Fresh.Desktop.Windows;
﻿using Fresh.Domain.Entities;
using Fresh.Service.Services.PageServices;
using Fresh.Service.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fresh.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            InitializeComponent();
            Click();
        }
        private void ProductsDgUi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public async void Click()
        {
            ProductPage products = new ProductPage();
            List<ProductsView> productPages = await products.GetProductViews();
            ProductsDgUi.ItemsSource = productPages;
        }

        private void PopupBox_OnClosed(object sender, RoutedEventArgs e)
        {

        }

        private void PopupBox_OnOpened(object sender, RoutedEventArgs e)
        {

        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ProductsView products = (ProductsView)ProductsDgUi.SelectedItem;
            if (products == null)
            {
                MessageBox.Show("Please select row", "Error", MessageBoxButton.OK, MessageBoxImage.Hand); return;
            }
            ProductPage productPage = new ProductPage();
            var result = await productPage.DeleteProduct(products);
            if (result)
            {
                ProductPage products1 = new ProductPage();
                List<ProductsView> productPages = await products1.GetProductViews();
                ProductsDgUi.ItemsSource = productPages;
                MessageBox.Show("Cashier successfully deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("There was wrong with delete cashier", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProducts add = new AddProducts();
            add.Show();
        }
        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            ProductPage productPage = new();
            var product = (ProductsView)ProductsDgUi.SelectedItem;
            if (product == null)
            {
                MessageBox.Show("Please select row", "Error", MessageBoxButton.OK, MessageBoxImage.Hand); return;
            }
            if (await productPage.UpdateProduct(product.Id, product))
                MessageBox.Show("Cashier successfully updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("There was wrong with update product", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
        }
    }
}
