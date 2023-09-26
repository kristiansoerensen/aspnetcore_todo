using ServiceLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class TodoService : ITodoService
    {
        private readonly List<TodoItem> _todos = new()
        {
            new TodoItem{
                Id = 1,
                Name = "Todo 1",
                Description = "",
                IsCompleted = false
            },
            new TodoItem{
                Id = 2,
                Name = "Todo 2",
                Description = "",
                IsCompleted = true
            },
        };

        public IEnumerable<TodoItem> GetTodos() => _todos;

        public TodoItem GetTodoById(int id) => _todos.FirstOrDefault(t => t.Id == id);

        public TodoItem AddTodoItem(TodoItem todoItem)
        {
            int id = _todos.Max(t => t.Id) + 1;
            todoItem.Id = id;
            _todos.Add(todoItem);
            return todoItem;
        }

        public void DeleteTodoItem(int id)
        {
            _todos.RemoveAll(t => t.Id == id);
        }

        public bool UpdateTodoItem(TodoItem todoItem)
        {
            TodoItem? updateTodoItem = _todos.FirstOrDefault(t => t.Id == todoItem.Id);
            if (todoItem != null)
            {
                updateTodoItem = todoItem;
                return true;
            }

            return false;
        }
    }
}
