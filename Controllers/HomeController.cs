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
using System.Threading;
using System.Threading.Tasks;

namespace testProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _uof;
        private readonly IRepository<User> _usersRepo;

        static Dictionary<string, Task<List<User>>> Tasks = new Dictionary<string, Task<List<User>>>();

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
            _logger.Info("HomeController.GetSearchResult(string) started");
            string taskId = Guid.NewGuid().ToString();
            try
            {
                Tasks[taskId] = Task.Factory.StartNew<List<User>>(() => SearchUserByName(nameToSearch));
            }
            catch (Exception ex)
            {
                _logger.Error("Error in HomeController.GetSearchResult(string)", ex);
            }
            _logger.Info("HomeController.GetSearchResult(string) ended");
            return Json(new { taskId = taskId });
        }

        [HttpPost]
        public JsonResult IsTaskComplete(string id)
        {
            _logger.Info("HomeController.IsTaskComplete(string) started");
            string status;
            if (!Tasks.ContainsKey(id))
            {
                _logger.Error(string.Format("Unknown Task Id : {0}", id), new KeyNotFoundException());
                status = "unknown_task";
            }
            else if (Tasks[id].IsCompleted)
            {
                _logger.Info(string.Format("Task completed. Id : {0}", id));
                status = "done";
            }
            else
            {
                _logger.Info(string.Format("Task is running. Id : {0}", id));
                status = "running";
            }
            _logger.Info("HomeController.IsTaskComplete(string) ended");
            return Json(new { status = status });
        }

        [HttpPost]
        public JsonResult End(string id)
        {
            _logger.Info("HomeController.End(string) started");
            if (!Tasks.ContainsKey(id))
            {
                _logger.Error(string.Format("Unknown Task Id : {0}", id), new KeyNotFoundException());
                return Json(new { error = "unknown task id" });
            }
            if (!Tasks[id].IsCompleted)
            {
                _logger.Info(string.Format("Finally waiting for task Id : {0}", id));
                Task.WaitAll(Tasks[id]);

            }
            _logger.Info("HomeController.End(string) ended");
            return Json(new { items = Tasks[id].Result.ToArray() });
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
            _logger.Info("HomeController.SearchUserByName(string) ended");
            
            Thread.Sleep(10 * 1000);

            return result;
        }

    }
}
