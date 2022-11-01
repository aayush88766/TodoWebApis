using System.ComponentModel;

namespace TodoListWebAPIs.DTOs.Responses
{
    public class TodoResponse
    {
        public int Id { get; set; }
        [DefaultValue("This will be the title of the todo")]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Category { get; set; }
        public string? Priority { get; set; }
        public string? DueDate { get; set; }

    }
}
