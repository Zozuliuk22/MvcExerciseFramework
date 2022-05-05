using AutoMapper;
using System;
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
            var modelDto = _scenarioCreator.GetModel();
            if (modelDto is null)
                return RedirectToAction("ErrorPage", "Home");
            Mapper.Map(modelDto, model);
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
            if (model is null)
                return RedirectToAction("ErrorPage", "Home");
            _scenarioCreator.UseEnteredFee(Decimal.Parse(model.EnteredFee.Replace('.', ',').Trim()));
            return Accept();
        }

        public ActionResult Reset()
        {
            _scenarioCreator.Reset();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ShowHistory()
        {
            return RedirectToAction("Index", "History");
        }
    }
}
