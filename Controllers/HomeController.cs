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
using testProject.Infrastructure.Search;

namespace testProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _uof;
        private readonly IRepository<User> _usersRepo;

        public HomeController(ILogger logger, IUnitOfWork uof)
        {
            try
            {
                _logger = logger;
                _uof = uof;
                _usersRepo = _uof.GetRepository<User>();
            }
            catch (Exception ex)
            {
                _logger.Error("Error in HomeController.ctor(ILogger, IUnitOfWork)", ex);
            }
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
            }

            _logger.Info("HomeController.Index() ended");
            
            return View(model);
        }

        [HttpPost]
        public JsonResult GetSearchResult(string nameToSearch)
        {
            System.Threading.Thread.Sleep(120000);
            var searchResults = this.SearchUserByName(nameToSearch);
            return Json(new { items = searchResults.ToArray() });
        }

        private List<User> SearchUserByName(string name)
        {
            _logger.Info("HomeController.SearchUserByName(string) started");
            List<User> result = null;
            try
            {
                var exactMatch = _usersRepo.GetAll().Where(u => u.name == name).ToList();
                var partialMatch = _usersRepo.GetAll().Where(u => u.name.Contains(name) && u.name != name).ToList();
                
                var userComparer = new SearchUserByNameComparer(name);
                partialMatch.Sort(userComparer);

                result = new List<User>();
                result.AddRange(exactMatch);
                result.AddRange(partialMatch);
            }
            catch (Exception ex)
            {
                _logger.Error("Error in HomeController.SearchUserByName(string)", ex);
            }
            _logger.Info("HomeController.SearchUserByName(string) started");
            
            return result;
        }

    }
}
