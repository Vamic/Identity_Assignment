using Identity_Assignment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Identity_Assignment.Controllers
{
    public class AccountController : Controller
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private static UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
        private static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);
        
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "People");
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.RedirectUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                SignInStatus result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, true, false);

                if(result == SignInStatus.Success)
                    return RedirectToLocal(returnUrl);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout(string returnUrl)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToLocal(returnUrl);
        }

        public ActionResult Register(string returnUrl)
        {
            ViewBag.RedirectUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                userManager.Create(new ApplicationUser()
                {
                    UserName = model.Username
                }, password: model.Password);

                await SignInManager.PasswordSignInAsync(model.Username, model.Password, true, false);
                return RedirectToLocal(returnUrl);
            }
            return View();
        }
        
        [Authorize]
        public ActionResult BecomeAdmin()
        {
            userManager.AddToRole(User.Identity.GetUserId(), "Admin");
            var user = userManager.FindById(User.Identity.GetUserId());
            SignInManager.SignIn(user, false, false);
            return RedirectToLocal("");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RemoveAdmin()
        {
            userManager.RemoveFromRole(User.Identity.GetUserId(), "Admin");
            var user = userManager.FindById(User.Identity.GetUserId());
            SignInManager.SignIn(user, false, false);
            return RedirectToLocal("");
        }
    }
}