using AutoMapper;
using System.Web.Mvc;
using BLL.Interfaces;
using BLL.Dtos;
using PL.Models;

namespace PL.Controllers
{
    public class PlayerController : Controller
    {
        private IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var playersDto = _playerService.GetAll();
            var playerModel = new PlayerViewModel();
            playerModel.Players = Mapper.Map(playersDto, playerModel.Players);
            return View(playerModel);
        }

        public ActionResult SetPlayer(PlayerViewModel playerModel)
        {
            var playerDto = new PlayerDto();
            Mapper.Map(playerModel, playerDto);
            _playerService.SetPlayer(playerDto);
            return RedirectToAction("Index", "Event");
        }
    }
}