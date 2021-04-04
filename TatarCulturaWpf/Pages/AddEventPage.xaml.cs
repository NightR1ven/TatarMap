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
    /// Логика взаимодействия для AddEventPage.xaml
    /// </summary>
    public partial class AddEventPage : Page
    {
        private Models.Event _currentEvent = new Models.Event();
        private string _filePath = null;
        private string _photoName = null;
        private string _fileCompare = null;
        private static string _currentDirectory = Directory.GetCurrentDirectory() + @"\ImagesEvent\";
        public AddEventPage(Models.Event eventTat)
        {
            InitializeComponent();
            
            if (eventTat != null)
            {
                _currentEvent = eventTat;
                _filePath = _currentDirectory + _currentEvent.EventPhoto;
                _fileCompare = _filePath;
                Title = "Редактироване информации об акции";
            }
            _filePath = _currentDirectory + _currentEvent.EventPhoto;
            DataContext = _currentEvent;
            _photoName = _currentEvent.EventPhoto;
            cmbObject.ItemsSource = TatarCulturDbEntities.GetContext().Objects.ToList();


        }



        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentEvent.Name))
                s.AppendLine("Поле название пустое");
            if (null==(_currentEvent.Coin))
                s.AppendLine("Поле стоимости пустое");
            if (_currentEvent.Object == null)
                s.AppendLine("Объект не выбрана");
            if (string.IsNullOrWhiteSpace(_photoName))
                s.AppendLine("Фото не выбрано пустое");
            if (dtStart == null)
                s.AppendLine("Дата начала акции не выброна");
            if (dtEnd == null)
                s.AppendLine("Дата конца акции не выброна");
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

            if (_currentEvent.IdEvent == 0)
            {

                string photo = ChangePhotoName();
                string dest = _currentDirectory + photo;
                File.Copy(_filePath, dest);
                _currentEvent.EventPhoto = photo;
                TatarCulturDbEntities.GetContext().Events.Add(_currentEvent);
            }
            try
            {
                if (_filePath != null)
                {
                    string photo = ChangePhotoName();
                    string dest = _currentDirectory + photo;
                    if (_filePath != _fileCompare)
                    {
                        File.Copy(_filePath, dest);
                        _currentEvent.EventPhoto = photo;
                    }
                }
                _currentEvent.DateStarEvent = dtStart.SelectedDate;
                _currentEvent.DateEndEvent = dtEnd.SelectedDate;
                TatarCulturDbEntities.GetContext().SaveChanges();
                MessageBox.Show("Запись Сохранена");
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void BtnCombClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddTypePage());
        }
    }
}
