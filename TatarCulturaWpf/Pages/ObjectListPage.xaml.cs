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
    /// Логика взаимодействия для ObjectListPage.xaml
    /// </summary>
    public partial class ObjectListPage : Page
    {
        int _itemcount = 0;
        private Models.Object _tatObject = new Models.Object();
        public ObjectListPage()
        {
            InitializeComponent();

            var type = TatarCulturDbEntities.GetContext().Types.OrderBy(p => p.Name).ToList();
            type.Insert(0, new Models.Type
            {
                Name = "Все типы"
            });
            ComboType.ItemsSource = type;
            ComboType.SelectedIndex = 0;
            ObjectListDG.ItemsSource = TatarCulturDbEntities.GetContext().Objects.OrderBy(p => p.Name).ToList();
            _itemcount = ObjectListDG.Items.Count;
            TextBlockCount.Text = $"Результат запроса: {_itemcount-1} записей из {_itemcount-1}";
        }

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            var selectedObject = ObjectListDG.SelectedItems.Cast<Models.Object>().ToList();
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить {selectedObject.Count()} записей???", "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {
                    TatarCulturDbEntities.GetContext().Objects.RemoveRange(selectedObject);
                    TatarCulturDbEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");
                    List<Models.Object> services = TatarCulturDbEntities.GetContext().Objects.OrderBy(p => p.Name).ToList();
                    ObjectListDG.ItemsSource = services;
                    if(ObjectListDG.ItemsSource!=null)
                    {
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
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
            var _currentObject = TatarCulturDbEntities.GetContext().Objects.OrderBy(p => p.Name).ToList();
            if (ComboType.SelectedIndex > 0)
                _currentObject = _currentObject.Where(p => p.IdType == (ComboType.SelectedItem as Models.Type).IdType).ToList();
            _currentObject = _currentObject.Where(p => p.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();


            ObjectListDG.ItemsSource = _currentObject;
            TextBlockCount.Text = $"Результат запроса: {_currentObject.Count} записей из {_itemcount}";
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddObjectPage((sender as Button).DataContext as Models.Object));
        }

        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility==Visibility.Visible)
            { 
            TatarCulturDbEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            List<Models.Object> tatObjects = TatarCulturDbEntities.GetContext().Objects.OrderBy(p => p.Name).ToList();
            ObjectListDG.ItemsSource = tatObjects;
            }
        }

        private void EditorClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddObjectPage((sender as Button).DataContext as Models.Object));
        }

        private void EditorCommentClick(object sender, RoutedEventArgs e)
        {
            
            Manager.MainFrame.Navigate(new CommentObjectPage((sender as Button).DataContext as Models.Object));
        }

        private void btnBackClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
    }
}
