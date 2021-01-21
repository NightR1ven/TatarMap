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

            var type = TatarCulturDbEntities.GetContext().Types.OrderBy(p => p.Name).ToList();
            type.Insert(0, new Models.Type
            {
                Name = "Все типы"
            });
            ComboType.ItemsSource = type;
            ComboType.SelectedIndex = 0;
            LViewObject.ItemsSource = TatarCulturDbEntities.GetContext().Objects.OrderBy(p => p.Name).ToList();
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
                _currentObject = _currentObject.Where(p => p.IdType == (ComboType.SelectedItem as Models.Type).IdType).ToList();
            _currentObject = _currentObject.Where(p => p.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();


            LViewObject.ItemsSource = _currentObject;
            TextBlockCount.Text = $"Результат запроса: {_currentObject.Count} записей из {_itemcount}";
        }

        private void LViewObjectSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LViewObject.SelectedIndex >= 0)
            {
                Manager.MainFrame.Navigate(new PageObject(LViewObject.SelectedItem as Models.Object));
            }
        }
    }
}
