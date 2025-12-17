using backend.Models;

namespace backend.Repository.Interface
{
    public interface ITodoRepository
    {
        Task<Todo> CreateTodo(int userId, string title, string? description);
        Task<List<Todo>> GetTodosByUserId(int userId);
        Task<Todo?> GetTodoById(int todoId);
        Task<int> SaveChanges();
    }
}
