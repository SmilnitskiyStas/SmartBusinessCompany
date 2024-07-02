using Microsoft.AspNetCore.Mvc;
using WebAppForSmartBusiness.Models;
using WebAppForSmartBusiness.Repositories.IRepositories;

namespace WebAppForSmartBusiness.Controllers
{
    public class SubdivisionController : Controller
    {
        private readonly ISubdivisionRepository _subdivisionRepository;

        public SubdivisionController(ISubdivisionRepository subdivisionRepository)
        {
            _subdivisionRepository = subdivisionRepository;
        }

        [HttpGet]
        [Route("/subdivision")]
        public IActionResult SubdivisionIndex()
        {
            var subdivisions = _subdivisionRepository.GetSubdivisions();
            return View(subdivisions);
        }

        [HttpGet]
        [Route("/subdivision-create")]
        public IActionResult SubdivisionCreate()
        {
            return View();
        }

        [HttpPost]
        [Route("/subdivision-create")]
        public IActionResult SubdivisionCreate(Subdivision subdivision)
        {
            if (subdivision == null)
            {
                return BadRequest("Object is nullable!");
            }

            if (!_subdivisionRepository.ExistsSubdivisionByName(subdivision.Name))
            {
                _subdivisionRepository.CreateSubdivision(subdivision);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/subdivision-update/{subdivisionId:int}")]
        public IActionResult SubdivisionUpdate(int subdivisionId)
        {
            var subdivision = _subdivisionRepository.GetSubdivisionById(subdivisionId);
            return View(subdivision);
        }

        [HttpPost]
        [Route("/subdivision-update/{subdivisionId:int}")]
        public IActionResult SubdivisionUpdate(int subdivisionId, Subdivision subdivisionUpdate)
        {
            if (subdivisionUpdate == null)
            {
                return BadRequest("Object is nullable!");
            }

            Subdivision subdivision = _subdivisionRepository.GetSubdivisionById(subdivisionId);

            subdivision.Name = subdivisionUpdate.Name;

            if (!_subdivisionRepository.ExistsSubdivisionByName(subdivisionUpdate.Name))
            {
                _subdivisionRepository.UpdateSubdivision(subdivision);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/subdivision-delete/{subdivisionId:int}")]
        public IActionResult SubdivisionDelete(int subdivisionId)
        {
            if (subdivisionId <= 0)
            {
                return BadRequest("Bad Request");
            }

            if (_subdivisionRepository.ExistsSubdivisionById(subdivisionId))
            {
                _subdivisionRepository.DeleteSubdivisionById(subdivisionId);
            }

            return RedirectToAction("Index");
        }
    }
}
