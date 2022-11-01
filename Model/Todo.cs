using System.ComponentModel.DataAnnotations;

namespace TodoListWebAPIs.Model
{
    public class Todo
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public int StatusCode { get; set; }

        [Required]
        public string? Category { get; set; }

        [Required]
        public int PriorityCode { get; set; }

        [Required]
        public DateTime? Created { get; set; }

        [Required]
        public DateTime? Updated { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }
}