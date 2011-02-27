using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using todoit.Domain.Model;


namespace todoit.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            _items = new ObservableCollection<TodoList>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        private ObservableCollection<TodoList> _items;
        public ObservableCollection<TodoList> Items
        {
            get
            {
                return _items;
            }
            set
            {
                if (value == _items)
                    return;

                _items = value;
                NotifyPropertyChanged("Items");

            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            App.Database.Query<TodoList, Guid>().Select(tl => tl.LazyValue.Value).ToList().ForEach(Items.Add);
            IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}