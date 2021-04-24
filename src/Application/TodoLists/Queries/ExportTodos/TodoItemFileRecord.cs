using Player_API.Application.Common.Mappings;
using Player_API.Domain.Entities;

namespace Player_API.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
