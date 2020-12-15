using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Blog.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public IFormFile Image { get; set; } = null;

        public string CurrentImage { get; set; } = string.Empty;

        public string Tags { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; }

        public int CategoryId { get; set; } = 1;

        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
