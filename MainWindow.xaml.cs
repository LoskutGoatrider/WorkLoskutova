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
using WorkLoskutova.Model;

namespace WorkLoskutova
{
        /// <summary>
        /// Логика взаимодействия для MainWindow.xaml
        /// </summary>
        public partial class MainWindow : Window
        {
                public MainWindow()
                {
                        InitializeComponent();
                        LoadData();
                        LoadGenres();

                        genreComboBox.SelectedIndex = 0;
                        sortComboBox.SelectedIndex = 0;
                }

                private void LoadData()
                {
                        using (var context = new loskutEntities())
                        {
                                var result = context.Books.Include("Genres").ToList();
                                dataGrid.ItemsSource = result;
                        }
                }

                private void LoadGenres()
                {
                        using (var context = new loskutEntities())
                        {
                                genreComboBox.Items.Clear();
                                var genres = new List<Genres> { new Genres { Id = -1, Name = "Без параметров" } };
                                genres.AddRange(context.Genres.ToList());

                                genreComboBox.ItemsSource = genres;
                                genreComboBox.DisplayMemberPath = "Name";
                                genreComboBox.SelectedValuePath = "Id";
                        }
                }

                private void SearchButton_Click(object sender, RoutedEventArgs e)
                {
                        var searchText = searchTextBox.Text.ToLower();
                        var selectedGenre = genreComboBox.SelectedItem as Genres;
                        var selectedSort = sortComboBox.SelectedItem as ComboBoxItem;

                        using (var context = new loskutEntities())
                        {
                                var query = context.Books.Include("Genres").AsQueryable();
                             
                                if (!string.IsNullOrEmpty(searchText))
                                {
                                        query = query.Where(b => b.Title.ToLower().StartsWith(searchText) || b.Author.ToLower().StartsWith(searchText));
                                }

                                if (selectedGenre != null && selectedGenre.Id != -1)
                                {
                                        query = query.Where(b => b.GenreId == selectedGenre.Id);
                                }

                                if (selectedSort != null && selectedSort.Content.ToString() != "Без параметров")
                                {
                                        switch (selectedSort.Content.ToString())
                                        {
                                                case "Название (А-Я)":
                                                        query = query.OrderBy(b => b.Title);
                                                        break;
                                                case "Название (Я-А)":
                                                        query = query.OrderByDescending(b => b.Title);
                                                        break;
                                                case "Автор (А-Я)":
                                                        query = query.OrderBy(b => b.Author);
                                                        break;
                                                case "Автор (Я-А)":
                                                        query = query.OrderByDescending(b => b.Author);
                                                        break;
                                                case "Цена (по возрастанию)":
                                                        query = query.OrderBy(b => b.Price);
                                                        break;
                                                case "Цена (по убыванию)":
                                                        query = query.OrderByDescending(b => b.Price);
                                                        break;
                                        }
                                }

                                dataGrid.ItemsSource = query.ToList();
                        }
                }

                private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
                {
                        searchTextBox.Clear();
                        SearchButton_Click(sender, e);
                }

                private void GenreComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
                {
                        SearchButton_Click(sender, e);
                }

                private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
                {
                        SearchButton_Click(sender, e);
                }

                private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
                {
                        genreComboBox.SelectedIndex = 0;
                        sortComboBox.SelectedIndex = 0;
                        SearchButton_Click(sender, e);
                }
        }
}
