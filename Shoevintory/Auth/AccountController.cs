using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Shoevintory.Auth.Models;
using Shoevintory.Repositories;
using Shoevintory.Models;
using Microsoft.Extensions.Configuration;


namespace Shoevintory.Auth
{
    public class AccountController : Controller
    {
        private readonly IFirebaseAuthService _firebaseAuthService;
        private readonly IUserProfileRepository _userProfileRepository;

        public AccountController(IFirebaseAuthService firebaseAuthService, IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
            _firebaseAuthService = firebaseAuthService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Credentials credentials)
        {
            if (!ModelState.IsValid)
            {
                return View(credentials);
            }

            var fbUser = await _firebaseAuthService.Login(credentials);
            if (fbUser == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(credentials);
            }

            var userProfile = _userProfileRepository.GetByFirebaseUserId(fbUser.FirebaseUserId);
            if (userProfile == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to Login.");
                return View(credentials);
            }

            await LoginToApp(userProfile);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Registration registration)
        {
            if (!ModelState.IsValid)
            {
                return View(registration);
            }

            var fbUser = await _firebaseAuthService.Register(registration);

            if (fbUser == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to register, do you already have an account?");
                return View(registration);
            }

            var newUserProfile = new UserProfile
            {
                Name = registration.Name,
                ImageUrl = registration.ImageUrl,
                UserTypeId = registration.UserType,
                Email = fbUser.Email,
                FirebaseUserId = fbUser.FirebaseUserId,
            };
            _userProfileRepository.Add(newUserProfile);

            await LoginToApp(newUserProfile);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task LoginToApp(UserProfile userProfile)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userProfile.Id.ToString()),
                new Claim(ClaimTypes.Email, userProfile.Email),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }
    }
}
