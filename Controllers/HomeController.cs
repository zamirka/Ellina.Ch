using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testProject.Models;

namespace testProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbContext dbContext;

        public HomeController()
        {
            try
            {
                dbContext = new DataContext();
            }
            catch (Exception ex)
            {

            }
        }
        public ActionResult Index()
        {
            List<User> model = null;
            try
            {
                using (var context = new DataContext())
                {
                    model = context.Users.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return View(model);
        }

        [HttpPost]
        public string GetSearchResult(string someText)
        {
            //System.Threading.Thread.Sleep(55000);
            return string.Format("OK for {0}", someText);
        }

        private User SearchUserByName(string name)
        {
            return new User() { id = 1, name = "Dummy" };
        }

    }
}
