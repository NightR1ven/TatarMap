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
    /// Логика взаимодействия для EventPage.xaml
    /// </summary>
    public partial class EventPage : Page
    {
        private Sale _currentSale = new Sale();
        private User _currentUser = new User();
        private Models.Key _currentKey = new Models.Key();
        int _itemcount = 0;
        public EventPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {

            //var type = TatarCulturDbEntities.GetContext().Types.OrderBy(p => p.Name).ToList();
            //type.Insert(0, new Models.Type
            //{
            //    Name = "Все типы"
            //});
            //ComboType.ItemsSource = type;
            //ComboType.SelectedIndex = 0;

            var event1 = TatarCulturDbEntities.GetContext().Events.ToList();

            LViewEvent.ItemsSource = event1;
            _itemcount = LViewEvent.Items.Count;
            //TextBlockCount.Text = $"Результат запроса: {_itemcount} записей из {_itemcount}";


        }

        private void LViewEventSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LViewEvent.SelectedIndex >= 0)
            {
                MessageBoxResult x = MessageBox.Show("Вы действительно купить?",
                    "Выйти", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (x == MessageBoxResult.Yes)
                {
                    var user = Manager.idUser;
                    Event eve = (LViewEvent.SelectedItem as Event);

                    List<User> users = TatarCulturDbEntities.GetContext().Users.ToList();
                    _currentUser = users.FirstOrDefault(p => p.IdUser==user);
                    var keys = TatarCulturDbEntities.GetContext().Keys.Where(p => p.IdEvent == eve.IdEvent).ToList();
                    var key = keys.FirstOrDefault(p=>p.Active==false);

                    var coin = eve.Coin - _currentUser.Coin;

                    StringBuilder s = new StringBuilder();
                    if (_currentUser.Coin == null)
                        s.AppendLine("У вас не хватает монет");
                    if (_currentUser.Coin<eve.Coin)
                        s.AppendLine("У вас не хватает монет");
                    if (_currentUser.Coin==0)
                        s.AppendLine("У вас нет монет");

                    MessageBox.Show($"{_currentUser.Coin}");

                    if (s.Length > 0)
                    {
                        MessageBox.Show(s.ToString());
                        return;
                    }
                    if (_currentSale.IdSale == 0)
                    {
                        _currentSale.SaleDate = DateTime.Now;
                        _currentUser.Coin =_currentUser.Coin-coin;
                        _currentSale.IdKey = key.IdKey;
                        _currentSale.IdUser = user;
                        TatarCulturDbEntities.GetContext().Sales.Add(_currentSale);

                        _currentKey = key;
                        _currentKey.Active = true;
                    }
                    try
                    {

                        TatarCulturDbEntities.GetContext().SaveChanges();
                        MessageBox.Show($"Куплено!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }


                }
            }
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
