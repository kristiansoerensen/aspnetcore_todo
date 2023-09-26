using ServiceLayer.Entities;

namespace ServiceLayer
{
    public interface ITodoService
    {
        IEnumerable<TodoItem> GetTodos();
        TodoItem GetTodoById(int id);

        TodoItem AddTodoItem(TodoItem item);

        public void DeleteTodoItem(int id);

        public bool UpdateTodoItem(TodoItem todoItem);
    }
}