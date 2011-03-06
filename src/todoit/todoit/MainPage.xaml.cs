using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                MessageBox.Show("Please provider a name for your item ...or are you trying \"todo\" nothing?");
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
            App.ViewModel.GetLists();
        }

        private void SaveList(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var panel = (StackPanel)button.Parent;
            var textbox = panel.Children.OfType<TextBox>().FirstOrDefault();

            if (textbox.Text.Length == 0)
            {
                MessageBox.Show("Please provide a short, remarkable name to your list!");
                textbox.Focus();
                return;
            }

            var list = new TodoList
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Name = textbox.Text
            };

            textbox.Text = String.Empty;

            App.Database.Save(list);
            App.Database.Flush();
            App.ViewModel.GetLists();

            //.SelectedItem = list;
        }

        private void EditList(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void DisplayEditList(object sender, RoutedEventArgs e)
        {
            var text = (TextBlock)sender;
            var panel = (StackPanel)text.Parent;
            var inner = panel.Children.OfType<StackPanel>().FirstOrDefault();
            var list = (TodoList)lists.SelectedItem;

            addPanel.Visibility = Visibility.Visible;

            if (list.Name == "<new list>")
            {
                inner.Visibility = Visibility.Visible;
                addPanel.Children.OfType<TextBox>().FirstOrDefault().Focus();
            }
            else
            {
                inner.Visibility = Visibility.Collapsed;
            }

        }

        private void SetListName(object sender, RoutedEventArgs e)
        {
            var text = (TextBox)sender;
            //text.Focus();
        }
    }


}