using LopushokDemo.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace LopushokDemo.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddAndEditPage.xaml
    /// </summary>
    public partial class AddAndEditPage : Page
    {
        private Product _currentProduct = new Product();
        public bool photochange = false;
        byte[] photo = null;
        public AddAndEditPage(Product selectedProduct)
        {
            InitializeComponent();
            if (selectedProduct != null)
                _currentProduct = selectedProduct;

            DataContext = _currentProduct;
            combotype.ItemsSource = App.lopushokDBEntities.TypeProd.ToList();
            combomat.ItemsSource = App.lopushokDBEntities.Material.ToList();
            combotype.DisplayMemberPath = "NameType";
            combomat.DisplayMemberPath = "Name";
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofdPicture = new OpenFileDialog();
            ofdPicture.Filter = "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            ofdPicture.FilterIndex = 1;
            if (ofdPicture.ShowDialog() == true)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(ofdPicture.FileName);
                image.EndInit();
                prodim.Source = image;
                photochange = true;
                photo = File.ReadAllBytes(ofdPicture.FileName);
            }
        }
        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(nametxt.Text))
                s.AppendLine("Поле наименование пустое");
            if (string.IsNullOrWhiteSpace(costtxt.Text))
                s.AppendLine("Поле стоимость пустое");
            if (combotype.SelectedIndex == -1)
                s.AppendLine("Поле тип пустое");
            if (combomat.SelectedIndex == -1)
                s.AppendLine("Поле материал пустое");
            if (string.IsNullOrWhiteSpace(counttxt.Text))
                s.AppendLine("Поле количество пустое");
            return s;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder _error = CheckFields();
            if (_error.Length > 0)

            {
                MessageBox.Show(_error.ToString());
                return;

            }
            if (_currentProduct.Id_Prod == 0)
            {
                if (photochange == true)
                {
                    _currentProduct.Picture = photo;
                }
                var gg = combotype.SelectedItem as TypeProd;
                _currentProduct.Id_Type = gg.Id;
                var ff = combomat.SelectedItem as Material;
                _currentProduct.Id_Material = ff.id;
                App.lopushokDBEntities.Product.Add(_currentProduct);
            }

            try
            {
                if (photochange == true)
                {
                    _currentProduct.Picture = photo;
                }
                var gg = combotype.SelectedItem as TypeProd;
                _currentProduct.Id_Type = gg.Id;
                var ff = combomat.SelectedItem as Material;
                _currentProduct.Id_Material = ff.id;
                App.lopushokDBEntities.SaveChanges();
                MessageBox.Show("Запись изменена");
                App.MainFrame.Navigate(new ProductPage());
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
}
    }
}
