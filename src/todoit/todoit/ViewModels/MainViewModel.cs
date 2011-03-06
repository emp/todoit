using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;
using todoit.Domain.Model;


namespace todoit.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            Items = new ObservableCollection<TodoList>();
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

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void GetLists()
        {
            var items = new ObservableCollection<TodoList>();
            //App.Database.Purge();
            App.Database.Query<TodoList, Guid>().Select(tl => tl.LazyValue.Value).ToList().ForEach(items.Add);
            
            // this basic item will reduce a ton of extra UI paths
            // by always having a new List available for quick inserts.
            items.Add(new TodoList
            {
                Created = DateTime.Now,
                Id = Guid.NewGuid(),
                Name = "<new list>"
            });

            Items = items;
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