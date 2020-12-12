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
    /// Логика взаимодействия для CommentObjectPage.xaml
    /// </summary>
    public partial class CommentObjectPage : Page
    {
        private Comment _currrentComment = new Comment();
        private Models.Object _currentObject = new Models.Object();
        public CommentObjectPage(Models.Object tatObject)
        {
            InitializeComponent();
            LoadData(tatObject);


        }


        void LoadData(Models.Object tatObject)
        {
            ObjectListDG.ItemsSource = TatarCulturDbEntities.GetContext().Comments.Where(p => p.IdObject == tatObject.IdObject).OrderBy(p=>p.User.Login).ToList();
        }

        private void btnSaveClick(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteClick(object sender, RoutedEventArgs e)
        {

        }

        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                TatarCulturDbEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            }
        }
    }
}
