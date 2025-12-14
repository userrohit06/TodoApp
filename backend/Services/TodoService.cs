using backend.DTO.Common;
using backend.DTO.Todo;
using backend.Repository.Interface;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepo;

        public TodoService(ITodoRepository todoRepository)
        {
            this._todoRepo = todoRepository;
        }

        public async Task<ApiResponse<TodoResponse>> CreateTodo(int userId, CreateTodoRequest request)
        {
            var response = new ApiResponse<TodoResponse>();

            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Title))
                {
                    response.Status = 400;
                    response.Message = "Title is required";
                    return response;
                }

                var todoEntity = await this._todoRepo.CreateTodo(userId, request.Title, request.Description);

                var todoDto = new TodoResponse
                {
                    Title = todoEntity.Title,
                    Description = todoEntity.Description ?? "",
                    IsCompleted = todoEntity.IsCompleted,
                    CreatedOn = todoEntity.CreatedOn
                };

                response.Status = 201;
                response.Message = "Todo created successfully";
                response.Data = todoDto;
                return response;
            }
            catch (Exception)
            {
                response.Status = 500;
                response.Message = "Unexpected error occured creating todo";
                return response;
            }
        }

        public async Task<ApiResponse<List<TodoResponse>>> GetTodoList(int userId)
        {
            var response = new ApiResponse<List<TodoResponse>>();

            try
            {
                var todos = await this._todoRepo.GetTodosByUserId(userId);
                if (todos == null || !todos.Any())
                {
                    response.Status = 404;
                    response.Message = "No todo found";
                    return response;
                }

                var todoDtos = todos.Select(todo => new TodoResponse
                {
                    todoId = todo.TodoId,
                    Title = todo.Title,
                    Description = todo.Description ?? "",
                    IsCompleted = todo.IsCompleted,
                    CreatedOn = todo.CreatedOn,

                }).ToList();

                response.Status = 200;
                response.Message = "Todos fetched successfully";
                response.Data = todoDtos;
                return response;
            }
            catch (Exception)
            {
                response.Status = 500;
                response.Message = "Unexpected error occured fetching todos";
                return response;
            }
        }
    }
}
