using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MahApps.Metro.Controls;

namespace Asuat.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddTov.xaml
    /// </summary>
    public partial class AddTov : MetroWindow
    {
        KurEntities tov = new KurEntities();
        public AddTov()
        {
            InitializeComponent();
        }
        private void Check_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char s = Convert.ToChar(e.Text);
            if ((s < 'А' || s > 'я')&&(s < '0' || s > '9')) e.Handled = true;
        }

        private void ButtonImg_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog open = new OpenFileDialog();
            open.FilterIndex = 2;
            open.Filter = "jpg|*.jpg| png|*.png";

            if (open.ShowDialog() == true)
            {
                BitmapImage source = new BitmapImage();
                source.BeginInit(); // начало считывания фото
                source.UriSource = new Uri(@"" + open.FileName, UriKind.Relative);
                source.CacheOption = BitmapCacheOption.OnLoad; //Задержка
                source.EndInit();

                ProductImg.Source = source;
                ProductImg.Stretch = Stretch.Uniform;
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void NumCheck(object sender, TextCompositionEventArgs e)
        {
            char s = Convert.ToChar(e.Text);
            if (s < '0' || s > '9') e.Handled = true;
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            int checker = 0;
            foreach(var h in tov.Product) 
            { 
                if(h.NameProduct == ProductNametxb.Text)
                {
                    checker = 1;
                    break;
                }
            }
            if (checker != 1)
            {
                if ((ProductPledgetxb.Text != "") && (ProductPricetxb.Text != "") && (ProductNametxb.Text != ""))
                {
                    if (ProductNametxb.Text.Length < 50)
                    {
                        if ((ProductPricetxb.Text.Length < 9) && (ProductPledgetxb.Text.Length < 9))
                        {
                            Product pr = new Product();
                            pr.NameProduct = ProductNametxb.Text;
                            pr.PledgePrice = Convert.ToInt32(ProductPledgetxb.Text);
                            pr.PriceProduct = Convert.ToInt32(ProductPricetxb.Text);
                            pr.ImgProduct = Img.ImageToByteArray(ProductImg.Source as BitmapImage);

                            tov.Product.Add(pr);
                            tov.SaveChanges();
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
                MessageBox.Show($"Такое имя существует", "Ошибка", MessageBoxButton.OK);
            }

        }
    }
}
