using KursProj.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Windows.Navigation;

namespace KursProj.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditUserPage.xaml
    /// </summary>
    public partial class AddEditUserPage : Page
    {

        private byte[] _mainImageData = null;
        public string img = null;
        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Images\");
        public string selectefFileName;
        public string extension = "";
        public User currentUser;

        Regex lpCheck = new Regex(@"^\w{5,30}$");
        Regex nameCheck = new Regex(@"^[A-ЯЁ][а-яё]+$");
        Regex emailCheck = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        MatchCollection matches;
        public AddEditUserPage()
        {
            InitializeComponent();

            var roles = AppData.db.Role.Select(r => r.name).ToList();
            CBRole.ItemsSource = roles;
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
        public AddEditUserPage(User cu)
        {
            currentUser = cu;

            InitializeComponent();

            TBName.Text = currentUser.name;
            TBSurname.Text = currentUser.surname;
            TBPatronymic.Text = currentUser.patronymic;
            TBLogin.Text = currentUser.login;   
            PBPass.Password = currentUser.password;
            TBemail.Text = currentUser.email;
            CBRole.Text = AppData.db.Role.Where(r => r.id == currentUser.roleID).Select(r => r.name).FirstOrDefault();
            if (currentUser.image != null)
            {
                _mainImageData = File.ReadAllBytes(path + currentUser.image);
                ImagePFP.Source = new ImageSourceConverter().ConvertFrom(_mainImageData) as ImageSource;
            }

            var roles = AppData.db.Role.Select(r => r.name).ToList();
            CBRole.ItemsSource = roles;
        }

        private void PBPass_Changed(object sender, RoutedEventArgs e)
        {
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {

            var role = AppData.db.Role.Where(r => r.name == CBRole.SelectedItem.ToString()).FirstOrDefault();

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
            if (AppData.db.User.Count(x => x.login == TBLogin.Text) > 0)
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
                if (currentUser == null)
                {
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
                }
                else
                {
                    currentUser.name = TBName.Text;
                    currentUser.surname= TBSurname.Text;
                    currentUser.patronymic= TBPatronymic.Text;
                    currentUser.roleID = role.id;
                    currentUser.login = TBLogin.Text;
                    currentUser.password = PBPass.Password;
                    currentUser.image = img;
                    currentUser.email = TBemail.Text;

                    AppData.db.SaveChanges();
                    MessageBox.Show("Пользователь успешно обновлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                
                
                
                NavigationService.Navigate(new UsersPage());
            }
            catch
            {
                MessageBox.Show("Ошибка в добавлении данных!");
            }
        }
    }
}
