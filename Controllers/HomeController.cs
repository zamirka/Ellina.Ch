using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testProject.Models;
using testProject.Infrastructure.DataAccess;
using testProject.Infrastructure.Logging;
using testProject.Infrastructure.Repositories;

namespace testProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _uof;
        private readonly IRepository<User> _usersRepo;

        public HomeController(ILogger logger, IUnitOfWork uof)
        {
            _logger = logger;
            _uof = uof;
            _usersRepo = _uof.GetRepository<User>();
        }
        public ActionResult Index()
        {
            _logger.Info("HomeController.Index() started");
            List<User> model = null;
            try
            {
                model = _usersRepo.GetAll().ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("Error in HomeController.Index()", ex);
                throw ex;
            }

            _logger.Info("HomeController.Index() ended");
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
