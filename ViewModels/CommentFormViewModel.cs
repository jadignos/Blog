using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class CommentFormViewModel
    {
        public int PostId { get; set; } = 0;

        public int CommentId { get; set; } = 0;

        [Required]
        [MaxLength(500)]
        public string Message { get; set; } = string.Empty;
    }
}
