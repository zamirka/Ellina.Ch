using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testProject.Models;

namespace testProject.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

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
        public String GetSearchResult(string someText)
        {
            return string.Format("OK for {0}", someText);
        }

    }
}
