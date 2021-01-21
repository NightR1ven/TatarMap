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
    /// Логика взаимодействия для AddCommentsPage.xaml
    /// </summary>
    public partial class AddCommentsPage : Page
    {
        private Comment _comment = new Comment();
        public AddCommentsPage()
        {
            InitializeComponent();

            ComboBoxRating.Items.Add(5);
            ComboBoxRating.Items.Add(4);
            ComboBoxRating.Items.Add(3);
            ComboBoxRating.Items.Add(2);
            ComboBoxRating.Items.Add(1);
            ComboBoxRating.Items.Add(0);

            DataContext = _comment;
            //MessageBox.Show($"{comment}");
            
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(tbComment.Text))
                s.AppendLine("Поле название пустое");

            _comment.IdObject = Manager.idObject;
            _comment.IdUser = Manager.idUser;
            _comment.Star = (int)ComboBoxRating.SelectedItem;

            if (_comment.IdComment == 0)
                TatarCulturDbEntities.GetContext().Comments.Add(_comment);

            try
            {
                TatarCulturDbEntities.GetContext().SaveChanges();
                MessageBox.Show("Запись Изменена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            Manager.MainFrame.GoBack();
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
    }
}
