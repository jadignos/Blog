using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data.Managers;
using Blog.Data.Repositories;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IImageManager _imageManager;
        private readonly ICategoryRepository _categoryRepository;

        public AdminController(IPostRepository postRepository, 
                               IImageManager imageManager, 
                               ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _imageManager = imageManager;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Index() =>
            View(_postRepository.GetPostsDescending());

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            var categories = _categoryRepository
                .GetCategories()
                .Select(category =>
                    new SelectListItem
                    {
                        Value = category.Id.ToString(),
                        Text = category.Name
                    });

            if (id == null)
                return View(new PostViewModel() { Categories = categories });

            var post = _postRepository.GetPost((int)id);

            if (post == null)
                return NotFound();

            var postViewModel = new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Tags = post.Tags,
                Body = post.Body,
                CurrentImage = post.Image,
                DateCreated = post.DateCreated,
                CategoryId = post.CategoryId,
                Categories = categories
            };

            return View(postViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(PostViewModel postViewModel)
        {
            var post = new Post
            {
                Id = postViewModel.Id,
                Title = postViewModel.Title,
                Description = postViewModel.Description,
                Tags = postViewModel.Tags,
                Body = postViewModel.Body,
                CategoryId = postViewModel.CategoryId
            };

            if (postViewModel.Image == null)
            {
                post.Image = postViewModel.CurrentImage;
            }
            else
            {
                _imageManager.DeleteImage(postViewModel.CurrentImage);
                post.Image = await _imageManager.SaveImageStream(postViewModel.Image);
            }

            if (post.Id == 0)
                _postRepository.AddPost(post);
            else
            {
                post.DateCreated = postViewModel.DateCreated;
                _postRepository.UpdatePost(post);
            }

            return await _postRepository.SaveChangesAsync()
                ? RedirectToAction(nameof(Index))
                : (IActionResult)View(postViewModel);
        }

        //[HttpGet]
        //public async Task<IActionResult> Remove(int id)
        //{
        //    _postRepository.RemovePost(id);

        //    await _postRepository.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}
    }
}
