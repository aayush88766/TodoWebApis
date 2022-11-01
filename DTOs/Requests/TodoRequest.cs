using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace TodoListWebAPIs.DTOs.Requests
{
    public class TodoRequest
    {
        [Required]
        [DefaultValue("Title 1")]
        public string? Title { get; set; }

        [Required]
        [DefaultValue("Description 1")]
        public string? Description { get; set; }

        [Required]
        [DefaultValue(0)]
        public int StatusCode { get; set; }

        [Required]
        [DefaultValue("Category 1")]
        public string? Category { get; set; }

        [Required]
        [DefaultValue(1)]
        public int PriorityCode { get; set; }

        [Required]
        [DefaultValue("2022-10-24")]
        public DateTime DueDate { get; set; }
    }
}