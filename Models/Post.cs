using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        [Required]
        public string Body { get; set; }

        public string Image { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public IEnumerable<Comment> Comments { get; set; }


        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
