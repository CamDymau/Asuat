using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
namespace Asuat
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : MetroWindow
    {
        KurEntities tov = new KurEntities();
        public Product dada;
        public AddClient()
        {
            InitializeComponent();
        }

        private void txtNum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char s = Convert.ToChar(e.Text);
            if (s < '0' || s > '9') e.Handled = true;
        }
        private void ButtunCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Check_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char s = Convert.ToChar(e.Text);
            if (s < 'А' || s > 'я') e.Handled = true;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {                  
            if((txbName.Text.Trim()!="")&&(txbSur.Text.Trim() != "")&&(txbFname.Text.Trim() != "")&&(txbMob.Text!="") && (Convert.ToInt32(txbMob.Text.Length)>=8) && (Convert.ToInt32(txbMob.Text.Length) <= 11) && (Convert.ToInt32(txbNum.Text.Length) == 8) && (Convert.ToInt32(txbPer.Text)>0) && (Convert.ToInt32(txbPer.Text) < 100))
            {
                if ((txbFname.Text.Length < 50) && (txbSur.Text.Length < 50) && (txbFname.Text.Length < 50))
                {
                    int conv = Convert.ToInt32(txbNum.Text);
                    var check = (from l in tov.Client
                     where l.NumCard== conv
                                 select l).SingleOrDefault();
                    if (check == null)
                    {
                        Client cl = new Client();
                        cl.FIO = txbName.Text + " " + txbSur.Text + " " + txbFname.Text;
                        cl.NumMobile = txbMob.Text;
                        cl.PerсentDiscount = Convert.ToInt32(txbPer.Text);
                        cl.NumCard = Convert.ToInt32(txbNum.Text);

                        tov.Client.Add(cl);
                        tov.SaveChanges();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show($"Такая карта уже есть", "Ошибка", MessageBoxButton.OK);
                    }                  
                }
                else 
                {
                    MessageBox.Show($"Поля имя, фамилия, отчество!\nНе должны превышать 50", "УПС", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show($"Вы где-то ошиблись!", "УПС", MessageBoxButton.OK);
            }
        }
    }
}
