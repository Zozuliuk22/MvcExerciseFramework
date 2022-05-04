using AutoMapper;
using PL.Models;
using BLL.Interfaces;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class EventController : Controller
    {
        private IScenarioCreatorService _scenarioCreator;

        public EventController(IScenarioCreatorService scenarioCreator)
        {
            _scenarioCreator = scenarioCreator;
        }

        public ActionResult Index()
        {
            var model = new EventModel();
            var item = _scenarioCreator.GetModel();
            model = Mapper.Map(item, model);
            return View(model);
        }

        public ActionResult Accept()
        {
            _scenarioCreator.Accept();
            return RedirectToAction("Index");
        }

        public ActionResult Skip()
        {
            _scenarioCreator.Skip();
            return RedirectToAction("Index");
        }

        public ActionResult EnterFee(EventModel model)
        {
            _scenarioCreator.UseEnteredFee(model.EnteredFee);
            _scenarioCreator.Accept();
            return RedirectToAction("Index");
        }

        public ActionResult Reset()
        {
            _scenarioCreator.Reset();
            return RedirectToAction("Index");
        }

        public ActionResult ShowHistory()
        {
            return RedirectToAction("Index", "History");
        }
    }
}
