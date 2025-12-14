using backend.DTO.Common;
using backend.DTO.Todo;

namespace backend.Services.Interfaces
{
    public interface ITodoService
    {
        Task<ApiResponse<TodoResponse>> CreateTodo(int userId, CreateTodoRequest request);
        Task<ApiResponse<List<TodoResponse>>> GetTodoList(int userId);
    }
}
