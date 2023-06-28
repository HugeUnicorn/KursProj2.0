using KursProj.Model;
using KursProj.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace KursProj.Windows
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            
            InitializeComponent();

            //UserFrame.Navigate(new Views.DataPage());

            try
            {
                UserFrame.Navigate(new DataPage());
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message, "Что-то пошло не так!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }          
        }

        private void GoMap_Click(object sender, MouseButtonEventArgs e)
        {
            UserFrame.Navigate(new MapPage());
        }
        private void GoBackImage_Click(object sender, MouseEventArgs e)
        {
            if (UserFrame.CanGoBack)
                UserFrame.GoBack();
        }

        private void GoShelf_Click(object sender, MouseButtonEventArgs e)
        {
            UserFrame.Navigate(new BookshelfPage());
        }

        private void GoExit_Click(object sender, MouseButtonEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Window.GetWindow(this).Close();
        }
    }
}
