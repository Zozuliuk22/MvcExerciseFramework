using System.Web.Mvc;
using BLL.Interfaces;
using PL.Models;

namespace PL.Controllers
{
    public class HistoryController : Controller
    {
        private IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new EventHistoryViewModel();
            model.EventsHistory = _historyService.EventsHistory;
            return View(model);
        }
    }
}