using System.ComponentModel.DataAnnotations;

namespace TodoListApi.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        public bool IsDone { get; set; }
    }
}
