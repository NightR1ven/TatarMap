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
        public UserListPage()
        {
            InitializeComponent();

            UserListDG.ItemsSource = TatarCulturDbEntities.GetContext().Users.ToList();
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            var selectedUser = UserListDG.SelectedItems.Cast<User>().ToList();
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить {selectedUser.Count()} записей???", "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {
                    TatarCulturDbEntities.GetContext().Users.RemoveRange(selectedUser);
                    TatarCulturDbEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");
                    List<User> services = TatarCulturDbEntities.GetContext().Users.OrderBy(p => p.IdRols).ToList();
                    UserListDG.ItemsSource = services;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
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
    }
}
