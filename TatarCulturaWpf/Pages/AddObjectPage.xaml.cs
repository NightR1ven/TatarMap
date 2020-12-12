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
    /// Логика взаимодействия для AddObjectPage.xaml
    /// </summary>
    public partial class AddObjectPage : Page
    {
        private Models.Object _currentObject = new Models.Object();
        private string _filePath = null;
        private string _photoName = null;
        private static string _currentDirectory = Directory.GetCurrentDirectory() + @"\Images\";

        public AddObjectPage(Models.Object tatObject)
        {
            InitializeComponent();
            if (tatObject != null) 
            {
                _currentObject = tatObject;
                _filePath = _currentDirectory + _currentObject.ObjectPhoto;
            }
            DataContext = _currentObject;
            _photoName = _currentObject.ObjectPhoto;
            cmbType.ItemsSource = TatarCulturDbEntities.GetContext().Types.ToList();
        }

        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentObject.Name))
                s.AppendLine("Поле название пустое");
            if (string.IsNullOrWhiteSpace(_currentObject.Description))
                s.AppendLine("Поле описание пустое");
            if (_currentObject.Type == null)
                s.AppendLine("Тип не выбрана");
            if (string.IsNullOrWhiteSpace(tbPhotoName.Text))
                s.AppendLine("Фото не выбрано пустое");
            return s;
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
            StringBuilder _error = CheckFields();
            if (_error.Length > 0)
            {
                MessageBox.Show(_error.ToString());
                return;
            }

            if(_currentObject.IdObject==0)
            {
                string photo = ChangePhotoName();
                string dest = _currentDirectory + photo;
                File.Copy(_filePath, dest);
                _currentObject.ObjectPhoto = photo;
                TatarCulturDbEntities.GetContext().Objects.Add(_currentObject);
            }
            try
            {
                if(_filePath!=null)
                {
                    string photo = ChangePhotoName();
                    string dest = _currentDirectory + photo;
                    File.Copy(_filePath, dest);
                    _currentObject.ObjectPhoto = photo;
                }
                TatarCulturDbEntities.GetContext().SaveChanges();
                MessageBox.Show("Запись Изменена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            Manager.MainFrame.GoBack();
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
    }
}
