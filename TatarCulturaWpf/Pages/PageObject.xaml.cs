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
    /// Логика взаимодействия для PageObject.xaml
    /// </summary>
    public partial class PageObject : Page
    {
        private Models.Object _currentObject = new Models.Object();

        public PageObject(Models.Object tatObject)
        {
            InitializeComponent();
            if (tatObject != null)
            {
                _currentObject = tatObject;
            }
            this.DataContext = _currentObject;
            ListBoxComment.ItemsSource = TatarCulturDbEntities.GetContext().Comments.Where(p=>p.IdObject==tatObject.IdObject).OrderBy(p => p.IdComment).ToList();
        }

        private void MapZelMouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            Point mousePosition = e.GetPosition(this);
            Location pinLocation = MapZel.ViewportPointToLocation(mousePosition);
            //Pushpin pin = new Pushpin();
            //pin.Location = pinLocation;
            MapZel.Center = pinLocation;
            //MapZel.Children.Add(pin);
        }

        private void MapZelMouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void MapZelMouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;

            Point mousePosition = e.GetPosition(this);
            Location pinLocation = MapZel.ViewportPointToLocation(mousePosition);

            // Pushpin pin = new Pushpin();
            //pin.Location = pinLocation;


            //TextBlockCoords.Text = $"{pinLocation.Latitude}: {pinLocation.Longitude}";
            //MainMap.Children.Add(pin);
        }

        private void PackIconMouseUp(object sender, MouseButtonEventArgs e)
        {
            Location pinLocation = new Location((double)_currentObject.Latitude, (double)_currentObject.Longitude);
            MapZel.Center = pinLocation;
        }

        private void TextBlockCoordsMouseDown(object sender, MouseButtonEventArgs e)
        {

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
