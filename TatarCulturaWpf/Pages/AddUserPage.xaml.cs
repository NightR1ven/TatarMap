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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TatarCulturaWpf.Models;

namespace TatarCulturaWpf.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        private int idUser1=0;
        private User _currentUser = new User();
        private string _filePath = null;
        private string _fileCompare = null;
        private string _photoName = null;
        private static string _currentDirectory = Directory.GetCurrentDirectory() + @"\ImagesUsers\";
        public AddUserPage(User selecetUser)
        {

            InitializeComponent();

            if (selecetUser != null)
            {
                _currentUser = selecetUser;
            _filePath = _currentDirectory + _currentUser.UserPhoto;
                _fileCompare = _filePath;
                Title = "Редактирование данных пользователя";
            }
            if(Manager.idUser==3)
            {
                cmbRol.Visibility = Visibility.Collapsed;
                RolLabel.Visibility = Visibility.Collapsed;
                _currentUser.IdRols = 3;
            }
            _photoName = _currentUser.UserPhoto;
            DataContext = _currentUser;
            cmbRol.ItemsSource = TatarCulturDbEntities.GetContext().UserRols.ToList();
        }

        public AddUserPage(User selecetUser,int idUser)
        {

            InitializeComponent();

            if (selecetUser != null)
            {
                _currentUser = selecetUser;
                _filePath = _currentDirectory + _currentUser.UserPhoto;
                _fileCompare = _filePath;
                Title = "Редактирование данных пользователя";
            }
                cmbRol.Visibility = Visibility.Collapsed;
                RolLabel.Visibility = Visibility.Collapsed;
            _currentUser.IdRols = idUser;
            idUser1 = idUser;
            _photoName = _currentUser.UserPhoto;
            DataContext = _currentUser;
            cmbRol.ItemsSource = TatarCulturDbEntities.GetContext().UserRols.ToList();
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
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentUser.Login))
                s.AppendLine("Поле логин пустое");
            if (tbPassword.Text != tbPassword1.Text)
                s.AppendLine("Пароль не совподают");
            if (string.IsNullOrWhiteSpace(_currentUser.Password))
                s.AppendLine("Поле пароль пустое");
            if (string.IsNullOrWhiteSpace(tbPassword1.Text))
                s.AppendLine("Повторите пароль");
            if (_currentUser.UserRol == null && idUser1==0)
                s.AppendLine("Категория не выбрана");
            if(string.IsNullOrWhiteSpace(_photoName))
                s.AppendLine("фото не выбрано пустое");


            if (s.Length > 0)
            {
                MessageBox.Show(s.ToString());
                return;
            }
            if (_currentUser.IdUser == 0)
            {
                string photo = ChangePhotoName();
                string dest = _currentDirectory + photo;
                File.Copy(_filePath, dest);
                _currentUser.UserPhoto = photo;
                TatarCulturDbEntities.GetContext().Users.Add(_currentUser);
            }
            try
            {
                if (_filePath != null)
                {
                    string photo = ChangePhotoName();
                    string dest = _currentDirectory + photo;
                    if(_filePath!=_fileCompare)
                    {
                        File.Copy(_filePath, dest);
                        _currentUser.UserPhoto = photo;
                    }    
                    
                }
                TatarCulturDbEntities.GetContext().SaveChanges();
                MessageBox.Show("Запись Изменена");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            Manager.MainFrame.GoBack();
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }


        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
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
