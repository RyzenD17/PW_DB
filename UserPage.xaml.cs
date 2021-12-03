using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private Users _user;
        private string _path;
        private UserPhoto UP;
        public UserPage(Users User)
        {
            InitializeComponent();
            _user = User;
            TBUserName.Text = _user.Name;
            TBUserSurname.Text = _user.Surname;
            if (User.UserPhoto != null && User.UserPhoto.PhotoBinary != null)
            {
                byte[] PhotoArray = User.UserPhoto.PhotoBinary;
                BitmapImage BI = new BitmapImage();
                using (MemoryStream MS = new MemoryStream(PhotoArray))
                {
                    BI.BeginInit();
                    BI.StreamSource = MS;
                    BI.CacheOption = BitmapCacheOption.OnLoad;
                    BI.EndInit();
                }
                UserPhotoImage.Source = BI;
            }
        }

        private void GoMenu_Click(object sender, RoutedEventArgs e)
        {
            if(_user.IDRole==1)
            {
                FrameClass.FrameMain.Navigate(new AutoPage());
            }
            if (_user.IDRole == 2)
            {
                FrameClass.FrameMain.Navigate(new AdminPage(_user));
            }
               
        }

        private void ChangePhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                UserPhoto U = BaseClass.Base.UserPhoto.FirstOrDefault(x => x.IDUser == _user.IDUser);
                if (U == null)
                {
                    UP = new UserPhoto();
                    UP.IDUser = _user.IDUser;
                    OpenFileDialog OFD = new OpenFileDialog();
                    OFD.ShowDialog();
                    _path = OFD.FileName;
                    System.Drawing.Image SDI = System.Drawing.Image.FromFile(_path);
                    ImageConverter IC = new ImageConverter();
                    byte[] PhotoArray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));
                    UP.PhotoBinary = PhotoArray;
                    BaseClass.Base.UserPhoto.Add(UP);
                    BaseClass.Base.SaveChanges();
                    MessageBox.Show("Картинка добавлена", "Добавление");
                }
                else
                {
                    OpenFileDialog OFD = new OpenFileDialog();
                    OFD.ShowDialog();
                    _path = OFD.FileName;
                    System.Drawing.Image SDI = System.Drawing.Image.FromFile(_path);
                    ImageConverter IC = new ImageConverter();
                    byte[] PhotoArray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));
                    U.PhotoBinary = PhotoArray;
                    BaseClass.Base.SaveChanges();
                    MessageBox.Show("Картинка изменена", "Редактирование");
                }

                FrameClass.FrameMain.Navigate(new UserPage(_user));


            }
            catch
            {
                MessageBox.Show("Картинка не выбрана", "Ошибка");
            }
        }

        private void UpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            UpdateUserInfo updateUserInfo = new UpdateUserInfo(_user);
            updateUserInfo.ShowDialog();
            FrameClass.FrameMain.Navigate(new UserPage(_user));
        }

        private void UpdateLP_Click(object sender, RoutedEventArgs e)
        {
            UpdateLP updateLoginPass = new UpdateLP(_user);
            updateLoginPass.ShowDialog();
            FrameClass.FrameMain.Navigate(new AutoPage());
        }
    }
}
