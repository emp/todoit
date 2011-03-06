using System;
using System.Collections.Generic;

namespace todoit.Domain.Model
{
    public class TodoList
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public List<Todo> Todos { get; set; }
    }
}
