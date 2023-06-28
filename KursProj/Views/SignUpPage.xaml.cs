using KursProj.Model;
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
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.IO;

namespace KursProj.Views
{
    /// <summary>
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        private byte[] _mainImageData = null;
        public string img = null;
        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Images\");
        public string selectefFileName;
        public string extension = "";

        Regex lpCheck = new Regex(@"^\w{5,30}$");
        Regex nameCheck = new Regex(@"^[A-ЯЁ][а-яё]+$");
        Regex emailCheck = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        MatchCollection matches;
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            
            matches = nameCheck.Matches(TBName.Text);
            if (matches.Count > 0)
            {
                MessageBox.Show("Некорректно введено имя");
                return;
            }
            matches = emailCheck.Matches(TBemail.Text);
            if (matches.Count == 0)
            {
                MessageBox.Show("Некорректно введен email");
                return;
            }
            if (AppData.db.User.Count(x => x.login==TBLogin.Text) > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже существует!");
            }
            try
            {
                if (img != null)
                {
                    img = TBLogin.Text + extension;
                    int a = 0;
                    while (File.Exists(path + img))
                    {
                        a++;
                        img = TBLogin.Text + $" ({a})" + extension;
                    }
                    path += img;
                    File.Copy(selectefFileName, path);
                }
                User user = new User();
                { 
                    user.name = TBName.Text;
                    user.surname = TBSurname.Text;
                    user.patronymic = TBPatronymic.Text;
                    user.email = TBemail.Text;
                    user.login = TBLogin.Text;
                    user.password = PBPass.Password;
                    user.image = img;
                    user.roleID = 2; //default user & not admin
                };
                AppData.db.User.Add(user);
                AppData.db.SaveChanges();
                MessageBox.Show("Пользователь успешно добавлен!");
                NavigationService.GoBack();
            }
            catch 
            {
                MessageBox.Show("Ошибка в добавлении данных!");
            }
        }

        private void PBPass_Changed(object sender, RoutedEventArgs e)
        {
            string password = PBPass.Password.ToString();
            Regex strongPass = new Regex(@"(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{6,}");
            MatchCollection strongMatch = strongPass.Matches(password);
            if (strongMatch.Count > 0)
            {
                TBPassStrength.Text = "Надежный пароль";
                TBPassStrength.Foreground = Brushes.DarkRed;
                TBPassStrength.Visibility = Visibility.Visible;
            }            
            else
            {
                TBPassStrength.Text = "слабый пароль";
                TBPassStrength.Foreground = Brushes.Green;
                TBPassStrength.Visibility = Visibility.Visible;
            }

            if (PBPass.Password != PBPassAgain.Password)
            {
                BtnReg.IsEnabled = false;
                PBPass.Background = Brushes.LightCoral;
                PBPass.Foreground = Brushes.Red;
                PBPassAgain.Background = Brushes.LightCoral;
                PBPassAgain.Foreground = Brushes.Red;
            }
            else if (String.IsNullOrEmpty(PBPass.Password.ToString()) || String.IsNullOrEmpty(PBPassAgain.Password.ToString()))
            {
                BtnReg.IsEnabled = false;
                PBPass.Background = default;
                PBPass.Foreground = default;
                PBPassAgain.Background = default;
                PBPassAgain.Foreground = default;
                TBPassStrength.Visibility = Visibility.Collapsed;
            }
            else
            {
                BtnReg.IsEnabled = true;
                PBPass.Background = Brushes.LightGreen;
                PBPass.Foreground = Brushes.Green;
                PBPassAgain.Background = Brushes.LightGreen;
                PBPassAgain.Foreground = Brushes.Green;
            }
        }

        private void TBLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AppData.db.User.Count(x => x.login == TBLogin.Text) > 0)
            {
                TBLoginError.Visibility = Visibility.Visible;
            }
            else
            {
                TBLoginError.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Фото | *.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                img = Path.GetFileName(ofd.FileName);
                extension = Path.GetExtension(img);
                selectefFileName = ofd.FileName;
                _mainImageData = File.ReadAllBytes(ofd.FileName);
                ImagePFP.Source = new ImageSourceConverter()
                    .ConvertFrom(_mainImageData) as ImageSource;
            }
        }
    }
}
