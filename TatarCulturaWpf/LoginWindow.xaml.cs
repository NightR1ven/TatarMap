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
using TatarCulturaWpf.Pages;


namespace TatarCulturaWpf
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            TbLogin.Text = "admin";
            PbPassword.Password = "admin";
        }


        private void BtnEnterClick(object sender, RoutedEventArgs e)
        {
            List<User> users = TatarCulturDbEntities.GetContext().Users.ToList();
            User user = users.FirstOrDefault(p => p.Login == TbLogin.Text && p.Password == PbPassword.Password);
            
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(TbLogin.Text))
                error.AppendLine("Введите логин");

            if (string.IsNullOrWhiteSpace(PbPassword.Password))
                error.AppendLine("Введите пароль");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString(), "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            

            if (user != null)
            {   
                int rols = (int)user.IdRols;

                
                MainWindow mainWindow = new MainWindow(rols);
                mainWindow.Owner = this;
                this.Hide();
                mainWindow.Show();
            }

            else if (user == null)
            {
                MessageBox.Show("Неверный пароль или логин", "Неверный пароль или логин", MessageBoxButton.OK, MessageBoxImage.Error);
                PbPassword.Clear();
            }

        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Close();
        }


        private void WindowIsVisibalChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            string log = TbLogin.Text;
            string pas = PbPassword.Password;
            if (CheckerBox.IsChecked == true)
            {
                TbLogin.Text = log;
                PbPassword.Password = pas;
            }
            //else
            //{
            //    TbLogin.Text = "";
            //    PbPassword.Password = "";
            //}
        }

        private void BtnRegistrationClick(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Owner = this;
            this.Hide();
            registrationWindow.Show();
        }
    }
}
