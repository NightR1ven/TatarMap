using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
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

namespace TatarCulturaWpf
{
    /// <summary>
    /// Логика взаимодействия для RecoverWindow.xaml
    /// </summary>
    public partial class RecoverWindow : Window
    {
        private User _currentUser = new User();
        int code = 0;
        public RecoverWindow()
        {
            InitializeComponent();
        }


        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {

            StringBuilder s = new StringBuilder();
            if (tbPassword.Text != tbPassword1.Text)
                s.AppendLine("Пароль не совподают");
            if (string.IsNullOrWhiteSpace(tbPassword.Text))
                s.AppendLine("Поле пароль пустое");
            else if (tbPassword.Text.Length <= 7)
                s.AppendLine("Пароль меньше 8 символов");
            if (string.IsNullOrWhiteSpace(tbPassword1.Text))
                s.AppendLine("Повторите пароль");

            if (s.Length > 0)
            {
                MessageBox.Show(s.ToString());
                return;
            }

            try
            {
                _currentUser.Password = tbPassword.Text;

                TatarCulturDbEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            Owner.Show();
            Close();
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

       

        private void BtnCodeClick(object sender, RoutedEventArgs e)
        {

            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(tbCode.Text))
                s.AppendLine("Поле пустое");

            if (s.Length > 0)
            {
                MessageBox.Show(s.ToString());
                return;
            }

            if (Convert.ToInt32(tbCode.Text) == code)
            {
                BtnCode.Visibility = Visibility.Collapsed;
                BtnSave.Visibility = Visibility.Visible;

                lbCode.Visibility = Visibility.Collapsed;
                tbCode.Visibility = Visibility.Collapsed;

                tbPassword.Visibility = Visibility.Visible;
                tbPassword1.Visibility = Visibility.Visible;
                lbPas.Visibility = Visibility.Visible;
                lbPas1.Visibility = Visibility.Visible;
            }
            

        }

        private void BtnSearchClick(object sender, RoutedEventArgs e)
        {
            StringBuilder s = new StringBuilder();
            List<User> users = TatarCulturDbEntities.GetContext().Users.ToList();
            User user = users.FirstOrDefault(p => p.Login == tbMailLogin.Text);
            if (string.IsNullOrWhiteSpace(tbMailLogin.Text))
                s.AppendLine("Поле поиска пустое");
            if (user==null)
            {
                user = users.FirstOrDefault(p => p.Mail == tbMailLogin.Text);
                    
            }
            _currentUser = user;
            if(_currentUser==null)
            s.AppendLine("Такая Почта или Логин не зарегистрирован");
            else if (_currentUser.Mail==null)
            {
                s.AppendLine("Почта не указана");
            }
          
            if (s.Length > 0)
            {
                MessageBox.Show(s.ToString());
                return;
            }

            BtnSearch.Visibility = Visibility.Collapsed;
            BtnCode.Visibility = Visibility.Visible;
            tbCode.Visibility = Visibility.Visible;
            lbCode.Visibility = Visibility.Visible;
            tbMailLogin.Visibility = Visibility.Collapsed;
            lbMailLogin.Visibility = Visibility.Collapsed;

            try
            {

                Random rnd = new Random();
                code = rnd.Next(1000, 9999);

                SmtpClient mySmtpClient = new SmtpClient("smtp.mail.ru");

                mySmtpClient.UseDefaultCredentials = true;
                mySmtpClient.EnableSsl = true;

                System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("tatar.wpf@bk.ru", "Gy1ktau1");
                mySmtpClient.Credentials = basicAuthenticationInfo;


                MailAddress from = new MailAddress("tatar.wpf@bk.ru", "Приложение tatarWpf");
                MailAddress to = new MailAddress(_currentUser.Mail);
                MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

                MailAddress replyTo = new MailAddress("tatar.wpf@bk.ru");
                myMail.ReplyToList.Add(replyTo);

                myMail.Subject = "Код восстановления";
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                myMail.Body = $"<b>Код для регистрации</b><br>Код: <b>{code}</b>";
                myMail.BodyEncoding = System.Text.Encoding.UTF8;

                myMail.IsBodyHtml = true;

                mySmtpClient.Send(myMail);
            }
            catch (SmtpException ex)
            {
                throw new ApplicationException("SmtpException has occured:" + ex.Message);

            }
        }
    }
}
