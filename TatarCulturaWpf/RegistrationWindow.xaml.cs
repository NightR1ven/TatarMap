using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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

namespace TatarCulturaWpf.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private User _currentUser = new User();
        private string _filePath = null;
        private string _photoName = null;
        private static string _currentDirectory = Directory.GetCurrentDirectory() + @"\ImagesUsers\";
        int code = 0;
        public RegistrationWindow()
        {
            InitializeComponent();
            _photoName = _currentUser.UserPhoto;
            DataContext = _currentUser;
            _currentUser.IdRols = (int)3;

            tbCode.Visibility = Visibility.Collapsed;
            lbCode.Visibility = Visibility.Collapsed;
            BtnSave.Visibility = Visibility.Collapsed;
        }

        string ChangePhotoName()
        {
            string x = _currentDirectory + _photoName;
            string photoname = _photoName;
            int i = 0;
            if (File.Exists(x))
            {
                while (File.Exists(x))
                {
                    i++;
                    x = _currentDirectory + i.ToString() + photoname;
                }
                photoname = i.ToString() + photoname;
            }
            return photoname;
        }


        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbCode.Text))
                MessageBox.Show("Поле пустое");
            if (Convert.ToInt32(tbCode.Text)==code)
            {
                if (_currentUser.IdUser == 0)
                {
                    _currentUser.IdRols = 3;
                    string photo = ChangePhotoName();
                    string dest = _currentDirectory + photo;
                    File.Copy(_filePath, dest);
                    _currentUser.UserPhoto = photo;
                    TatarCulturDbEntities.GetContext().Users.Add(_currentUser);
                }

            }
            else
            {
                MessageBox.Show("Код не верный", "Ошибка");
            }
            try
            {
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

        private void BtnLoadClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //Диалог открытия файла
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif) | *.gif";
                // диалог вернет true, если файл был открыт
                if (op.ShowDialog() == true)
                {
                    // проверка размера файла
                    // по условию файл дожен быть не более 2Мб.
                    FileInfo fileInfo = new FileInfo(op.FileName);
                    if (fileInfo.Length > (1024 * 1024 * 2))
                    {
                        // размер файла меньше 2Мб. Поэтому выбрасывается новое исключение
                        throw new Exception("Размер файла должен быть меньше 2Мб");
                    }
                    imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
                    _photoName = op.SafeFileName;
                    _filePath = op.FileName;
                }
            }
            catch
            {
                MessageBox.Show("Нет файла");
                _filePath = null;
            }
        }

        private void BtnCodeClick(object sender, RoutedEventArgs e)
        {
            List<User> users = TatarCulturDbEntities.GetContext().Users.ToList();
            User user = users.FirstOrDefault(p => p.Login == _currentUser.Login);
            MessageBox.Show(_currentUser.Mail);
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentUser.Login))
                s.AppendLine("Поле логин пустое");
            if (user != null)
            {
                string user1 = user.Login;
                if (user1 == _currentUser.Login)
                    s.AppendLine("Такой Логин уже зарегистрирован");
            }
            User mail = users.FirstOrDefault(p => p.Mail == _currentUser.Mail);
            if (mail != null)
            {
                string mail1 = mail.Mail;
                if (mail1 == _currentUser.Mail)
                    s.AppendLine("Такая Почта уже зарегистрирован");
            }
            if (string.IsNullOrWhiteSpace(_currentUser.Mail))
                s.AppendLine("Поле почта пустое");
            if (tbPassword.Text != tbPassword1.Text)
                s.AppendLine("Пароль не совподают");
            if (string.IsNullOrWhiteSpace(_currentUser.Password))
                s.AppendLine("Поле пароль пустое");
            else if (_currentUser.Password.Length<=7)
                s.AppendLine("Пароль меньше 8 символов");
            if (string.IsNullOrWhiteSpace(tbPassword1.Text))
                s.AppendLine("Повторите пароль");
            if (string.IsNullOrWhiteSpace(_photoName))
                s.AppendLine("фото не выбрано пустое");

            if (s.Length > 0)
            {
                MessageBox.Show(s.ToString());
                return;
            }

            try
            {
                if (!string.IsNullOrEmpty(_currentUser.Mail?.Trim()))
                {
                    const string pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                    var email = _currentUser.Mail.Trim().ToLowerInvariant();

                    if (Regex.Match(email, pattern).Success)
                    {
                        BtnCode.Visibility = Visibility.Collapsed;
                        BtnSave.Visibility = Visibility.Visible;

                        lbCode.Visibility = Visibility.Visible;
                        tbCode.Visibility = Visibility.Visible;

                        tbMail.Visibility = Visibility.Collapsed;
                        tbLogin.Visibility = Visibility.Collapsed;
                        tbPassword.Visibility = Visibility.Collapsed;
                        tbPassword1.Visibility = Visibility.Collapsed;
                        lbLog.Visibility = Visibility.Collapsed;
                        lbPas.Visibility = Visibility.Collapsed;
                        lbPas1.Visibility = Visibility.Collapsed;
                        lbImg.Visibility = Visibility.Collapsed;
                        btnLoad.Visibility = Visibility.Collapsed;
                        lbMail.Visibility = Visibility.Collapsed;
                        imgPhoto.Visibility = Visibility.Collapsed;

                        Random rnd = new Random();
                        code = rnd.Next(1000, 9999);

                        SmtpClient mySmtpClient = new SmtpClient("smtp.mail.ru");

                        mySmtpClient.UseDefaultCredentials = true;
                        mySmtpClient.EnableSsl = true;

                        System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("tatar.wpf@bk.ru", "Gy1ktau1");
                        mySmtpClient.Credentials = basicAuthenticationInfo;


                        MailAddress from = new MailAddress("tatar.wpf@bk.ru", "Приложение tatarWpf");
                        MailAddress to = new MailAddress(tbMail.Text);
                        MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

                        MailAddress replyTo = new MailAddress("tatar.wpf@bk.ru");
                        myMail.ReplyToList.Add(replyTo);

                        myMail.Subject = "Код подтверждения";
                        myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                        myMail.Body = $"<b>Код для регистрации</b><br>Код: <b>{code}</b>";
                        myMail.BodyEncoding = System.Text.Encoding.UTF8;

                        myMail.IsBodyHtml = true;

                        mySmtpClient.Send(myMail);

                        MessageBox.Show("Письмо отправлено! Может быть в спаме проверте!");
                    }
                    else
                    {
                        MessageBox.Show("Почта некорректная");

                    }
                }
               
            }
            catch(SmtpException ex)
            {
                throw new ApplicationException("SmtpException has occured:" + ex.Message);

            }
            
        }
    }
}
