using KursProj.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Логика взаимодействия для Change.xaml
    /// </summary>
    public partial class Change : Page
    {
        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName)).FullName, @"Images\");
        byte[] _mainImageData = null;
        int anotherOwnerID = 0;
        int currentBookID = 0;
        Books anotherBook;
        User anotherUser;
        Authors anotherAuthor;
        public Change(int ownerID, int bookID)
        {
            InitializeComponent();
            anotherOwnerID = ownerID;
            currentBookID = bookID;
            Update();
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        public void Update()
        {
            var contentShelf = AppData.db.Books.Where(x => x.currentOwnerID == AppData.userID).ToList();
            LWShelf.ItemsSource = contentShelf;

            anotherUser = AppData.db.User.First(y => y.id == anotherOwnerID);
            anotherBook = AppData.db.Books.First(y => y.id == currentBookID);
            anotherAuthor = AppData.db.Authors.First(x => x.id == anotherBook.authorID);

            TBName.Text = anotherAuthor.name;
            TBSurname.Text = anotherAuthor.surname;
            TBPatronymic.Text = anotherAuthor.patronymic;
            TBBookName.Text = anotherBook.name;

            if (anotherBook.image != null)
            {
                _mainImageData = File.ReadAllBytes(path + anotherBook.image);
                AnotherBookImage.Source = new ImageSourceConverter().ConvertFrom(_mainImageData) as ImageSource;
            }
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var currentBook = button.DataContext as Books;
            NavigationService.Navigate(new LookingBookPage(currentBook));
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            var booksFromShelf = LWShelf.SelectedItems.Cast<Books>().ToList();
            

            if (MessageBox.Show("Вы уверены что хотите обменяться?") == MessageBoxResult.OK && booksFromShelf.Count == 1)
            {
                try
                {
                    foreach (var book in booksFromShelf)
                    { 
                        book.currentOwnerID = anotherOwnerID;

                        UserBookPair pair = new UserBookPair()
                        {
                            userID = anotherOwnerID,
                            bookID = book.id,
                            time = DateTime.Now,
                        };
                        AppData.db.UserBookPair.Add(pair);
                        AppData.db.SaveChanges();
                    }

                    anotherBook.currentOwnerID = AppData.userID;

                    UserBookPair pair2 = new UserBookPair()
                    {
                        userID = AppData.userID,
                        bookID = anotherBook.id,
                        time = DateTime.Now,
                    };
                    AppData.db.UserBookPair.Add(pair2);
                    AppData.db.SaveChanges();
                    MessageBox.Show("Вы успешно обменялись");
                    NavigationService.Navigate(new BookshelfPage());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
