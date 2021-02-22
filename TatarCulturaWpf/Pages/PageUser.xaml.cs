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
    /// Логика взаимодействия для PageUser.xaml
    /// </summary>
    public partial class PageUser : Page
    {
        private User _currentUser = new User();
        public PageUser(User user)
        {
            InitializeComponent();
            DataContext = user;
            ListBoxKey.Visibility = Visibility.Collapsed;
            ListBoxComment.ItemsSource = TatarCulturDbEntities.GetContext().Comments.Where(p=>p.IdUser==user.IdUser).OrderBy(p => p.IdComment).ToList();
            ListBoxKey.ItemsSource = TatarCulturDbEntities.GetContext().Sales.Where(p => p.IdUser == user.IdUser).OrderBy(p => p.IdSale).ToList();
        }
        
        private void btnBackClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void btnCommentClick(object sender, RoutedEventArgs e)
        {
            ListBoxKey.Visibility = Visibility.Collapsed;
            ListBoxComment.Visibility = Visibility.Visible;
        }

        private void btnKeyClick(object sender, RoutedEventArgs e)
        {
            ListBoxComment.Visibility = Visibility.Collapsed;
            ListBoxKey.Visibility = Visibility.Visible;
        }
    }
}
