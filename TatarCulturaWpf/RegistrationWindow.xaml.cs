using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using TatarCulturaWpf.Models;

namespace TatarCulturaWpf.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private User _currentUser = new User();
        private string _filePath = null;
        private string _photoName = null;
        private static string _currentDirectory = Directory.GetCurrentDirectory() + @"\ImagesUsers\";
        public RegistrationWindow()
        {
            InitializeComponent();
            _photoName = _currentUser.UserPhoto;
            DataContext = _currentUser;
            _currentUser.IdRols = (int)3;

        }

        string ChangePhotoName()
        {
            string x = _currentDirectory + _photoName;
            string photoname = _photoName;
            int i = 0;
            if (File.Exists(x))
            {
                while (File.Exists(x))
                {
                    i++;
                    x = _currentDirectory + i.ToString() + photoname;
                }
                photoname = i.ToString() + photoname;
            }
            return photoname;
        }


        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
           
            List<User> users = TatarCulturDbEntities.GetContext().Users.ToList();
            User user = users.FirstOrDefault(p => p.Login == _currentUser.Login);
           
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentUser.Login))
                s.AppendLine("Поле логин пустое");
            if (user != null)
            {
                string user1 = user.Login;
                if (user1 == _currentUser.Login)
                    s.AppendLine("Такой Логин уже зарегистрирован");
            }
            if (tbPassword.Text != tbPassword1.Text)
                s.AppendLine("Пароль не совподают");
            if (string.IsNullOrWhiteSpace(_currentUser.Password))
                s.AppendLine("Поле пароль пустое");
            if (string.IsNullOrWhiteSpace(tbPassword1.Text))
                s.AppendLine("Повторите пароль");
            //if (_currentUser.UserRol == null)
            //    s.AppendLine("Категория не выбрана");
            if (string.IsNullOrWhiteSpace(_photoName))
                s.AppendLine("фото не выбрано пустое");

            if (s.Length > 0)
            {
                MessageBox.Show(s.ToString());
                return;
            }
            if (_currentUser.IdUser == 0)
            {
                _currentUser.IdRols = 2;
                string photo = ChangePhotoName();
                string dest = _currentDirectory + photo;
                File.Copy(_filePath, dest);
                _currentUser.UserPhoto = photo;
                TatarCulturDbEntities.GetContext().Users.Add(_currentUser);
            }
            try
            {
                
                TatarCulturDbEntities.GetContext().SaveChanges();
                MessageBox.Show("Запись Изменена");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            Owner.Show();
            Close();
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Close();
        }


        private void WindowClosed(object sender, EventArgs e)
        {
           Owner.Show();
        }


        private void WindowIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (Visibility == Visibility.Visible)
            {
                TatarCulturDbEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            }
        }

        private void BtnLoadClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //Диалог открытия файла
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif) | *.gif";
                // диалог вернет true, если файл был открыт
                if (op.ShowDialog() == true)
                {
                    // проверка размера файла
                    // по условию файл дожен быть не более 2Мб.
                    FileInfo fileInfo = new FileInfo(op.FileName);
                    if (fileInfo.Length > (1024 * 1024 * 2))
                    {
                        // размер файла меньше 2Мб. Поэтому выбрасывается новое исключение
                        throw new Exception("Размер файла должен быть меньше 2Мб");
                    }
                    imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
                    _photoName = op.SafeFileName;
                    _filePath = op.FileName;
                }
            }
            catch
            {
                MessageBox.Show("Нет файла");
                _filePath = null;
            }
        }
    }
}
