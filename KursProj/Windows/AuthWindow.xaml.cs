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
using System.Windows.Shapes;

namespace KursProj.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
            AppFrame.frameAuth = AuthFrame;
            AuthFrame.Navigate(new SignInPage());
        }

        private void GoBackImage_Click(object sender, MouseEventArgs e)
        {
            if (AuthFrame.CanGoBack)
                AuthFrame.GoBack();
        }
    }
}
