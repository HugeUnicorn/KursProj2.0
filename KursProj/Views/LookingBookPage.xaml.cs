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
    /// Логика взаимодействия для LookingBookPage.xaml
    /// </summary>
    public partial class LookingBookPage : Page
    {

        private byte[] _mainImageData = null;
        public string img = null;
        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Images\");
        public string selectefFileName;
        public string extension = "";
        public Books currentBook;
        public LookingBookPage(Books cb)
        {
            InitializeComponent();

            currentBook = cb;

            TBBookName.Text = currentBook.name;
            TBArticule.Text = currentBook.article;
            TBDescription.Text = currentBook.description;
            TBAuthor.Text = currentBook.Authors.surname;
            TBGenre.Text = currentBook.Genres.name;
            TBPublisher.Text = currentBook.PublishingHouse.name;
            TBGenre.Text = currentBook.Genres.name;
            TBState.Text = currentBook.State.name;
            if (currentBook.image != null)
            {
                _mainImageData = File.ReadAllBytes(path + currentBook.image);
                ImagePFP.Source = new ImageSourceConverter().ConvertFrom(_mainImageData) as ImageSource;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DataPage());
        }
    }
}
