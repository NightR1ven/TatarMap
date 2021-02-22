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
    /// Логика взаимодействия для EventListPage.xaml
    /// </summary>
    public partial class EventListPage : Page
    {
        int _itemcount = 0;
        private Models.Event _tatEvent = new Models.Event();
        public EventListPage()
        {
            InitializeComponent();

            var type = TatarCulturDbEntities.GetContext().Types.OrderBy(p => p.Name).ToList();
            type.Insert(0, new Models.Type
            {
                Name = "Все типы"
            });
            ComboType.ItemsSource = type;
            ComboType.SelectedIndex = 0;
            EventListDG.ItemsSource = TatarCulturDbEntities.GetContext().Events.OrderBy(p => p.Name).ToList();
            _itemcount = EventListDG.Items.Count;
            TextBlockCount.Text = $"Результат запроса: {_itemcount - 1} записей из {_itemcount - 1}";
        }


        private void EditorKeyClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new KeyListPage((sender as Button).DataContext as Models.Event));
        }

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            var selectedEvent = EventListDG.SelectedItems.Cast<Models.Event>().ToList();
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить {selectedEvent.Count()} записей???", "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {
                    Event x = selectedEvent [0];
                    if(x.Keys.Count>0)
                        throw new Exception("Есть связанные записи c ключами");
                    TatarCulturDbEntities.GetContext().Events.Remove(x);
                    TatarCulturDbEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");

                    List<Event> events =
                    TatarCulturDbEntities.GetContext().Events.OrderBy(p =>
                    p.Name).ToList();
                    EventListDG.ItemsSource = null;
                    EventListDG.ItemsSource = events;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void TBoxSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void ComboTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            var _currentEvent = TatarCulturDbEntities.GetContext().Events.OrderBy(p => p.Name).ToList();
            if (ComboType.SelectedIndex > 0)
                _currentEvent = _currentEvent.Where(p => p.Object.IdType == (ComboType.SelectedItem as Models.Type).IdType).ToList();
            _currentEvent = _currentEvent.Where(p => p.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();


            EventListDG.ItemsSource = _currentEvent;
            TextBlockCount.Text = $"Результат запроса: {_currentEvent.Count} записей из {_itemcount}";
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddObjectPage((sender as Button).DataContext as Models.Object));
        }

        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                TatarCulturDbEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                List<Models.Event> tatEvent = TatarCulturDbEntities.GetContext().Events.OrderBy(p => p.Name).ToList();
                EventListDG.ItemsSource = tatEvent;
            }
        }

        private void EditorClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddObjectPage((sender as Button).DataContext as Models.Object));
        }

        private void btnBackClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
    }
}
