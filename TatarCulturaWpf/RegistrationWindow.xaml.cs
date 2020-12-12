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
using System.Windows.Shapes;
using TatarCulturaWpf.Models;

namespace TatarCulturaWpf.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private User _currentUser = new User();
        public RegistrationWindow()
        {
            InitializeComponent();
            DataContext = _currentUser;
            cmbRol.ItemsSource = TatarCulturDbEntities.GetContext().UserRols.ToList();
        }


        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentUser.Login))
                s.AppendLine("Поле логин пустое");
            if (tbPassword.Text != tbPassword1.Text)
                s.AppendLine("Пароль не совподают");
            if (string.IsNullOrWhiteSpace(_currentUser.Password))
                s.AppendLine("Поле пароль пустое");
            if (string.IsNullOrWhiteSpace(tbPassword1.Text))
                s.AppendLine("Повторите пароль");
            if (_currentUser.UserRol == null)
                s.AppendLine("Категория не выбрана");

            if (s.Length > 0)
            {
                MessageBox.Show(s.ToString());
                return;
            }
            if (_currentUser.IdUser == 0)
                TatarCulturDbEntities.GetContext().Users.Add(_currentUser);
            try
            {
                
                TatarCulturDbEntities.GetContext().SaveChanges();
                MessageBox.Show("Запись Изменена");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            Owner.Show();
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Close();
        }


        private void WindowClosed(object sender, EventArgs e)
        {
           Owner.Show();
        }


        private void WindowIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (Visibility == Visibility.Visible)
            {
                TatarCulturDbEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            }
        }
    }
}
