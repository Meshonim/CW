using System.Linq;
using System.Web.Mvc;
using CW.ViewModels;
using DalToWeb.Repositories;

namespace CW.Controllers
{
    public class HomeController : Controller
    {
        private readonly MainContext db = new MainContext();
        
        public ActionResult Index()
        {
            
            //var model = db.Users.Select(u => new UserViewModel()
            //{
            //    Email = u.Email,
            //    CreationDate = u.CreationDate,
            //    Role = u.Role.Name
            //});                

            return RedirectToAction("Index", "News");
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
            var model = db.Users.Select(u => new UserViewModel
            {
                Email = u.Email,
                CreationDate = u.CreationDate,
                Role = u.Role.Name
            });

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}