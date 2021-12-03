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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private Users _user;
        public AdminPage(Users User)
        {
            InitializeComponent();
            _user = User;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new AutoPage());
        }

        private void ShowData_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new AdmShowPage(_user));
        }

        private void ShowOrders_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new AdminShowOrders(_user));
        }

        private void PersonalParlor_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new UserPage(_user));
        }
    }
}
