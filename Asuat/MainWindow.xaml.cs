using Asuat.Pages;
using System.Linq;
using System.Windows;
using MahApps.Metro.Controls;
using System.Data;

namespace Asuat
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : MetroWindow
    {
        KurEntities tov = new KurEntities();
        public MainWindow()
        {         
             InitializeComponent();
             TovarBd.ItemsSource = tov.Product.ToList();
        }
        private void ButtonAddPr_Click(object sender, RoutedEventArgs e)
        {
            AddTov s = new AddTov();
            s.ShowDialog();

            TovarBd.ItemsSource = null;
            TovarBd.ItemsSource = tov.Product.ToList();     
        }
        private void ButtonCheck_Click(object sender, RoutedEventArgs e)
        {
            Check s = new Check();
            s.ShowDialog();

            TovarBd.ItemsSource = null;
            if (s.list != null) TovarBd.ItemsSource = s.list;
            
            else TovarBd.ItemsSource = tov.Product.ToList();
            
        }
    
        private void ButtonAddClient_Click(object sender, RoutedEventArgs e)
        {
            AddClient s = new AddClient();
            s.ShowDialog();           
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Product prrr = TovarBd.SelectedValue as Product;
            if (prrr != null)
            {
                Edit s = new Edit(prrr);
                s.ShowDialog();

                if (s.list != null)
                {
                    TovarBd.ItemsSource = null;
                    TovarBd.ItemsSource = s.list;tov.SaveChanges();
                }           
            }
            else
            {
                MessageBox.Show($"Вы не выбрали товар!", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void ButtonFind_Click(object sender, RoutedEventArgs e)
        {
            TovarBd.ItemsSource = tov.Product.Where(itemF => itemF.NameProduct == txbFind.Text).ToList();
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            TovarBd.ItemsSource = tov.Product.ToList();
        }
        private void txbFind_GotFocus(object sender, RoutedEventArgs e)
        {
            txbFind.Text = "";
        }
        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            Product prrr = TovarBd.SelectedValue as Product;
            if (prrr != null)
            {
                tov.Product.Remove(prrr);
                tov.SaveChanges();
                MessageBox.Show($"Товар удален!", "Удалено", MessageBoxButton.OK);

                TovarBd.ItemsSource = null;
                TovarBd.ItemsSource = tov.Product.ToList();
            }
            else
            {
                MessageBox.Show($"Вы не выбрали товар!", "Ошибка", MessageBoxButton.OK);
            }
        }
   
    }
}
