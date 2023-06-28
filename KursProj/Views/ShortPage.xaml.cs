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
    /// Логика взаимодействия для ShortPage.xaml
    /// </summary>

    
    public partial class ShortPage : Page
    {
        public TableName currentTable;

        public ShortPage()
        {
            InitializeComponent();
        }
        public ShortPage(TableName tn)
        {
            currentTable = tn;
            this.WindowTitle = currentTable.ToString();

            InitializeComponent();
            Update();
        }
        public void Update()
        {
            switch (currentTable)
            {
                case TableName.Genres:
                    List<KursProj.Model.Genres> genres = AppData.db.Genres.ToList();
                    LVShort.ItemsSource = genres;
                    break;
                case TableName.Role:
                    var roles = AppData.db.Role.ToList();
                    LVShort.ItemsSource = roles;
                    break;
                case TableName.PublishingHouse:
                    var publishingHouse = AppData.db.PublishingHouse.ToList();
                    LVShort.ItemsSource = publishingHouse;
                    break;
                case TableName.State:
                    var state = AppData.db.State.ToList();
                    LVShort.ItemsSource = state;
                    break;
                default:
                    MessageBox.Show("Ошибка, сорри!!!");
                    break;
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            switch (currentTable)
            {               
                case TableName.Genres:
                    var currentGenre = button.DataContext as Genres;
                    NavigationService.Navigate(new AddEditShortPage(currentTable, currentGenre));
                    break;
                case TableName.Role:
                    var currentRole= button.DataContext as Role;
                    NavigationService.Navigate(new AddEditShortPage(currentTable, null, currentRole));
                    break;
                case TableName.PublishingHouse:
                    var currentPH = button.DataContext as PublishingHouse;
                    NavigationService.Navigate(new AddEditShortPage(currentTable, null, null, currentPH));
                    break;
                case TableName.State:
                    var currentState = button.DataContext as State;
                    NavigationService.Navigate(new AddEditShortPage(currentTable, null, null, null, currentState));
                    break;
                default:
                    break;
            }         
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы уверены что хотите удалить?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {               
                switch (currentTable)
                {
                    case TableName.Genres:
                        var currentGenre = (sender as Button).DataContext as Genres;
                        AppData.db.Genres.Remove(currentGenre);                        
                        break;
                    case TableName.Role:
                        var currentRole = (sender as Button).DataContext as Role;
                        AppData.db.Role.Remove(currentRole);
                        break;
                    case TableName.PublishingHouse:
                        var currentPH = (sender as Button).DataContext as PublishingHouse;
                        AppData.db.PublishingHouse.Remove(currentPH);
                        break;
                    case TableName.State:
                        var currentState = (sender as Button).DataContext as State;
                        AppData.db.State.Remove(currentState);
                        break;
                    default:
                        break;
                }
                AppData.db.SaveChanges();
                Update();
            }
        }
    }
}
