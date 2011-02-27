using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using todoit.Domain.Model;

namespace todoit
{
    public partial class ListsPage
    {

        // Constructor
        public ListsPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            if (App.ViewModel.IsDataLoaded)
                return;

            App.ViewModel.LoadData();
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

            textbox.Text = "";

            App.Database.Save(list);
            App.Database.Flush();

            App.ViewModel.LoadData();

            addPanel.Visibility = Visibility.Collapsed;

        }

        private void DisplayAddList(object sender, EventArgs e)
        {
            addPanel.Visibility = Visibility.Visible;
            add.Focus();
        }
    }


}