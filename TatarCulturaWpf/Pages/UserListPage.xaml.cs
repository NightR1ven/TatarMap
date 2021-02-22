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
using TatarCulturaWpf.Pages;

namespace TatarCulturaWpf
{
    /// <summary>
    /// Логика взаимодействия для UserListPage.xaml
    /// </summary>
    public partial class UserListPage : Page
    {
        int _itemcount = 0;
        public UserListPage()
        {
            InitializeComponent();

            var type = TatarCulturDbEntities.GetContext().UserRols.OrderBy(p => p.NameRols).ToList();
            type.Insert(0, new Models.UserRol
            {
                NameRols = "Все типы"
            });
            ComboType.ItemsSource = type;
            ComboType.SelectedIndex = 0;
            UserListDG.ItemsSource = TatarCulturDbEntities.GetContext().Users.ToList();
            _itemcount = UserListDG.Items.Count;
            TextBlockCount.Text = $"Результат запроса: {_itemcount-1} записей из {_itemcount-1}";
        }


        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            var selectedUser = UserListDG.SelectedItems.Cast<User>().ToList();
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить {selectedUser.Count()} записей???", "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {
                    User x = selectedUser[0];
                    if (x.Sales.Count > 0)
                        throw new Exception("Есть связанные записи c истории");
                    if (x.Comments.Count > 0)
                        throw new Exception("Есть связанные записи c комментариями");
                    TatarCulturDbEntities.GetContext().Users.Remove(x);
                    TatarCulturDbEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");

                    List<User> users =TatarCulturDbEntities.GetContext().Users.OrderBy(p =>p.Login).ToList();
                    UserListDG.ItemsSource = null;
                    UserListDG.ItemsSource = users;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void UpdateData()
        {
            var _currentUser = TatarCulturDbEntities.GetContext().Users.OrderBy(p => p.IdRols).ToList();
            if (ComboType.SelectedIndex > 0)
                _currentUser = _currentUser.Where(p => p.IdRols == (ComboType.SelectedItem as UserRol).IdRols).ToList();
            _currentUser = _currentUser.Where(p => p.Login.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();


            UserListDG.ItemsSource = _currentUser;
            TextBlockCount.Text = $"Результат запроса: {_currentUser.Count} записей из {_itemcount-1}";
        }

        private void TBoxSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void ComboRolSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void EditorClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddUserPage((sender as Button).DataContext as User));
        }


        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (Visibility == Visibility.Visible)
            {
                TatarCulturDbEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                UserListDG.ItemsSource = TatarCulturDbEntities.GetContext().Users.OrderBy(p => p.Login).ToList();
            }
        }

        private void btnBackClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddUserPage((sender as Button).DataContext as User));
        }
    }
}
