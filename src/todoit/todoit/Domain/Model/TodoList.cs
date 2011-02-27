using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace todoit.Domain.Model
{
    public class TodoList : INotifyPropertyChanged
    {
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set
            {
                if (value == _id)
                    return;

                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

        private DateTime _created;
        public DateTime Created
        {
            get { return _created; }
            set
            {
                if (value == _created)
                    return;

                _created = value;
                NotifyPropertyChanged("Created");
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name)
                    return;

                _name = value;
                NotifyPropertyChanged("Name");
            }
        }


        private IEnumerable<Todo> _todos;
        public IEnumerable<Todo> Todos
        {
            get { return _todos; }
            set
            {
                if (value == _todos)
                    return;

                _todos = value;
                NotifyPropertyChanged("Todos");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
