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
using TatarCulturaWpf.Models;

namespace TatarCulturaWpf.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddKeyPage.xaml
    /// </summary>
    public partial class AddKeyPage : Page
    {
        private Models.Key _currentKey = new Models.Key();
        private Models.Event tatEvent1 = new Models.Event();
        public AddKeyPage(Models.Event tatEvent)
        {
            InitializeComponent();
            tatEvent1 = tatEvent;
            DataContext = _currentKey;
        }
        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentKey.KeyEvent))
                s.AppendLine("Ключ невведен");
            return s;
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            
            StringBuilder _error = CheckFields();
            if (_error.Length > 0)
            {
                MessageBox.Show(_error.ToString());
                return;
            }
            if (_currentKey.IdKey == 0)
            {
                _currentKey.IdEvent = tatEvent1.IdEvent;
                _currentKey.Active = false;
                TatarCulturDbEntities.GetContext().Keys.Add(_currentKey);
            }

            try
            {

                TatarCulturDbEntities.GetContext().SaveChanges();
                MessageBox.Show($"Ключь {_currentKey.KeyEvent} сохранен!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{_currentKey.IdKey} {_currentKey.IdEvent} {_currentKey.Active} {_currentKey.KeyEvent}") ;
                MessageBox.Show(ex.Message.ToString());
            }
            Manager.MainFrame.GoBack();
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
    }
}
