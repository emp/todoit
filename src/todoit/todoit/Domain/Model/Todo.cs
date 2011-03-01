using System;

namespace todoit.Domain.Model
{
    public class Todo
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }

    }
}
