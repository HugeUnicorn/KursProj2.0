using KursProj.Model;
using System;
using System.Collections.Generic;
using System.IO;
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


namespace KursProj.Views
{
    /// <summary>
    /// Логика взаимодействия для BookPage.xaml
    /// </summary>
    public partial class BookPage : Page
    {
        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Images\");
        public string img;
        public BookPage()
        {
            InitializeComponent();
            Update();
        }

        public void Update()
        {
            var content = AppData.db.Books.ToList();
            LWBooks.ItemsSource = content;

        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var currentBook = button.DataContext as Books;
            NavigationService.Navigate(new AddEditBookPage(currentBook));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentBook = (sender as Button).DataContext as Books;
            if (MessageBox.Show("Вы уверены что хотите удалить эту книгу?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                AppData.db.Books.Remove(currentBook);
                AppData.db.SaveChanges();
                Update();
            }
        }
    }
}
