using Microsoft.AspNetCore.Mvc;
using WebAppForSmartBusiness.Models;
using WebAppForSmartBusiness.Repositories;
using WebAppForSmartBusiness.Repositories.IRepositories;

namespace WebAppForSmartBusiness.Controllers
{
    public class PositionController : Controller
    {
        private readonly IPositionRepository _positionRepository;

        public PositionController(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        [HttpGet]
        [Route("/position")]
        public IActionResult PositionIndex()
        {
            var positions = _positionRepository.GetPositions();
            return View(positions);
        }

        [HttpGet]
        [Route("/position-create")]
        public IActionResult PositionCreate()
        {
            return View();
        }

        [HttpPost]
        [Route("/position-create")]
        public IActionResult PositionCreate(Position position)
        {
            if (position == null)
            {
                return BadRequest("Object is nullable!");
            }

            if (!_positionRepository.ExistsPositionByName(position.Name))
            {
                _positionRepository.CreatePosition(position);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/position-update/{positionId:int}")]
        public IActionResult PositionUpdate(int positionId)
        {
            var position = _positionRepository.GetPositionById(positionId);
            return View(position);
        }

        [HttpPost]
        [Route("/position-update/{positionId:int}")]
        public IActionResult PositionUpdate(int positionId, Position positionUpdate)
        {
            if (positionUpdate == null)
            {
                return BadRequest("Object is nullable!");
            }

            Position position = _positionRepository.GetPositionById(positionId);

            position.Name = positionUpdate.Name;

            if (!_positionRepository.ExistsPositionByName(positionUpdate.Name))
            {
                _positionRepository.UpdatePosition(position);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/position-delete/{positionId:int}")]
        public IActionResult PositionDelete(int positionId)
        {
            if (positionId <= 0)
            {
                return BadRequest("Bad Request");
            }

            if (_positionRepository.ExistsPositionById(positionId))
            {
                _positionRepository.DeletePositionById(positionId);
            }

            return RedirectToAction("Index");
        }
    }
}
