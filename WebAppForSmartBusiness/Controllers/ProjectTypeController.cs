using Microsoft.AspNetCore.Mvc;
using WebAppForSmartBusiness.Models;
using WebAppForSmartBusiness.Repositories.IRepositories;

namespace WebAppForSmartBusiness.Controllers
{
    public class ProjectTypeController : Controller
    {
        private readonly IProjectTypeRepository _projectTypeRepository;
        
        public ProjectTypeController(IProjectTypeRepository projectTypeRepository)
        {
            _projectTypeRepository = projectTypeRepository;
        }

        [HttpGet]
        [Route("/projecttype")]
        public IActionResult ProjectTypeIndex()
        {
            var projectTypes = _projectTypeRepository.GetProjectTypes();
            return View(projectTypes);
        }

        [HttpGet]
        [Route("/projecttype-create")]
        public IActionResult ProjectTypeCreate()
        {
            return View();
        }

        [HttpPost]
        [Route("/projecttype-create")]
        public IActionResult ProjectTypeCreate(ProjectType projectTypeCreate)
        {
            if (projectTypeCreate == null)
            {
                return BadRequest();
            }

            if (!_projectTypeRepository.ExistsProjectTypeByName(projectTypeCreate.Name))
            {
                _projectTypeRepository.CreateProjectType(projectTypeCreate);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("projecttype-update/{projectTypeId:int}")]
        public IActionResult ProjectTypeUpdate(int projectTypeId)
        {
            var projectType = _projectTypeRepository.GetProjectTypeById(projectTypeId);
            return View(projectType);
        }

        [HttpPost]
        [Route("projecttype-update/{projectTypeId:int}")]
        public IActionResult ProjectTypeUpdate(int projectTypeId, ProjectType projectTypeUpdate)
        {
            if (projectTypeId != projectTypeUpdate.ProjectTypeId)
            {
                return BadRequest();
            }

            if (projectTypeUpdate == null)
            {
                return BadRequest();
            }

            if (_projectTypeRepository.ExistsProjectTypeById(projectTypeId))
            {
                var projectType = _projectTypeRepository.GetProjectTypeById(projectTypeId);

                projectType.Name = projectTypeUpdate.Name;

                _projectTypeRepository.UpdateProjectType(projectType);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/projecttype-delete/{projectTypeId:int}")]
        public IActionResult ProjectTypeDelete(int projectTypeId)
        {
            if (projectTypeId == 0)
            {
                return BadRequest();
            }

            if (_projectTypeRepository.ExistsProjectTypeById(projectTypeId))
            {
                _projectTypeRepository.DeleteProjectTypeById(projectTypeId);
            }

            return RedirectToAction("Index");
        }
    }
}
