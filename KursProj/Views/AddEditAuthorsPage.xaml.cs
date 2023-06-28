using KursProj.Model;
using KursProj.Windows;
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
using System.Xml.Linq;

namespace KursProj.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditAuthorsPage.xaml
    /// </summary>
    public partial class AddEditAuthorsPage : Page
    {
        public Authors currentAuthor;
        //пустая страница для добавления
        public AddEditAuthorsPage()
        {
            InitializeComponent();
            this.WindowTitle = "Добавление автора";
        }
        //редактирование записи
        public AddEditAuthorsPage(Authors author)
        {
            currentAuthor = author; 
            InitializeComponent();

            this.WindowTitle = "Редактирование автора";

            TBAuthorSurname.Text = currentAuthor.surname;
            TBAuthorName.Text = currentAuthor.name;
            TBAuthorPatronymic.Text = currentAuthor.patronymic;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TBAuthorName.Text)  || String.IsNullOrEmpty(TBAuthorSurname.Text))
            {
                MessageBox.Show("Заполните поля!");
                return;
            }
            if (currentAuthor == null)
            {
                Authors author = new Authors()
                {
                    name = TBAuthorName.Text,
                    surname = TBAuthorSurname.Text,
                    patronymic = TBAuthorPatronymic.Text,
                };
                AppData.db.Authors.Add(author);
                AppData.db.SaveChanges();
                MessageBox.Show("Автор успешно добавлен!");
            }
            else if (currentAuthor.name != TBAuthorName.Text || currentAuthor.surname != TBAuthorSurname.Text || currentAuthor.patronymic != TBAuthorPatronymic.Text)
            {
                currentAuthor.name = TBAuthorName.Text;
                currentAuthor.surname = TBAuthorSurname.Text;
                currentAuthor.patronymic = TBAuthorPatronymic.Text;
                AppData.db.SaveChanges();                
                MessageBox.Show("Автор успешно обновлен!");
                currentAuthor = null;
            }
        }
    }
}
