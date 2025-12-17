using Azure.Core;
using backend.DTO.Common;
using backend.DTO.Todo;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            this._todoService = todoService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ApiResponse<TodoResponse>>> Create([FromBody] CreateTodoRequest request)
        {
            var useridClaim = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if(!int.TryParse(useridClaim, out var userId))
            {
                return Unauthorized(new ApiResponse<object>
                {
                    Status = 401,
                    Message = "Invalid token",
                    Data = null
                });
            }

            var result = await this._todoService.CreateTodo(userId, request);
            return StatusCode(result.Status, result);
        }

        [HttpGet("all")]
        public async Task<ActionResult<ApiResponse<TodoResponse>>> All()
        {
            var useridClaim = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!int.TryParse(useridClaim, out var userId))
            {
                return Unauthorized(new ApiResponse<object>
                {
                    Status = 401,
                    Message = "Invalid token",
                    Data = null
                });
            }

            var result = await this._todoService.GetTodoList(userId);
            return StatusCode(result.Status, result);
        }

        [HttpPost("complete-task")]
        public async Task<ActionResult<ApiResponse<bool>>> CompleteTask(int todoId)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if(!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new ApiResponse<object>
                {
                    Status = 401,
                    Message = "Invalid token",
                    Data = null
                });

            }
            var result = await this._todoService.UpdateStatus(userId, todoId);
            return StatusCode(result.Status, result);
        }
    }
}
