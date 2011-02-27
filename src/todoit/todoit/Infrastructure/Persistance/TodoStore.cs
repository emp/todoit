using System;
using System.Collections.Generic;
using todoit.Domain.Model;
using Wintellect.Sterling.Database;

namespace todoit.Infrastructure.Persistance
{
    public class TodoStore : BaseDatabaseInstance
    {
        public override string Name
        {
            get { return "TodoStore"; }
        }

        protected override List<ITableDefinition> _RegisterTables()
        {
            return new List<ITableDefinition>
            {
                CreateTableDefinition<TodoList, Guid>(tl => tl.Id).WithIndex<TodoList, string, Guid>("TodoListName", tl => tl.Name),
                CreateTableDefinition<Todo, Guid>(t => t.Id).WithIndex<Todo, string, Guid>("TodoName", t => t.Name)
            };
        }
    }
}
