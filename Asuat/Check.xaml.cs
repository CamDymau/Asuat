using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Asuat.Pages;
using System.Data;
namespace Asuat
{
    /// <summary>
    /// Логика взаимодействия для Check.xaml
    /// </summary>

    
    public partial class Check : MetroWindow
    {
        KurEntities tov = new KurEntities();
        List<Product> pr = new List<Product>();

        public List<Product> list;
        double allMoney = 0;
        int percent = 0;
        int countDay = 1;

        public Check()
        {
            InitializeComponent();
            cmbCart.ItemsSource = tov.Client.ToList();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            int AllSum = 0;
            int PledgeSum = 0;
            int check = 0;
         
            AddInCheck s = new AddInCheck();
            s.ShowDialog();

            foreach (var b in pr)
            {
                if (s.c != null)
                {
                    if (b.IDProduct == s.c.IDProduct)
                    {
                        check = 1;
                    }
                }
                else
                {
                    s.Close();
                }
            }
            if (check == 0)
            {    
                if (s.c != null)
                {
                    pr.Add(s.c);
                    foreach (var b in pr)
                    {
                        try
                        {
                            AllSum += Convert.ToInt32(b.PriceProduct);
                            PledgeSum += Convert.ToInt32(b.PledgePrice);
                        }
                        catch
                        {
                            break;
                        }
                    }

                    allMoney = AllSum;
                    txtAllSum.Text = Convert.ToString(AllSum * countDay) + " руб.";
                    txtPledge.Text = Convert.ToString(PledgeSum) + " руб.";
                    if (percent == 0)
                    {
                        txtPerClient.Text = "Скидки нет";
                    }
                    else
                    {
                        txtPerClient.Text = percent.ToString()+"%";
                        txtFianalMoney.Text = Convert.ToString(Math.Round((allMoney - ((allMoney / 100) * percent)) * countDay, 2)) + " руб.";
                    }

                    TovarBd.ItemsSource = null;
                    TovarBd.ItemsSource = pr;
                }                    
            }
            else
            {
                MessageBox.Show($"Товар уже добавлен!", "Ошибочка", MessageBoxButton.OK);
            }
        }

        private void cmbCart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {          
           Client cl = cmbCart.SelectedValue as Client;
             percent = (int)cl.PerсentDiscount;
            txtPerClient.Text = percent.ToString()+"%";
            txtFianalMoney.Text = Convert.ToString(Math.Round((allMoney - ((allMoney / 100) * percent)) * countDay, 2)) + " руб.";
        }

        private void dateSt_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if ((dateStart.SelectedDate != null) && (dateEnd.SelectedDate != null))
            {
                if (dateStart.SelectedDate < dateEnd.SelectedDate)
                {


                    DateTime dt1 = dateStart.SelectedDate.Value;
                    DateTime dt2 = dateEnd.SelectedDate.Value;
                    TimeSpan x = dt2 - dt1;
                    countDay = ((int)x.TotalDays);

                    int AllSum = 0;
                    int PledgeSum = 0;
                    foreach (var b in pr)
                    {
                        AllSum += Convert.ToInt32(b.PriceProduct);
                        PledgeSum += Convert.ToInt32(b.PledgePrice);
                    }
                    allMoney = AllSum;
                    txtAllSum.Text = Convert.ToString(AllSum * countDay) + " руб.";
                    txtPledge.Text = Convert.ToString(PledgeSum) + " руб.";
                    if (percent == 0)
                    {
                        txtPerClient.Text = "Скидки нет";
                    }
                    else
                    {
                        txtPerClient.Text = percent.ToString()+"%";
                        txtFianalMoney.Text = Convert.ToString(Math.Round((allMoney - ((allMoney / 100) * percent)) * countDay, 2)) + " руб.";
                    }
                }
                else
                {
                    MessageBox.Show($"Вы ввели не правильную дату!", "Ошибка", MessageBoxButton.OK);
                }

            }

        }

        private void ButtonGiveCheck_Click(object sender, RoutedEventArgs e)
        {
            int check = 0;
            foreach(var b in pr)
            {
                if (b.Rent == true)
                {
                    check = 1;
                }
            }
            
            if ((txtAllSum.Text != "") && (dateStart.SelectedDate != null) && (dateEnd.SelectedDate != null) && (dateStart.SelectedDate < dateEnd.SelectedDate)&&(TovarBd.ItemsSource !=null))
            {
                if (check != 1)
                {
                    foreach (var v in pr)
                    {
                        tov.Database.ExecuteSqlCommand("UPDATE Product SET NameProduct='" + v.NameProduct + "', PledgePrice='" + v.PledgePrice + "', PriceProduct='" + v.PriceProduct + "', Rent='" + true + "', StartRent='" + dateStart.SelectedDate + "', EndRent='" + dateEnd.SelectedDate + "' WHERE IDProduct='" + v.IDProduct + "'");                     
                        
                        tov.SaveChanges();
                        list = tov.Product.ToList();
                        MessageBox.Show($"Ваш чек выдан!", "ПОЗДРАВЛЯЮ", MessageBoxButton.OK);
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show($"Товар уже арендован!", "ОШИБКА", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show($"Вы что-то упустили!", "Ошибочка", MessageBoxButton.OK);
            }
         
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            Product s = TovarBd.SelectedValue as Product;
            if (s != null)
            {
                int AllSum = 0;
                int PledgeSum = 0;
                pr.Remove(s);
                TovarBd.ItemsSource = null;
                TovarBd.ItemsSource = pr;


                foreach (var b in pr)
                {
                    AllSum += Convert.ToInt32(b.PriceProduct);
                    PledgeSum += Convert.ToInt32(b.PledgePrice);
                }
                allMoney = AllSum;
                txtAllSum.Text = Convert.ToString(AllSum* countDay) + " руб.";
                txtPledge.Text = Convert.ToString(PledgeSum) + " руб.";
                if (percent == 0)
                {
                    txtPerClient.Text = "Скидки нет";
                }
                else
                {
                    txtPerClient.Text = percent.ToString();
                    txtFianalMoney.Text = Convert.ToString(Math.Round((allMoney - ((allMoney / 100) * percent)) * countDay, 2)) + " руб.";
                }
            }
            else
            {
                MessageBox.Show($"Вы не выбрали товар!", "УПС", MessageBoxButton.OK);
            }
        }
    }
}
