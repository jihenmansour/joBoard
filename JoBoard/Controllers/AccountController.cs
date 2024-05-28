using AutoMapper;
using JoBoard.App_Start;
using JoBoard.Data;
using JoBoard.Models;
using JoBoard.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;

namespace JoBoard.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<MyIdentityUser> userManager;
        private readonly AppliedJobsService AjobService;
        private readonly IMapper mapper;
        public AccountController()
        {
            var db = new JobsIdentityContext();

            var userStore = new UserStore<MyIdentityUser>(db);
            userManager = new UserManager<MyIdentityUser>(userStore);
            AjobService = new AppliedJobsService();
            mapper = AutoMapperConfig.Mapper;
        }


        private async Task SignIn(MyIdentityUser myIdentityUser)
        {
            var identity = await userManager.CreateIdentityAsync(myIdentityUser, DefaultAuthenticationTypes.ApplicationCookie);


            var owinContext = Request.GetOwinContext();
            var authManager = owinContext.Authentication;
            authManager.SignIn(identity);
        }

        // Login

        [AllowAnonymous]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginData)
        {
            if (ModelState.IsValid)
            {
                var existsUser = await userManager.FindAsync(loginData.Email, loginData.Password);

                if (existsUser != null)
                {
                    await SignIn(existsUser);

                    return RedirectToAction("Index", "Home");
                }

                loginData.Message = "Email or Password is incorrect";
            }

            return View(loginData);
        }


        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel userInfo)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new MyIdentityUser
                {
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    Name = userInfo.Name
                };
                var creationResult = await userManager.CreateAsync(identityUser, userInfo.Password);

                if (creationResult.Succeeded)
                {
                    await SignIn(identityUser);
                    return RedirectToAction("Index", "Home");
                }

                var message = creationResult.Errors.FirstOrDefault();

                userInfo.Message = message;
                return View(userInfo);
            }
            return View(userInfo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            var owinContext = Request.GetOwinContext();
            var authManager = owinContext.Authentication;
            authManager.SignOut("ApplicationCookie");
            Session.Abandon();


            return RedirectToAction("Index", "Home");
        }


    }
}
