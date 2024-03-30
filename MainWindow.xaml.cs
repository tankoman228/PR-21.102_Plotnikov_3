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

namespace PR_21._102_15_RK2023
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            btnSearch.Click += BtnSearch_Click;
        }

        //Обработка нажатия на кнопку 
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            //Строка, по которой будет производиться поиск
            string search = tbSearch.Text;

            try
            {
                //Подключение к базе
                using (var model = new Entity.Entities())
                {                   
                    //Проверка, по какому способу сортировать (см. выпадающий список)
                    if (cbOrderBy.SelectedItem == cbiNoOrder)
                    {
                        var c = model.Discipline.Where(x => x.Title.Contains(search));
                        LoadData.ItemsSource = c.ToList();

                        //Проверка, найдено ли хоть что-то
                        if (c.Count() == 0)
                        {
                            MessageBox.Show("Ничего не найдено", "Поиск",
                                MessageBoxButton.OK, MessageBoxImage.Question);
                        }
                    }
                    else if (cbOrderBy.SelectedItem == cbiByName)
                    {
                        var c = model.Discipline.Where(x => x.Title.Contains(search))
                            .OrderBy(x => x.Title);
                        LoadData.ItemsSource = c.ToList();

                        //Проверка, найдено ли хоть что-то
                        if (c.Count() == 0)
                        {
                            MessageBox.Show("Ничего не найдено", "Поиск",
                                MessageBoxButton.OK, MessageBoxImage.Question);
                        }
                    }
                    else if (cbOrderBy.SelectedItem == cbiByNameDescr)
                    {
                        var c = model.Discipline.Where(x => x.Title.Contains(search))
                            .OrderByDescending(x => x.Title);
                        LoadData.ItemsSource = c.ToList();

                        //Проверка, найдено ли хоть что-то
                        if (c.Count() == 0)
                        {
                            MessageBox.Show("Ничего не найдено", "Поиск",
                                MessageBoxButton.OK, MessageBoxImage.Question);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ошибка -> не вылет, а вывод сообщения о ней
                MessageBox.Show("Couldn\'t make this operation, error: \n" + ex.Message
                    , "Fatal error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
