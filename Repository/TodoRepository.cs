using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TodoListWebAPIs.DataAccessLayers;
using TodoListWebAPIs.DTOs;
using TodoListWebAPIs.DTOs.Requests;
using TodoListWebAPIs.DTOs.Responses;
using TodoListWebAPIs.Model;

namespace TodoListWebAPIs.Repository
{
    public enum PriorityIndicator
    {
        Low = 0,
        Medium,
        High
    }
    public enum StatusIndicator
    {
        Incomplete = 0,
        Completed
    }
    public class TodoRepository : IRepository<TodoRequest, TodoResponse, int>
    {
        private readonly TodoDBContext _context;
        public TodoRepository(TodoDBContext context)
        {
            _context = context;
        }
        public async Task<TodoResponse> Add(TodoRequest todoRequest)
        {
            if (todoRequest == null)
            {
                throw new ArgumentNullException("Not received data to add", new NullReferenceException());
            }
            Todo todoToBeAdded = new Todo()
            {
                Title = todoRequest.Title,
                Description = todoRequest.Description,
                StatusCode = todoRequest.StatusCode,
                Category = todoRequest.Category,
                PriorityCode = todoRequest.PriorityCode,
                DueDate = todoRequest.DueDate,
                Updated = DateTime.Now,
                Created = DateTime.Now
            };
            await _context.Todos.AddAsync(todoToBeAdded);
            await _context.SaveChangesAsync();

            IFormatProvider culture = new CultureInfo("en-US", true);

            TodoResponse addedTodo = new TodoResponse()
            {
                Id = todoToBeAdded.Id,
                Title = todoToBeAdded.Title,
                Description = todoToBeAdded.Description,
                Status = ((StatusIndicator)todoToBeAdded.StatusCode).ToString(),
                Category = todoToBeAdded.Category,
                Priority = ((PriorityIndicator)todoToBeAdded.PriorityCode).ToString(),
                DueDate = String.Format("{0:yyyy-MM-dd}", todoToBeAdded.DueDate)
            };

            return addedTodo;
        }

        public async Task<bool> Delete(int id)
        {
            Todo? todoToBeDeleted = await _context.Todos.FindAsync(id);
            if (todoToBeDeleted == null)
            {
                throw new KeyNotFoundException("Todo with the given ID does not exist");
            }

            _context.Todos.Remove(todoToBeDeleted);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<TodoResponse> Get(int id)
        {
            Todo todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                throw new KeyNotFoundException("Todo with the given ID does not exist");
            }

            TodoResponse foundedTodo = new TodoResponse()
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Status = ((StatusIndicator)todo.StatusCode).ToString(),
                Category = todo.Category,
                Priority = ((PriorityIndicator)todo.PriorityCode).ToString(),
                DueDate = String.Format("{0:yyyy-MM-dd}", todo.DueDate)
            };

            return foundedTodo;
        }

        public async Task<List<TodoResponse>> GetAll(QueryParameters queryParameters)
        {
            List<TodoResponse> allTodosToBeRetured = new List<TodoResponse>();

            List<Todo> allTodos = await _context.Todos.ToListAsync();
            if (queryParameters.Category != "All")
            {
                allTodos = allTodos.FindAll(match: t => t.Category!.Equals(queryParameters.Category, StringComparison.OrdinalIgnoreCase));
            }

            allTodos.Sort((a, b) =>
            {
                if (a.StatusCode == b.StatusCode)
                {
                    if (a.PriorityCode == b.PriorityCode)
                    {
                        return (queryParameters.DuedateSortDirection.Equals("asc") ? a.DueDate.CompareTo(b.DueDate) : b.DueDate.CompareTo(a.DueDate));
                    }
                    else
                    {
                        return (queryParameters.PrioritySortDirection.Equals("asc") ? a.PriorityCode.CompareTo(b.PriorityCode) : b.PriorityCode.CompareTo(a.PriorityCode));
                    }
                }
                return (queryParameters.StatusSortDirection.Equals("asc") ? a.StatusCode.CompareTo(b.StatusCode) : b.StatusCode.CompareTo(a.StatusCode));
                // return (queryParameters.DuedateSortDirection.Equals("asc") ? a.DueDate.CompareTo(b.DueDate) : b.DueDate.CompareTo(a.DueDate));
            });

            foreach (Todo todo in allTodos)
            {
                allTodosToBeRetured.Add(new TodoResponse()
                {
                    Id = todo.Id,
                    Title = todo.Title,
                    Description = todo.Description,
                    Status = ((StatusIndicator)todo.StatusCode).ToString(),
                    Category = todo.Category,
                    Priority = ((PriorityIndicator)todo.PriorityCode).ToString(),
                    DueDate = String.Format("{0:yyyy-MM-dd}", todo.DueDate)
                });
            }

            return allTodosToBeRetured;
        }

        public async Task<TodoResponse> Update(int id, TodoRequest todoToBeUpdatedTo)
        {
            if (todoToBeUpdatedTo == null)
            {
                throw new ArgumentNullException("Not received data to add", new NullReferenceException());
            }
            Todo? todoToBeUpdated = await _context.Todos.FindAsync(id);
            if (todoToBeUpdated == null)
            {
                throw new KeyNotFoundException("Todo with the given ID does not exist");
            }

            todoToBeUpdated.Title = todoToBeUpdatedTo.Title;
            todoToBeUpdated.Description = todoToBeUpdatedTo.Description;
            todoToBeUpdated.StatusCode = todoToBeUpdatedTo.StatusCode;
            todoToBeUpdated.Category = todoToBeUpdatedTo.Category;
            todoToBeUpdated.PriorityCode = todoToBeUpdatedTo.PriorityCode;
            todoToBeUpdated.DueDate = todoToBeUpdatedTo.DueDate;

            _context.SaveChanges();

            TodoResponse updatedTodo = new TodoResponse()
            {
                Id = todoToBeUpdated.Id,
                Title = todoToBeUpdated.Title,
                Description = todoToBeUpdated.Description,
                Status = ((StatusIndicator)todoToBeUpdated.StatusCode).ToString(),
                Category = todoToBeUpdated.Category,
                Priority = ((PriorityIndicator)todoToBeUpdated.PriorityCode).ToString(),
                DueDate = String.Format("{0:yyyy-MM-dd}", todoToBeUpdated.DueDate)
            };

            return updatedTodo;
        }

        public async Task<List<TodoResponse>> GetByCategory(string category)
        {
            List<TodoResponse> allTodosToBeRetured = new List<TodoResponse>();

            List<Todo> allTodos = await _context.Todos.ToListAsync();
            allTodos = allTodos.FindAll(t => t.Category!.Equals(category, StringComparison.OrdinalIgnoreCase));
            foreach (Todo todo in allTodos)
            {
                allTodosToBeRetured.Add(new TodoResponse()
                {
                    Id = todo.Id,
                    Title = todo.Title,
                    Description = todo.Description,
                    Status = ((StatusIndicator)todo.StatusCode).ToString(),
                    Category = todo.Category,
                    Priority = ((PriorityIndicator)todo.PriorityCode).ToString(),
                    DueDate = String.Format("{0:yyyy-MM-dd}", todo.DueDate)
                });
            }

            return allTodosToBeRetured;
        }

    }
}
