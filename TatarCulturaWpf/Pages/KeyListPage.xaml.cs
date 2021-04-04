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
    /// Логика взаимодействия для KeyListPage.xaml
    /// </summary>
    public partial class KeyListPage : Page
    {
        private Models.Key _currentKey = new Models.Key();
        Models.Event tatEvent1 = new Models.Event();
        public KeyListPage(Models.Event tatEvent)
        {
            InitializeComponent();
            tatEvent1 = tatEvent;
            KeyListDG.ItemsSource = TatarCulturDbEntities.GetContext().Keys.Where(p => p.IdEvent == tatEvent.IdEvent).OrderBy(p=>p.KeyEvent).ToList();
            DataContext = _currentKey;
        }

        private void btnDeleteClick(object sender, RoutedEventArgs e)
        {
            var selectedKey = KeyListDG.SelectedItems.Cast<Models.Key>().ToList();
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить {selectedKey.Count()} записей???", "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {
                    TatarCulturDbEntities.GetContext().Keys.RemoveRange(selectedKey);
                    TatarCulturDbEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");
                    List<Models.Key> key = TatarCulturDbEntities.GetContext().Keys.OrderBy(p => p.KeyEvent).ToList();
                    KeyListDG.ItemsSource = key;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnBackClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddKeyPage(tatEvent1));
        }

        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TatarCulturDbEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            List<Models.Key> tatKey = TatarCulturDbEntities.GetContext().Keys.Where(p => p.IdEvent == tatEvent1.IdEvent).OrderBy(p => p.KeyEvent).ToList();
            KeyListDG.ItemsSource = tatKey;
        }
    }
}
