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
    /// Логика взаимодействия для CreateorUpdatePage.xaml
    /// </summary>
    /// 

    public partial class CreateorUpdatePage : Page
    {
        private Users _user;
        bool flag;
        OrdersTable order = new OrdersTable();
        List<WorkersTable> WorkersTableList = BaseClass.Base.WorkersTable.ToList();
        List<TimeTable> TimeTableList = BaseClass.Base.TimeTable.ToList();

        public CreateorUpdatePage(Users User)
        {
            InitializeComponent();
            _user = User;
            flag = true;
          
            LBUsers.ItemsSource = BaseClass.Base.Users.ToList();
            LBUsers.SelectedValuePath = "IDUser";
            LBUsers.DisplayMemberPath = "Surname";
            LBService.ItemsSource = BaseClass.Base.ServicesTable.ToList();
            LBService.SelectedValuePath = "IDService";
            LBService.DisplayMemberPath = "Service";
            LBTime.ItemsSource = BaseClass.Base.WorkTimeTable.ToList();
            LBTime.SelectedValuePath = "ID";
            LBTime.DisplayMemberPath = "WorkTime";
        }

        public CreateorUpdatePage(OrdersTable OrderUpdate,Users User)
        {
            InitializeComponent();
            order = OrderUpdate;
            _user = User;
            TBWorkH.Text = Convert.ToString(OrderUpdate.WorkTime);
            switch(OrderUpdate.IDPets)
            {
                case 1:
                    RBPets.IsChecked = true;
                    break;
                case 2:
                    RBNoPets.IsChecked = true;
                    break;
            }
            LBService.ItemsSource = BaseClass.Base.ServicesTable.ToList();
            LBService.SelectedValuePath = "IDService";
            LBService.DisplayMemberPath = "Service";
            List<WorkersTable> WT = WorkersTableList.Where(x => x.IDOrder == OrderUpdate.IDOrder).ToList();
            foreach(ServicesTable ST in LBService.Items)
            {
                if(WT.FirstOrDefault(x=>x.IDService==ST.IDService)!=null)
                {
                    LBService.SelectedItems.Add(ST);
                }
            }

            LBTime.ItemsSource = BaseClass.Base.WorkTimeTable.ToList();
            LBTime.SelectedValuePath = "ID";
            LBTime.DisplayMemberPath = "WorkTime";
            List<TimeTable> TT = TimeTableList.Where(x => x.IDOrder == OrderUpdate.IDOrder).ToList();
            foreach(WorkTimeTable WTT in LBTime.Items)
            {
                if(TT.FirstOrDefault(x=>x.IDWorkTime==WTT.ID)!=null)
                {
                    LBTime.SelectedItems.Add(WTT);
                }
            }
            LBUsers.ItemsSource = BaseClass.Base.Users.ToList();
            LBUsers.SelectedValuePath = "IDUser";
            LBUsers.DisplayMemberPath = "Surname";
            LBUsers.SelectedIndex= OrderUpdate.IDUser-1;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new AdminShowOrders(_user));
        }

        private void AddUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int pets = 0;
                if(RBPets.IsChecked==true)
                {
                    pets = 1;
                }
                if (RBNoPets.IsChecked == true)
                {
                    pets = 2;
                }
                int iduser = Convert.ToInt32(LBUsers.SelectedIndex.ToString())+1;
                int hours = Convert.ToInt32(TBWorkH.Text);
                order.IDUser = iduser;
                order.IDPets = pets;
                order.WorkTime = hours;
                if(flag)
                {
                    BaseClass.Base.OrdersTable.Add(order);
                }
                BaseClass.Base.SaveChanges();

                List<WorkersTable> WTL = WorkersTableList.Where(x => x.IDOrder == order.IDOrder).ToList();
                if(WTL.Count!=0)
                {
                    foreach(WorkersTable wt in WTL)
                    {
                        BaseClass.Base.WorkersTable.Remove(wt);
                    }
                }

                foreach(ServicesTable st in LBService.SelectedItems)
                {
                    WorkersTable WT = new WorkersTable();
                    WT.IDOrder = order.IDOrder;
                    WT.IDService = st.IDService;
                    BaseClass.Base.WorkersTable.Add(WT);
                }
                BaseClass.Base.SaveChanges();

                List<TimeTable> TTL = TimeTableList.Where(x => x.IDOrder == order.IDOrder).ToList();
                if(TTL.Count!=0)
                {
                    foreach(TimeTable tt in TTL)
                    {
                        BaseClass.Base.TimeTable.Remove(tt);
                    }
                }
                foreach(WorkTimeTable wtt in LBTime.SelectedItems)
                {
                    TimeTable TT = new TimeTable();
                    TT.IDOrder = order.IDOrder;
                    TT.IDWorkTime = wtt.ID;
                    BaseClass.Base.TimeTable.Add(TT);
                }
                BaseClass.Base.SaveChanges();


                MessageBox.Show("Данные записаны!", "Запись");
                FrameClass.FrameMain.Navigate(new AdminShowOrders(_user));
            }
            catch
            {
                MessageBox.Show("Данные не записаны!", "Ошибка");
            }

        }
    }
}
