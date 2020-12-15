using Blog.Data.Services;
using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMailService _mailService;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IMailService mailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mailService = mailService;
        }

        [HttpGet]
        public IActionResult Login() =>
            View(new LoginViewModel());

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
                return View(login);

            var user = await _userManager.FindByNameAsync(login.Username);

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError(string.Empty, Constant.MessageError.EMAIL_NOT_CONFIRMED);
                return View(login);
            }

            var signInStatus = await _signInManager.PasswordSignInAsync(login.Username, login.Password, login.RememberMe, false);
            if (!signInStatus.Succeeded)
            {
                ModelState.AddModelError(string.Empty, Constant.MessageError.INVALID_LOGIN_ATTEMPT);
                return View(login);
            }

            if (await _userManager.IsInRoleAsync(user, Constant.Name.ADMIN))
                return RedirectToAction("Index", "Admin");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register() =>
            View(new RegisterViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
                return View(register);

            var user = new IdentityUser
            {
                UserName = register.Username,
                Email = register.Email
            };

            var status = await _userManager.CreateAsync(user, register.Password);

            if (!status.Succeeded)
            {
                foreach (var error in status.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(register);
            }

            // send mail
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string url = Url.Action("ConfirmEmail", "Auth", new { userId = user.Id, userToken = token }, HttpContext.Request.Scheme);

            await _mailService.SendEmailConfirmation(
                user.UserName,
                user.Email,
                Constant.MailMessage.CONFIRM_EMAIL_SUBJECT,
                string.Format(Constant.MailMessage.CONFIRM_EMAIL_BODY_HTML, user.UserName, url));

            // await _signInManager.SignInAsync(user, false);
            // return RedirectToAction("Index", "Home");

            ModelState.AddModelError(string.Empty,
                $"{Constant.MessageInfo.ACCOUNT_HAS_BEEN_CREATED}. {Constant.MessageInfo.CHECK_YOUR_MAIL}.");

            return View(register);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string userToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, userToken);

            ViewBag.Title = result.Succeeded ?
                Constant.MessageInfo.SUCCESS :
                Constant.MessageError.ERROR;

            ViewBag.BackgroundHeaderImg = result.Succeeded ?
                $"{Request.PathBase}/img/success-bg.jpg" :
                $"{Request.PathBase}/img/error-bg.jpg";

            ViewBag.PageHeader = ViewBag.Title;

            ViewBag.PageSubHeader = result.Succeeded ?
                $"{Constant.MessageInfo.EMAIL_CONFIRMED}. {Constant.MessageInfo.LOGIN_TO_CONTINUE}." :
                Constant.MessageError.UNKNOWN_ERROR;

            return View();
        }
    }
}
