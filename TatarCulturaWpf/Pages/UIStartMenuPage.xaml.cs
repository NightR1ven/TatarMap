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

namespace TatarCulturaWpf.Pages
{
    /// <summary>
    /// Логика взаимодействия для UIStartMenuPage.xaml
    /// </summary>
    public partial class UIStartMenuPage : Page
    {
        public UIStartMenuPage(int rols)
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
                        Editor.Visibility = Visibility.Collapsed;
                        UserList.Visibility = Visibility.Collapsed;
                    }
                    break;
            }
           
            
        }


        private void EditorObjectClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddObjectPage(null));
        }

        private void ObjeckListClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ObjectListPage());
        }

        private void AccountClick(object sender, RoutedEventArgs e)
        {

        }

        private void UserListClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new UserListPage() );
        }

        private void MapClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageObject(null));
        }
    }
}
