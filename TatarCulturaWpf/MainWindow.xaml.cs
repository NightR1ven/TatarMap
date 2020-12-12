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
using System.Windows.Threading;
using TatarCulturaWpf.Pages;

namespace TatarCulturaWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int rols1;
        public MainWindow(int rols)
        {
            InitializeComponent();
            MainFrame.Navigate(new UIStartMenuPage(rols));
            Manager.MainFrame = MainFrame;
            rols1 = rols;
            BtnActive.Visibility = Visibility.Collapsed;
            if (Manager.MainFrame.CanGoBack == true)
            {
                BtnBack.Visibility = Visibility.Visible;
            }
            else
                BtnBack.Visibility = Visibility.Collapsed;

        }

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ListViewObjectPage());
            Manager.MainFrame = MainFrame;

            BtnProfile.Visibility = Visibility.Collapsed;
            BtnObject.Visibility = Visibility.Collapsed;
            BtnListObject.Visibility = Visibility.Collapsed;
            BtnListUsers.Visibility = Visibility.Collapsed;
            if (Manager.MainFrame.CanGoBack == true)
            {
                BtnBack.Visibility = Visibility.Visible;

            }
            else
                BtnBack.Visibility = Visibility.Collapsed;
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsVisible==true)
            {
                MessageBoxResult x = MessageBox.Show("Вы действительно хотите выйти?",
                    "Выйти", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (x == MessageBoxResult.No)
                    e.Cancel = true;
            }

           
        }

        private void BtnMenuOpenClick(object sender, RoutedEventArgs e)
        {
            BtnMenuClose.Visibility = Visibility.Visible;
            BtnMenuOpen.Visibility = Visibility.Collapsed;
            TextBlockMain.Visibility = Visibility.Visible;
        }

        private void BtnMenuCloseClick(object sender, RoutedEventArgs e)
        {
            BtnMenuOpen.Visibility = Visibility.Visible;
            BtnMenuClose.Visibility = Visibility.Collapsed;
            TextBlockMain.Visibility = Visibility.Collapsed;

        }


        private void BtnListUsersClick(object sender, RoutedEventArgs e)
        {

            Manager.MainFrame.Navigate(new UserListPage());
            BtnMenuClose.Visibility = Visibility.Collapsed;
            BtnMenuOpen.Visibility = Visibility.Visible;
        }

        private void BtnObjectClick(object sender, RoutedEventArgs e)
        {
            TextBlockMain.Visibility = Visibility.Hidden;
            MainFrame.Navigate(new ObjectListPage());
        }

        private void BtnExitClick(object sender, RoutedEventArgs e)
        {
            Close();
            Application.Current.Shutdown();


        }


        private void WindowIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            switch (rols1)
            {
                case 2:
                    BtnListUsers.Visibility = Visibility.Collapsed;
                    break;

                case 3:
                    {
                        BtnListUsers.Visibility = Visibility.Collapsed;
                        BtnListObject.Visibility = Visibility.Collapsed;
                        break;
                    }
                    
            }
        }

        private void BtnListObjectClick(object sender, RoutedEventArgs e)
        {
            TextBlockMain.Visibility = Visibility.Hidden;
        }

        private void BtnProfileClick(object sender, RoutedEventArgs e)
        {
            TextBlockMain.Visibility = Visibility.Hidden;
        }

        private void BtnActiveClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Owner = this;
            this.Hide();
            loginWindow.Show();
        }

        private void BtnBackClick(object sender, RoutedEventArgs e)
        {
                Manager.MainFrame.GoBack();
        }
    }
}
