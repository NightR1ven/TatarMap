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
    /// Логика взаимодействия для KeyListPage.xaml
    /// </summary>
    public partial class KeyListPage : Page
    {
        public KeyListPage(Models.Event tatEvent)
        {
            InitializeComponent();
            //LoadData(tatEvent);
            //_itemcount = CommentListDG.Items.Count;
            //TextBlockCount.Text = $"Результат запроса: {_itemcount - 1} записей из {_itemcount - 1}";
        }

        private void UpdateData()
        {
            var _currentComment = TatarCulturDbEntities.GetContext().Events.OrderBy(p => p.IdObject).ToList();
           // _currentComment = _currentComment.Where(p => p.User.Login.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();


            //CommentListDG.ItemsSource = _currentComment;
            //TextBlockCount.Text = $"Результат запроса: {_currentComment.Count} записей из {_itemcount - 1}";
        }
    }
}
