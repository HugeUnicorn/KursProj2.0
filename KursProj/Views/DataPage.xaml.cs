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
    /// Логика взаимодействия для DataPage.xaml
    /// </summary>
    public partial class DataPage : Page
    {
        public DataPage()
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
            var content = AppData.db.Books.ToList();
            LWBooks.ItemsSource = content;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var currentBook = button.DataContext as Books;
            NavigationService.Navigate(new LookingBookPage(currentBook));
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var currentBook = button.DataContext as Books;

            if (currentBook.currentOwnerID == AppData.userID)
            {
                MessageBox.Show("Эта книга уже у вас на полке!");
            }
            else
            {
                if (currentBook.currentOwnerID != null)
                {
                    int currenOwner = Int32.Parse(currentBook.currentOwnerID.ToString());
                    NavigationService.Navigate(new Change(currenOwner, currentBook.id));
                }
                else if(MessageBox.Show("У этой книги нет хоязина, хотите забрать ее?") == MessageBoxResult.OK)      
                {
                    UserBookPair pair = new UserBookPair()
                    {
                        bookID = currentBook.id,
                        userID = AppData.userID,
                        time = DateTime.Now
                    };
                    currentBook.currentOwnerID = AppData.userID;
                    AppData.db.UserBookPair.Add(pair);
                    AppData.db.SaveChanges();
                    MessageBox.Show("Книга добавлена на вашу полку");
                }
            }
        }
    }
}
