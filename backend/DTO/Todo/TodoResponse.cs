namespace backend.DTO.Todo
{
    public class TodoResponse
    {
        public int todoId { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public bool IsCompleted { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
