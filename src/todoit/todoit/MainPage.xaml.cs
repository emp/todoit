using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using todoit.Domain.Model;

namespace todoit
{
    public partial class MainPage
    {

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            DataBind();

            // when we dont have any lists 
            // then we send the user to the List Manager
            if (!App.ViewModel.Items.Any())
                Loaded += ManageLists;
        }


        private void DataBind()
        {
            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            App.ViewModel.GetLists();
        }

        private void CompleteTodo(object sender, RoutedEventArgs e)
        {
            var box = (CheckBox)sender;
            var panel = (StackPanel)box.Parent;

            var todo = App.Database.Load<Todo, Guid>(new Guid(box.Tag.ToString()));
            todo.Completed = true;
            App.Database.Save(todo);

            panel.Opacity = 0.5;
        }

        private void ReviveTodo(object sender, RoutedEventArgs e)
        {
            var box = (CheckBox)sender;
            var panel = (StackPanel)box.Parent;

            var todo = App.Database.Load<Todo, Guid>(new Guid(box.Tag.ToString()));
            todo.Completed = false;
            App.Database.Save(todo);

            panel.Opacity = 1;
        }

        private void AddTodo(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var panel = (StackPanel)button.Parent;
            var textbox = panel.Children.OfType<TextBox>().FirstOrDefault();

            if (textbox.Text.Length == 0)
            {
                MessageBox.Show("TODOs are like pets, they need a name!");
                textbox.Focus();
                return;
            }

            var todoList = (TodoList)lists.SelectedItem;

            var todo = new Todo
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Name = textbox.Text
            };

            textbox.Text = "";
            lists.Focus();

            List<Todo> todos;

            try
            {
                todos = todoList.Todos.ToList();
            }
            catch (Exception)
            {
                //Sterling behaves weirdly when collection references are empty
                todos = new List<Todo>();
            }

            todos.Add(todo);
            todoList.Todos = todos;

            App.Database.Save(todoList);
            App.Database.Flush();
        }

        private void ManageLists(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/ListsPage.xaml", UriKind.Relative));
            Loaded -= ManageLists;
        }
    }


}