using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using WebAppForSmartBusiness.Models;
using WebAppForSmartBusiness.Models.Helpers;
using WebAppForSmartBusiness.Repositories.IRepositories;

namespace WebAppForSmartBusiness.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectTypeRepository _projectTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ProjectController(IProjectRepository projectRepository, IProjectTypeRepository projectTypeRepository,
            IUserRepository userRepository, IEmployeeRepository employeeRepository)
        {
            _projectRepository = projectRepository;
            _projectTypeRepository = projectTypeRepository;
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        [Route("/project")]
        public IActionResult ProjectIndex()
        {
            ProjectInputFilter filter = new ProjectInputFilter();
            var projects = _projectRepository.GetProjectsFullInfo();
            filter.Projects = projects;

            return View(filter);
        }

        [HttpGet]
        [Route("/project-create")]
        public IActionResult ProjectCreate()
        {
            ViewBag.ProjectTypes = GetProjectTypes(_projectTypeRepository.GetProjectTypes());
            ViewBag.ProjectManagers = GetProjectManager(_employeeRepository.GetEmployeesWithFullInfo());

            return View();
        }

        [HttpPost]
        [Route("/project-create")]
        public IActionResult ProjectCreate(ProjectForView projectCreate)
        {
            if (projectCreate == null)
            {
                return BadRequest("Object is nullable!");
            }

            var projectManager = _userRepository.GetUserByName(projectCreate.ProjectManager);

            var projectType = _projectTypeRepository.GetProjectTypeByName(projectCreate.ProjectType);

            Project project = new Project()
            {
                ProjectTypeId = projectType.ProjectTypeId,
                ProjectManagerId = projectManager.UserId,
                StartDate = projectCreate.StartDate,
                EndDate = projectCreate.EndDate,
                Comment = projectCreate.Comment,
                Status = projectCreate.Status,
            };

            _projectRepository.CreateProject(project);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/project-update/{projectId:int}")]
        public IActionResult ProjectUpdate(int projectId)
        {
            ProjectForView project = _projectRepository.GetProjectById(projectId);

            ViewBag.ProjectTypes = GetProjectTypes(_projectTypeRepository.GetProjectTypes());
            ViewBag.ProjectManagers = GetProjectManager(_employeeRepository.GetEmployeesWithFullInfo());

            return View(project);
        }

        [HttpPost]
        [Route("/project-update/{projectId:int}")]
        public IActionResult ProjectUpdate(int projectId, ProjectForView projectUpdate)
        {
            if (projectId != projectUpdate.ProjectId)
            {
                return BadRequest();
            }

            var projectTypeId = _projectTypeRepository.GetProjectTypeByName(projectUpdate.ProjectType);
            var userId = _userRepository.GetUserByName(projectUpdate.ProjectManager);

            if (_projectRepository.ExistsProjectById(projectId))
            {
                Project project = new Project()
                {
                    ProjectId = projectId,
                    ProjectTypeId = projectTypeId.ProjectTypeId,
                    ProjectManagerId = userId.UserId,
                    StartDate = projectUpdate.StartDate,
                    EndDate = projectUpdate.EndDate,
                    Comment = projectUpdate.Comment,
                    Status = projectUpdate.Status,
                };

                _projectRepository.UpdateProject(project);
            }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/project-delete/{projectId:int}")]
        public IActionResult ProjectDelete(int projectId)
        {
            if (projectId == 0)
            {
                return BadRequest();
            }

            if (_projectRepository.ExistsProjectById(projectId))
            {
                _projectRepository.DeleteProjectById(projectId);
            }

            return RedirectToAction("Index");
        }

        private static IEnumerable<SelectListItem> GetProjectTypes(IEnumerable<ProjectType> projectTypes)
        {
            List<SelectListItem> projects = new List<SelectListItem>();

            foreach (var projectType in projectTypes)
            {
                projects.Add(new SelectListItem { Text = projectType.Name, Value = projectType.Name });
            }

            return projects;
        }

        private static IEnumerable<SelectListItem> GetProjectManager(IEnumerable<EmployeeForView> users)
        {
            List<SelectListItem> projectManagers = new List<SelectListItem>();

            foreach (var user in users)
            {
                if (user.Subdivision.ToLower() == "manager")
                {
                    projectManagers.Add(new SelectListItem { Text = $"{user.FirstName} {user.LastName}", Value = $"{user.FirstName} {user.LastName}" });
                }
            }

            return projectManagers;
        }
    }
}
