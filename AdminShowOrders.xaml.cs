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

namespace PW10_DB
{
    /// <summary>
    /// Логика взаимодействия для AdminShowOrders.xaml
    /// </summary>
    public partial class AdminShowOrders : Page
    {
        private Users _user;
        List<OrdersTable> OrderStart = BaseClass.Base.OrdersTable.ToList();
        public AdminShowOrders(Users User)
        {
            InitializeComponent();
            _user = User;
            LVOrders.ItemsSource = OrderStart;
            List<GenderTable> GT = BaseClass.Base.GenderTable.ToList();
            CBFilterGender.Items.Add("Все записи");
            for (int i =0;i<GT.Count;i++)
            {
                CBFilterGender.Items.Add(GT[i].Gender);
            }
            CBFilterGender.SelectedIndex=0;
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<WorkersTable> WT = BaseClass.Base.WorkersTable.Where(x => x.IDOrder == index).ToList();
            string str = "";
            foreach(WorkersTable s in WT)
            {
                str += s.ServicesTable.Service + " ,";
            }

            tb.Text = "Предоставляемые услуги: "+ str.Substring(0, str.Length - 2);
        }

        private void TextBlock_Loaded_1(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<TimeTable> TT = BaseClass.Base.TimeTable.Where(x => x.IDOrder == index).ToList();
            string str = "";
            foreach (TimeTable s in TT)
            {
                str += s.WorkTimeTable.WorkTime + " ,";
            }

            tb.Text = "Время работы: " + str.Substring(0, str.Length - 2);
        }

        private void TextBlock_Loaded_2(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<WorkersTable> WT = BaseClass.Base.WorkersTable.Where(x => x.IDOrder == index).ToList();
            List<OrdersTable> OT = BaseClass.Base.OrdersTable.Where(x => x.IDOrder == index).ToList();
            int sum = 0;
            
            foreach (WorkersTable s in WT)
            {
                foreach(OrdersTable o in OT)
                {
                    sum += s.ServicesTable.ServiceCost * o.WorkTime;
                }
               
            }
            tb.Text = sum + " рублей";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new AdminPage(_user));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new CreateorUpdatePage(_user));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Button B =(Button)sender;
            int id = Convert.ToInt32(B.Uid);
            OrdersTable OrderDelete = BaseClass.Base.OrdersTable.FirstOrDefault(x => x.IDOrder == id);
            BaseClass.Base.OrdersTable.Remove(OrderDelete);
            BaseClass.Base.SaveChanges();
            FrameClass.FrameMain.Navigate(new AdminShowOrders(_user));
            MessageBox.Show("Запись удалена!", "Удаление");

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender;
            int id = Convert.ToInt32(B.Uid);
            OrdersTable OrderUpdate = BaseClass.Base.OrdersTable.FirstOrDefault(x => x.IDOrder == id);
            FrameClass.FrameMain.Navigate(new CreateorUpdatePage(OrderUpdate,_user));
        }
        List<OrdersTable> OrdersFilter;
        private void Filters()
        {
            int index = CBFilterGender.SelectedIndex;
            if (index != 0)
            {
                OrdersFilter = OrderStart.Where(x => x.Users.IDGender == index).ToList();
            }
            else
            {
                OrdersFilter = OrderStart;
            }
            if (CBFilterPets.IsChecked == true)
            {
                OrdersFilter = OrdersFilter.Where(x => x.IDPets ==1).ToList();
            }
            if (!string.IsNullOrWhiteSpace(TBFilterSurname.Text))
            {
                OrdersFilter = OrdersFilter.Where(x => x.Users.Surname.ToLower().Contains(TBFilterSurname.Text.ToLower())).ToList();
            }
            LVOrders.ItemsSource = OrdersFilter;
        }

        private void CBFilterGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filters();
        }

        private void CBFilterPets_Checked(object sender, RoutedEventArgs e)
        {
            Filters();
        }

        private void CBFilterPets_Unchecked(object sender, RoutedEventArgs e)
        {
            Filters();
        }

        private void TBFilterSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filters();
        }

        private void SortSurname_Checked(object sender, RoutedEventArgs e)
        {
            SortCost.IsChecked = false;
            SortHours.IsChecked = false;
        }

        private void SortCost_Checked(object sender, RoutedEventArgs e)
        {
            SortSurname.IsChecked = false;
            SortHours.IsChecked = false;
        }

        private void SortHours_Checked(object sender, RoutedEventArgs e)
        {
            SortSurname.IsChecked = false;
            SortCost.IsChecked = false;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (SortSurname.IsChecked == true)
            {
                OrdersFilter.Sort((x, y) => x.Users.Surname.CompareTo(y.Users.Surname));
                LVOrders.Items.Refresh();
            }
            if(SortHours.IsChecked==true)
            {
                OrdersFilter.Sort((x, y) => x.WorkTime.CompareTo(y.WorkTime));
                LVOrders.Items.Refresh();
            }
            if(SortCost.IsChecked==true)
            {

                OrdersFilter.Sort((x, y) => x.IDOrder.CompareTo(y.IDOrder));
                LVOrders.Items.Refresh();
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (SortSurname.IsChecked == true)
            {
                OrdersFilter.Sort((x, y) => x.Users.Surname.CompareTo(y.Users.Surname));
                OrdersFilter.Reverse();
                LVOrders.Items.Refresh();
            }
            if (SortHours.IsChecked == true)
            {
                OrdersFilter.Sort((x, y) => x.WorkTime.CompareTo(y.WorkTime));
                OrdersFilter.Reverse();
                LVOrders.Items.Refresh();
            }
            if(SortCost.IsChecked==true)
            {
                OrdersFilter.Sort((x, y) => x.IDOrder.CompareTo(y.IDOrder));
                OrdersFilter.Reverse();
                LVOrders.Items.Refresh();
            }
        }

       
    }
   
}






