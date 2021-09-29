using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Linq;

namespace Asuat
{
    /// <summary>
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : MetroWindow
    {
        string nameTov;
        KurEntities tov = new KurEntities();
        int idTovar = 0;
        string str = "";
        public List<Product> list;
        Product product1;
        int ustal=0;
        public Edit(Product product)
        {
            InitializeComponent();
            this.product1 = product;
           
            nameTov = product.NameProduct;
            if (product.ImgProduct != null)
            {
                using (var stream = new MemoryStream(product.ImgProduct))
                {
                    var decoder = BitmapDecoder.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    ProductImg.Source = decoder.Frames[0];
                }
            }
            ProductNametxb.Text = product.NameProduct;
            ProductPledgetxb.Text = product.PledgePrice.ToString();
            ProductPricetxb.Text = product.PriceProduct.ToString();
            chbRent.IsChecked = product.Rent;
            idTovar = product.IDProduct;
            str = ProductNametxb.Text;
            if (product.Rent == false)
            {
                chbRent.IsEnabled = false;
            }
        }
        private void Button_ClickCancel(object sender, RoutedEventArgs e)
        {
            tov.SaveChanges();
            Close();
        }
        private void Check_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char s = Convert.ToChar(e.Text);
            if ((s < 'А' || s > 'я') && (s < '0' || s > '9')) e.Handled = true;
        }
        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            if(idTovar != 0)
            {
                tov.Database.ExecuteSqlCommand("DELETE FROM Product WHERE IDProduct=" + idTovar);
                tov.SaveChanges();
                MessageBox.Show($"Успешно удаленно", "Удаленно", MessageBoxButton.OK);
                list = tov.Product.ToList();
                Close();
            }
            else
            {
                MessageBox.Show($"Вы не выбрали товар!", "Ошибка", MessageBoxButton.OK);
            }        
        }
        private void ButtonAddImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.FilterIndex = 2;
            open.Filter = "jpg|*.jpg|  png|*.png";

            if (open.ShowDialog() == true)
            {
                BitmapImage source = new BitmapImage();
                source.BeginInit(); // начало считывания фото
                source.UriSource = new Uri(@"" + open.FileName, UriKind.Relative);
                source.CacheOption = BitmapCacheOption.OnLoad; //Задержка
                source.EndInit();
                ProductImg.Source = null;
                ProductImg.Source = source;
                ProductImg.Stretch = Stretch.Uniform;
            }
            ustal = 1;
        }
        private void NumCheck(object sender, TextCompositionEventArgs e)
        {
            char s = Convert.ToChar(e.Text);
            if (s < '0' || s > '9') e.Handled = true;
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            int checker = 0;
            int checker1 = 0;
   
            foreach (var h in tov.Product)
            {
                if (h.NameProduct == ProductNametxb.Text)
                {
                    checker = 1;
                    break;
                }              
            }
            foreach (var h in tov.Product)
            {
                if (ProductNametxb.Text == str)
                {
                    checker1 = 1;
                    break;
                }
            }
            if ((checker != 1) || ( checker1==1))
            {
                if (nameTov != null)
                {
                    if ((ProductPledgetxb.Text != "") && (ProductPricetxb.Text != "") && (ProductNametxb.Text != ""))
                    {
                        if (ProductNametxb.Text.Length < 50)
                        {
                            if ((ProductPricetxb.Text.Length < 9)&&(ProductPledgetxb.Text.Length<9))
                            {
                                foreach (Product p in tov.Product)
                                {
                                    if (p.IDProduct == idTovar)
                                    {
                                        p.NameProduct = ProductNametxb.Text;
                                        p.PledgePrice = Convert.ToInt32(ProductPledgetxb.Text);
                                        p.PriceProduct = Convert.ToInt32(ProductPricetxb.Text);
                                        if (ustal == 1)
                                        {
                                            if (ProductImg.Source != null)
                                            {
                                                p.ImgProduct = Img.ImageToByteArray(ProductImg.Source as BitmapImage);
                                            }
                                        }
                                        if (ustal == 2)
                                        {
                                            p.ImgProduct = null;
                                        }
                                        if (chbRent.IsChecked == false)
                                        {
                                            p.Rent = false;
                                            p.StartRent = null;
                                            p.EndRent = null;
                                        }                                       
                                    }
                                }

                                tov.SaveChanges();
                                list = tov.Product.ToList();

;                                MessageBox.Show($"Вы изменили товар!", "Успех", MessageBoxButton.OK);
                                Close();
                            }
                            else
                            {
                                MessageBox.Show($"Поля c ценой не должны превышать\nДевяти значного числа", "УПС", MessageBoxButton.OK);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Поле ИМЯ не должно превышать 50", "УПС", MessageBoxButton.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Вы не заполнили поле!", "Ошибка", MessageBoxButton.OK);
                    }                                 
                }
                else
                {
                    MessageBox.Show($"Вы не выбрали товар!", "Ошибка", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show($"Товар с таким названием уже существует!", "Ошибка", MessageBoxButton.OK);
            }
            
        }
        private void ButtonВDel_Click(object sender, RoutedEventArgs e)
        {
            if (ProductImg.Source != null)
            {               
                ProductImg.Source = null;
                ustal = 2;
            }
            else
            {
                MessageBox.Show($"Картинки-то нет!", "Ошибка", MessageBoxButton.OK);
            }
        }
    }
}