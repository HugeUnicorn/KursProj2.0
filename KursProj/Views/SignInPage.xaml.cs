using KursProj.Model;
using KursProj.Windows;
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

namespace KursProj.Views
{
    /// <summary>
    /// Логика взаимодействия для SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Page
    {
        public SignInPage()
        {
            InitializeComponent();
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(TBLogin.Text) || String.IsNullOrEmpty(TBPassword.Password))
                {
                    MessageBox.Show("Заполните поля!");
                    return;
                }

                var currentUser = AppData.db.User.FirstOrDefault((u) => u.login == TBLogin.Text && u.password == TBPassword.Password);

                if (currentUser == null)
                {
                    MessageBox.Show("Такого пользователя нет!", "Ошибка авторизации");
                }
                else
                {
                    if (currentUser.login.Equals(TBLogin.Text) && currentUser.password.Equals(TBPassword.Password))
                    {
                        AppData.userID = currentUser.id;
                        if (currentUser.roleID == 1)
                        {
                            AdminWindow admin = new AdminWindow(); //currentUser.userID
                            admin.Show();
                        }
                        else
                        {
                            UserWindow userWindow = new UserWindow();
                            userWindow.Show();
                        }                      
                        Window.GetWindow(this).Close();
                    }
                    else
                    {
                        MessageBox.Show("Введите корректные логин и пароль", "Ошибка авторизации");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка " + ex.Message.ToString());
            }
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignUpPage());
        }
    }
}
