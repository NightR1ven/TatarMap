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
    /// Логика взаимодействия для UIStartMenuPage.xaml
    /// </summary>
    public partial class UIStartMenuPage : Page
    {
        User user1;
        int rols1;
        public UIStartMenuPage(int rols, User user)
        {
            InitializeComponent();
            switch (rols)
            {
                case 1:
                    {
                        
                    }
                    break;
                case 2:
                    {
                        UserList.Visibility = Visibility.Collapsed;

                    }
                    break;
                case 3:
                    {
                        UserList.Visibility = Visibility.Collapsed;
                    }
                    break;
            }

            user1 = user;
            rols1 = rols;
        }


        private void EditorObjectClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddObjectPage(null));
        }

        private void ObjeckListClick(object sender, RoutedEventArgs e)
        {
            if(rols1<3)
            Manager.MainFrame.Navigate(new ObjectListPage());
            else
            Manager.MainFrame.Navigate(new ListViewObjectPage());
        }

        private void AccountClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageUser(user1));
        }

        private void UserListClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new UserListPage() );
        }

        private void EventListClick(object sender, RoutedEventArgs e)
        {
            if (rols1 < 3)
                Manager.MainFrame.Navigate(new EventPage());
            else
                Manager.MainFrame.Navigate(new EventListPage());
        }
    }
}
