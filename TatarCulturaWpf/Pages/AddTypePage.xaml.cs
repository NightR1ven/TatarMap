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
    /// Логика взаимодействия для AddTypePage.xaml
    /// </summary>
    public partial class AddTypePage : Page
    {
        private Models.Type _currentType = new Models.Type();
        public AddTypePage()
        {
            InitializeComponent();

            DataContext = _currentType;
        }

        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentType.Name))
                s.AppendLine("Название типа невведено!");
            List<Models.Type> types = TatarCulturDbEntities.GetContext().Types.ToList();
            Models.Type type = types.FirstOrDefault(p => p.Name == _currentType.Name);
            if (type != null)
            {
                string name = type.Name;
                if (name == _currentType.Name)
                    s.AppendLine("Такой тип уже есть!");
            }
            return s;
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {

            StringBuilder _error = CheckFields();

            if (_error.Length > 0)
            {
                MessageBox.Show(_error.ToString());
                return;
            }
            if (_currentType.IdType == 0)
            {
                
                TatarCulturDbEntities.GetContext().Types.Add(_currentType);
            }

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
