using Blog.Data.Managers;
using Blog.Data.Helpers;
using Blog.Data.Repositories;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IImageManager _imageManager;
        private readonly ICommentRepository _commentRepository;
        private readonly ISubCommentRepository _subCommentRepository;
        private readonly IContactRepository _contactRepository;

        public HomeController(IPostRepository postRepository,
                              IImageManager imageManager,
                              ICommentRepository commentRepository,
                              ISubCommentRepository subCommentRepository,
                              IContactRepository contactRepository)
        {
            _postRepository = postRepository;
            _imageManager = imageManager;
            _commentRepository = commentRepository;
            _subCommentRepository = subCommentRepository;
            _contactRepository = contactRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page, int? category)
        {
            if (page == null || page < 1)
                page = 1;

            var posts = await PaginatedList<Post>.CreateAsync(
                _postRepository.GetPostsDescending(), (int)page, Constant.Variable.PAGE_SIZE);

            return View(posts);
        }

        [HttpGet]
        public IActionResult About() =>
            View();

        [HttpGet]
        public IActionResult Contact() =>
            View(new ContactViewModel());

        [HttpPost]
        public IActionResult Contact(ContactViewModel contact)
        {
            if (ModelState.IsValid)
            {
                _contactRepository.AddContact(new Models.Contact
                {
                    Name = contact.Name,
                    Email = contact.Email,
                    PhoneNumber = contact.PhoneNumber,
                    Message = contact.Message
                });
                _contactRepository.SaveChangesAsync();

                ViewBag.DisplayStatus =
                    $"{Constant.MessageInfo.MESSAGE_SENT}. {Constant.MessageInfo.WELL_GET_BACK_SHORTLY}";

                ModelState.Clear();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Post(int id) =>
            View(_postRepository.GetPost(id));

        [HttpGet("/images/{image}")]
        [ResponseCache(Duration = 2629746)] // no. of secs in a month
        public IActionResult GetImage(string image)
        {
            string mimeType = image.Substring(image.IndexOf('.') + 1);
            return new FileStreamResult(_imageManager.GetImageStream(image), $"image/{mimeType}");
        }

        [HttpPost]
        public async Task<IActionResult> SaveComment(CommentFormViewModel comment)
        {
            if (!ModelState.IsValid)
                return Post(comment.PostId);

            // comment
            if (comment.CommentId == 0)
            {
                _commentRepository.AddComment(comment.PostId, new Comment { UserName = User.Identity.Name, Message = comment.Message });
                await _commentRepository.SaveChangesAsync();
            }
            // sub-comment
            else
            {
                _subCommentRepository.AddSubComment(comment.CommentId, new SubComment { UserName = User.Identity.Name, Message = comment.Message });
                await _subCommentRepository.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Post), new { id = comment.PostId });
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    }
}
