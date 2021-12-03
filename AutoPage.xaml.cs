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
    /// Логика взаимодействия для AutoPage.xaml
    /// </summary>
    public partial class AutoPage : Page
    {
        public AutoPage()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int pasCode = TbPasAvto.Password.GetHashCode();
            Users User = BaseClass.Base.Users.FirstOrDefault(c => c.Login == TbLoginAvto.Text && c.Password == pasCode);
            if (User == null)
            {
                MessageBox.Show("Вы не зарегистрированы!", "Авторизация");
                MessageBoxResult result = MessageBox.Show("Хотите зарегистрироваться", "Регистрация", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        FrameClass.FrameMain.Navigate(new RegPage());
                        break;
                    case MessageBoxResult.No:
                        FrameClass.FrameMain.Navigate(new AutoPage());
                        break;
                }
            }

            else
            {
                switch (User.IDRole)
                {
                    case 1:
                        MessageBox.Show("Добро пожаловать, "+ User.Name, "Авторизация");
                        FrameClass.FrameMain.Navigate(new UserPage(User));
                        break;
                    case 2:
                        MessageBox.Show("Вы вошли как Администратор. Добро пожаловать, " + User.Name, "Авторизация");
                        FrameClass.FrameMain.Navigate(new AdminPage(User));
                        break;
                }
            }
        }
    }
}
