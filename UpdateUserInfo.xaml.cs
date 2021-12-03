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

namespace PW10_DB
{
    /// <summary>
    /// Логика взаимодействия для UpdateUserInfo.xaml
    /// </summary>
    public partial class UpdateUserInfo : Window
    {
        private Users _user;
        public UpdateUserInfo(Users User)
        {
            InitializeComponent();
            _user = User;
            TBUpdateName.Text = User.Name;
            TBUpdateSurname.Text = User.Surname;
        }

        private void UpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            _user.Name = TBUpdateName.Text;
            _user.Surname = TBUpdateSurname.Text;
            BaseClass.Base.SaveChanges();
            MessageBox.Show("Данные обновлены", "Обновление");
            this.Close();

        }
    }
}
