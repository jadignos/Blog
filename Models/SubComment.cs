using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class SubComment
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        [Required]
        [MaxLength(300)]
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;


        [ForeignKey(nameof(Comment))]
        public int CommentId { get; set; }

        public Comment Comment { get; set; }
    }
}
