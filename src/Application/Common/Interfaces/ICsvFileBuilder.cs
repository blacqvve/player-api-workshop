using Player_API.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace Player_API.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
