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
    /// Логика взаимодействия для AddEditBookPage.xaml
    /// </summary>
    public partial class AddEditBookPage : Page
    {
        private byte[] _mainImageData = null;
        public string img = null;
        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Images\");
        public string selectefFileName;
        public string extension = "";
        public Books currentBook;

        public AddEditBookPage()
        {
            InitializeComponent();

            var authors = AppData.db.Authors.Select(r => r.surname).ToList();
            TBAuthor.ItemsSource = authors;
            var genres = AppData.db.Genres.Select(r => r.name).ToList();
            TBGenre.ItemsSource = genres;
            var publishers = AppData.db.PublishingHouse.Select(r => r.name).ToList();
            TBPublisher.ItemsSource = publishers;
            var states = AppData.db.State.Select(r => r.name).ToList();
            TBState.ItemsSource = states;
        }
        public AddEditBookPage(Books cb)
        {
            InitializeComponent();

            currentBook = cb;

            TBBookName.Text = currentBook.name;
            TBArticule.Text = currentBook.article;
            TBDescription.Text = currentBook.description;
            TBAuthor.SelectedItem = currentBook.Authors.surname;
            TBGenre.SelectedItem = currentBook.Genres.name;
            TBPublisher.SelectedItem = currentBook.PublishingHouse.name;
            TBGenre.SelectedItem = currentBook.Genres.name;
            TBState.SelectedItem = currentBook.State.name;

            if (currentBook.image != null)
            {
                _mainImageData = File.ReadAllBytes(path + currentBook.image);
                ImagePFP.Source = new ImageSourceConverter().ConvertFrom(_mainImageData) as ImageSource;
            }

            var authors = AppData.db.Authors.Select(r => r.surname).ToList();
            TBAuthor.ItemsSource = authors;
            var genres = AppData.db.Genres.Select(r => r.name).ToList();
            TBGenre.ItemsSource = genres;
            var publishers = AppData.db.PublishingHouse.Select(r => r.name).ToList();
            TBPublisher.ItemsSource = publishers;
            var states = AppData.db.State.Select(r => r.name).ToList();
            TBState.ItemsSource = states;          
        }

        private void BtnImage_Click(object sender, RoutedEventArgs e)
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
            if (img != null)
            {
                img = TBArticule.Text + extension;
                int a = 0;
                while (File.Exists(path + img))
                {
                    a++;
                    img = TBArticule.Text + $" ({a})" + extension;
                }
                path += img;
                File.Copy(selectefFileName, path);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var author = AppData.db.Authors.Where(a => a.surname == TBAuthor.SelectedItem.ToString()).FirstOrDefault();
            var genre = AppData.db.Genres.Where(a => a.name == TBGenre.SelectedItem.ToString()).FirstOrDefault();
            var publisher = AppData.db.PublishingHouse.Where(a => a.name == TBPublisher.SelectedItem.ToString()).FirstOrDefault();
            var state = AppData.db.State.Where(a => a.name == TBState.SelectedItem.ToString()).FirstOrDefault();

            if (AppData.db.Books.Count(x => x.article == TBArticule.Text) > 0)
            {
                MessageBox.Show("Такая книга уже есть");
                return;
            }
            if (currentBook == null )
            {
                Books book = new Books()
                {
                    article = TBArticule.Text,
                    name = TBBookName.Text,
                    yearOfPublic = Int32.Parse(TBYoP.Text),
                    description = TBDescription.Text,
                    authorID = author.id,
                    genreID = genre.id,
                    publishID = publisher.id,
                    stateID = state.id,
                    image = img
                };
                AppData.db.Books.Add(book);
                AppData.db.SaveChanges();
                MessageBox.Show("Книга успешно добавлена!");
                NavigationService.GoBack();
            }
            else 
            {
                currentBook.image = img;
                currentBook.article = TBArticule.Text;
                currentBook.name = TBBookName.Text;
                currentBook.description = TBDescription.Text;
                currentBook.authorID = author.id;
                currentBook.publishID = publisher.id;
                currentBook.genreID = genre.id;
                currentBook.stateID = state.id;
                AppData.db.SaveChanges();
                MessageBox.Show("Автор успешно обновлен!");
                currentBook = null;
            }
            NavigationService.Navigate(new BookPage());
        }
    }
}
