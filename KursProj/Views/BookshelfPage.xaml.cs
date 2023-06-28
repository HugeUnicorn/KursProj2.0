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
using System.Windows.Shapes;

namespace KursProj.Views
{
    /// <summary>
    /// Логика взаимодействия для BookshelfPage.xaml
    /// </summary>
    public partial class BookshelfPage : Page
    {
        public BookshelfPage()
        {
            InitializeComponent();
            Update();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        public void Update()
        {
            var content = AppData.db.Books.Where(x => x.currentOwnerID == AppData.userID).ToList();
            LWBooks.ItemsSource = content;
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var currentBook = button.DataContext as Books;
            NavigationService.Navigate(new LookingBookPage(currentBook));
        }
    }
}
