using Microsoft.Maps.MapControl.WPF;
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
    /// Логика взаимодействия для ListViewObjectPage.xaml
    /// </summary>
    public partial class ListViewObjectPage : Page
    {
        User user1;
        int _itemcount = 0;
        public ListViewObjectPage()
        {
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
        {

            Location pinLocation = new Location(55.7887, 49.1221);
            MapTat.Center = pinLocation;

            var type = TatarCulturDbEntities.GetContext().Types.OrderBy(p => p.Name).ToList();
            type.Insert(0, new Models.Type
            {
                Name = "Все типы"
            });
            ComboType.ItemsSource = type;
            ComboType.SelectedIndex = 0;
            
            var obj= TatarCulturDbEntities.GetContext().Objects.ToList();

            LViewObject.ItemsSource = obj;
            _itemcount = LViewObject.Items.Count;
            TextBlockCount.Text = $"Результат запроса: {_itemcount} записей из {_itemcount}";


        }

       

        private void TBoxSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void ComboTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                TatarCulturDbEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                LViewObject.ItemsSource = TatarCulturDbEntities.GetContext().Objects.OrderBy(p => p.Name).ToList();
            }
        }
        
        private void UpdateData()
        {
           


            var _currentObject = TatarCulturDbEntities.GetContext().Objects.OrderBy(p => p.Name).ToList();
            if (ComboType.SelectedIndex > 0)
            {
                _currentObject = _currentObject.Where(p => p.IdType == (ComboType.SelectedItem as Models.Type).IdType).ToList();
            }
               
            _currentObject = _currentObject.Where(p => p.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            MapTat.Children.Clear();
            foreach (Models.Object obj in _currentObject)
            {
                Location pinLocation = new Location((double)obj.Latitude, (double)obj.Longitude);
                Location pinLocationCenter = new Location(55.60307988301807, 49.586290315150926);
                var pin = new Pushpin();
                pin.ToolTip = $"{obj.Name}";
                pin.Location = pinLocation;
                
                MapTat.Children.Add(pin);
                MapTat.ZoomLevel = 8;
                MapTat.Center = pinLocationCenter;
            }
            LViewObject.ItemsSource = _currentObject;
            TextBlockCount.Text = $"Результат запроса: {_currentObject.Count} записей из {_itemcount}";
        }

        private void LViewObjectSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LViewObject.SelectedIndex >= 0)
            {
                MapTat.Children.Clear();
                StackInfo.Visibility = Visibility.Visible;
                Models.Object obj = (LViewObject.SelectedItem as Models.Object);
                var i = TatarCulturDbEntities.GetContext().Objects.Where(x => x.IdObject == obj.IdObject).ToList();
                Location pinLocation = new Location((double)obj.Latitude, (double)obj.Longitude);
                Pushpin pin = new Pushpin();
                pin.Location = pinLocation;
               
                MapTat.Children.Add(pin);
                MapTat.ZoomLevel = 15;
                MapTat.Center = pinLocation;
                this.DataContext = obj;
                if (obj.Description == null)
                { textblockInfo.Text = "Нет информации"; }
                else if (obj.Description.Length>250)
                {
                    int text= obj.Description.Length - 250;
                    var des = obj.Description;
                    des = des.Substring(0, des.Length - text);
                    des = des.Insert(des.Length, " ...");
                    textblockInfo.Text = des;
                }
                else
                {
                    textblockInfo.Text = obj.Description;
                }
                
                

            }
            else
            StackInfo.Visibility = Visibility.Collapsed;
        }

        private void MapZelMouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            Point mousePosition = e.GetPosition(this);
            Location pinLocation = MapTat.ViewportPointToLocation(mousePosition);
            MapTat.Center = pinLocation;
        }

        private void MapZelMouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void MapZelMouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;

            Point mousePosition = e.GetPosition(this);
            Location pinLocation = MapTat.ViewportPointToLocation(mousePosition);

        }

        private void PackIconMouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }



        private void PackIconMouseUpMenu(object sender, MouseButtonEventArgs e)
        {
            GridLength x = new GridLength(0, GridUnitType.Star);
            if (gridWidth.Width==x)
            {
                gridWidth.Width = new GridLength(5, GridUnitType.Star);
            }
            else
            gridWidth.Width = new GridLength(0, GridUnitType.Star);

           
        }

        private void btnObjectClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageObject((sender as Button).DataContext as Models.Object));
        }
    }
}
