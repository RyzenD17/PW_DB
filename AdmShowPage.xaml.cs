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
    /// Логика взаимодействия для AdmShowPage.xaml
    /// </summary>
    public partial class AdmShowPage : Page
    {
        private Users _user;
        public AdmShowPage(Users User)
        {
            InitializeComponent();
            _user = User;
            DgUsers.ItemsSource = BaseClass.Base.Users.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new AdminPage(_user));
        }
    }
}
