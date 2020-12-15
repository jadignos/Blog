using Blog.Models;
using System.Collections.Generic;

namespace Blog.ViewModels
{
    public class DisplayCommentViewModel
    {
        public int PostId { get; set; } = 0;

         public string User { get; set; }

        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
    }
}
