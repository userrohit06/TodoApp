using backend.Models;
using backend.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace backend.Repository.Implementation
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _dbContext;

        public TodoRepository(TodoDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Todo> CreateTodo(int userId, string title, string? description)
        {
            var conn = this._dbContext.Database.GetDbConnection();

            if(conn.State != ConnectionState.Open)
            {
                await conn.OpenAsync();
            }

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "usp_CreateTodo";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("UserId", SqlDbType.Int) { Value = userId });
            cmd.Parameters.Add(new SqlParameter("Title", SqlDbType.NVarChar, 100) { Value = title });
            cmd.Parameters.Add(new SqlParameter("Description", SqlDbType.NVarChar, 1000) { Value = (object?)description ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("IsCompleted", SqlDbType.Bit) { Value = false });

            // SP: SELECT SCOPE_IDENTITY() AS TodoId
            var scalar = await cmd.ExecuteScalarAsync();
            var todoId = Convert.ToInt32(scalar);

            // Now load the created row with EF
            var createdTodo = await this._dbContext.Todos
                .AsNoTracking()
                .FirstAsync(t => t.TodoId == todoId);

            return createdTodo;
        }

        public async Task<List<Todo>> GetTodosByUserId(int userId)
        {
            return await this._dbContext.Todos
                .AsNoTracking()
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }
    }
}
