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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public TableName currentTable;
        public AdminWindow()
        {
            InitializeComponent();    
            SelectTable.SelectedIndex = 0;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentTable = (TableName)SelectTable.SelectedIndex;
            switch (currentTable)
            {
                case TableName.Genres:
                    MainFrame.Navigate(new Views.ShortPage(TableName.Genres));
                    break;
                case TableName.Role:
                    MainFrame.Navigate(new Views.ShortPage(TableName.Role));
                    break;
                case TableName.PublishingHouse:
                    MainFrame.Navigate(new Views.ShortPage(TableName.PublishingHouse));
                    break;
                case TableName.State:
                    MainFrame.Navigate(new Views.ShortPage(TableName.State));
                    break;
                case TableName.Authors:
                    MainFrame.Navigate(new Views.AuthorsPage());
                    break;
                case TableName.Users:
                    MainFrame.Navigate(new Views.UsersPage());
                    break;
                case TableName.Books:
                    MainFrame.Navigate(new Views.BookPage());
                    break;
                case TableName.UserBookPair:
                    MainFrame.Navigate(new Views.HistoryPage());
                    break;
                default:
                    break;
            }            
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            switch (currentTable)
            {
                case TableName.Authors:
                    MainFrame.Navigate(new Views.AddEditAuthorsPage());
                    break;
                case TableName.Books:
                    MainFrame.Navigate(new Views.AddEditBookPage());
                    break;
                case TableName.Genres:
                    MainFrame.Navigate(new Views.AddEditShortPage(TableName.Genres));
                    break;
                case TableName.Role:
                    MainFrame.Navigate(new Views.AddEditShortPage(TableName.Role));
                    break;
                case TableName.PublishingHouse:
                    MainFrame.Navigate(new Views.AddEditShortPage(TableName.PublishingHouse));
                    break;
                case TableName.State:
                    MainFrame.Navigate(new Views.AddEditShortPage(TableName.State));
                    break;
                case TableName.Users:
                    MainFrame.Navigate(new Views.AddEditUserPage());
                    break;
                case TableName.UserBookPair:
                    
                    break;
                default:
                    break;
            }
        }

        private void GoExit_Click(object sender, MouseButtonEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Window.GetWindow(this).Close();
        }
        private void GoBackImage_Click(object sender, MouseEventArgs e)
        {
            if (MainFrame.CanGoBack)
                MainFrame.GoBack();
        }
    }
}
