using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using todoit.Domain.Model;

namespace todoit
{
    public partial class ListsPage
    {

        // Constructor
        public ListsPage()
        {
            InitializeComponent();
            DataBind();
        }

        private void DataBind()
        {
            DataContext = App.ViewModel;
            App.ViewModel.GetLists();
        }

        private void AddList(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var panel = (StackPanel)button.Parent;
            var textbox = panel.Children.OfType<TextBox>().FirstOrDefault();

            if(textbox.Text.Length == 0)
            {
                MessageBox.Show("Please provide a short, remarkable name to yout list!");
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

        }
    }
}