using KursProj.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
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
    /// Логика взаимодействия для AddEditShortPage.xaml
    /// </summary>
    public partial class AddEditShortPage : Page
    {
        public TableName currentTable;       
        public string itemName;
        public Role role;
        public Genres genre;
        public PublishingHouse publishingHouse;
        public State state;
        public bool addOrEditFlag = false; //добавление - true, изменение - false;

        //добавление
        public AddEditShortPage(TableName tn)
        {
            InitializeComponent();
            this.WindowTitle = "Добавление";
            currentTable = tn;
            addOrEditFlag = true;

            switch (currentTable)
            {
                case TableName.Genres:
                    this.WindowTitle = "Добавление жанров";
                    TBName.Text = "Жанр:";
                    break;
                case TableName.Role:
                    this.WindowTitle = "Добавление ролей";
                    TBName.Text = "Роль:";
                    break;
                case TableName.PublishingHouse:
                    this.WindowTitle = "Добавление издательств";
                    TBName.Text = "Издательство:";
                    break;
                case TableName.State:
                    this.WindowTitle = "Добавление состояний книг";
                    TBName.Text = "Состояние:";
                    break;
                default:
                    break;
            }
        }
        //редактирование
        public AddEditShortPage(TableName tn, Genres gn = null, Role rl = null, PublishingHouse pbl = null, State st = null)
        {
            this.currentTable = tn;
            addOrEditFlag = false;

            this.genre = gn;
            this.role = rl;
            this.publishingHouse = pbl;
            this.state = st;
            InitializeComponent();

            switch (currentTable)
            {
                case TableName.Genres:
                    this.WindowTitle = "Редактирование жанров";
                    TBName.Text = "Жанр:";
                    TBShortName.Text = genre.name;
                    break;
                case TableName.Role:
                    this.WindowTitle = "Редактирование ролей";
                    TBName.Text = "Роль:";
                    TBShortName.Text = role.name;
                    break;
                case TableName.PublishingHouse:
                    this.WindowTitle = "Редактирование издательств";
                    TBName.Text = "Издательство:";
                    TBShortName.Text = publishingHouse.name;
                    break;
                case TableName.State:
                    this.WindowTitle = "Редактирование состояний книг";
                    TBName.Text = "Состояние:";
                    TBShortName.Text = state.name;
                    break; 
                default:
                    break;
            }           
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //(TableName)currentTable ct = new (TableName)currentTable();
            //CS8370: Feature 'top-level statements' is not available in C# 7.3. Please use language version 9.0 or greater.
            if (addOrEditFlag)
            {
                switch (currentTable)
                {                     
                    case TableName.Genres:

                        if (AppData.db.Genres.Count(x => x.name == TBName.Text) > 0)
                        {
                            MessageBox.Show("Такой жанр уже есть");
                            return;
                        }
                        Genres genre = new Genres()
                        {
                            name = TBShortName.Text,
                        };
                        AppData.db.Genres.Add(genre);
                        break;
                    case TableName.Role:
                        if (AppData.db.Role.Count(x => x.name == TBName.Text) > 0)
                        {
                            MessageBox.Show("Такая роль уже есть");
                            break;
                        }
                        Role role = new Role()
                        {
                            name = TBShortName.Text,
                        };
                        AppData.db.Role.Add(role);
                        break;
                    case TableName.PublishingHouse:
                        if (AppData.db.PublishingHouse.Count(x => x.name == TBName.Text) > 0)
                        {
                            MessageBox.Show("Такой издатель уже есть");
                            return;
                        }
                        PublishingHouse publishingHouse = new PublishingHouse()
                        { 
                            name = TBShortName.Text,
                        };
                        AppData.db.PublishingHouse.Add(publishingHouse);
                        break;
                    case TableName.State:
                        if (AppData.db.State.Count(x => x.name == TBName.Text) > 0)
                        {
                            MessageBox.Show("Такое уже есть");
                            return;
                        }
                        State state = new State() { name = TBShortName.Text, };
                        AppData.db.State.Add(state);
                        break;
                    default:
                        break;
                }
                AppData.db.SaveChanges();
                MessageBox.Show("Запись успешно добавлена в таблицу!");
            }
            else  
            {
                switch (currentTable)
                {
                    case TableName.Genres:
                        genre.name = TBShortName.Text;
                        break;
                    case TableName.Role:
                        role.name = TBShortName.Text;
                        break;
                    case TableName.PublishingHouse:
                        publishingHouse.name = TBShortName.Text;
                        break;
                    case TableName.State:
                        state.name = TBShortName.Text;
                        break;
                    default:
                        break;
                }
                AppData.db.SaveChanges();
                MessageBox.Show("Запись успешно изменена");
            }
            NavigationService.Navigate(new ShortPage(currentTable));
        }
    }
}
