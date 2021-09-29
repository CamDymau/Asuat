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
using MahApps.Metro.Controls;
namespace Asuat
{
    /// <summary>
    /// Логика взаимодействия для AddInCheck.xaml
    /// </summary>
    public partial class AddInCheck : MetroWindow
    {
        public Product c;
        KurEntities tov = new KurEntities();
        public AddInCheck()
        {
            InitializeComponent();
            TovarBd.ItemsSource = tov.Product.ToList();
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (c != null)
            {
                Check s = new Check();
                s.Show();
                Close();
            }
            else
            {
                Close();
            }
        }
        private void GridTovar_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            c = TovarBd.SelectedValue as Product;
            this.Close();
        }
       
    }
}
