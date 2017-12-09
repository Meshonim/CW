using System.Linq;
using System.Web.Mvc;
using CW.ViewModels;
using DalToWeb.Repositories;

namespace CW.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly MainContext ctx = new MainContext();
        
        public ActionResult Index()
        {
            var model = ctx.Users.Select(u => new UserViewModel()
            {
                Email = u.Email,
                CreationDate = u.CreationDate,
                Role = u.Role.Name
            });                

            return View(model);
        }

        public ActionResult About()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                ViewBag.AuthType = User.Identity.AuthenticationType;
            }
            ViewBag.Login = User.Identity.Name;
            ViewBag.IsAdminInRole = User.IsInRole("Administrator") ?
                "You have administrator rights." : "You do not have administrator rights.";

            return View();
            //HttpContext.Profile["FirstName"] = "Вася";
            //HttpContext.Profile["LastName"] = "Иванов";
            //HttpContext.Profile.SetPropertyValue("Age",23);
            //Response.Write(HttpContext.Profile.GetPropertyValue("FirstName"));
            //Response.Write(HttpContext.Profile.GetPropertyValue("LastName"));
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult UsersEdit()
        {
            var model = ctx.Users.Select(u => new UserViewModel
            {
                Email = u.Email,
                CreationDate = u.CreationDate,
                Role = u.Role.Name
            });

            return View(model);
        }
    }
}