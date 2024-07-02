using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppForSmartBusiness.Models;
using WebAppForSmartBusiness.Models.Helpers;
using WebAppForSmartBusiness.Repositories.IRepositories;

namespace WebAppForSmartBusiness.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISubdivisionRepository _subdivisionRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICreatecommandForFilterRepository _createcommandForFilterRepository;

        public EmployeeController(IEmployeeRepository employeeRepository,
            ISubdivisionRepository subdivisionRepository, IPositionRepository positionRepository, IUserRepository userRepository,
            ICreatecommandForFilterRepository createcommandForFilterRepository)
        {
            _employeeRepository = employeeRepository;
            _subdivisionRepository = subdivisionRepository;
            _positionRepository = positionRepository;
            _userRepository = userRepository;
            _createcommandForFilterRepository = createcommandForFilterRepository;
        }

        [HttpGet]
        [Route("/employee")]
        public IActionResult EmployeeIndex()
        {
            EmployeeInputFilter filter = new EmployeeInputFilter();
            filter.Employees = _employeeRepository.GetEmployeesWithFullInfo();

            ViewBag.Subdivisions = GetSubdivisionItems(_subdivisionRepository.GetSubdivisions());
            ViewBag.Positions = GetPositionItems(_positionRepository.GetPositions());
            ViewBag.PeoplePartners = GetPartnerForEmployee(_employeeRepository.GetEmployeesWithFullInfo());
            return View(filter);
        }

        [HttpPost]
        [Route("/employee")]
        public IActionResult EmployeeIndex(EmployeeForView employeeFofView)
        {
            EmployeeInputFilter filter = new EmployeeInputFilter();
            filter.Employees = _employeeRepository.GetEmployeeWithFilter(_createcommandForFilterRepository.CommandForFilter(employeeFofView));

            ViewBag.Subdivisions = GetSubdivisionItems(_subdivisionRepository.GetSubdivisions());
            ViewBag.Positions = GetPositionItems(_positionRepository.GetPositions());
            ViewBag.PeoplePartners = GetPartnerForEmployee(_employeeRepository.GetEmployeesWithFullInfo());

            return View(filter);
        }

        [HttpGet]
        [Route("/employee-create")]
        public IActionResult EmployeeCreate()
        {
            ViewBag.Subdivisions = GetSubdivisionItems(_subdivisionRepository.GetSubdivisions());
            ViewBag.Positions = GetPositionItems(_positionRepository.GetPositions());
            ViewBag.PeoplePartners = GetPartnerForEmployee(_employeeRepository.GetEmployeesWithFullInfo());

            return View();
        }

        [HttpPost]
        [Route("/employee-create")]
        public IActionResult EmployeeCreate(EmployeeForView employeeInput)
        {
            if (employeeInput == null)
            {
                return BadRequest("Object is nullable");
            }

            User user = new()
            {
                FirstName = employeeInput.FirstName,
                LastName = employeeInput.LastName,
                Email = $"{employeeInput.LastName.ToLower()}{employeeInput.FirstName.ToLower()}@gmail.com",
                Password = $"{employeeInput.LastName.ToLower()}{employeeInput.FirstName.ToLower()}",
                PhoneNumber = employeeInput.PhoneNumber,
            };

            if (!_userRepository.ExistsUserByFullName(employeeInput.FirstName, employeeInput.LastName))
            {
                user = _userRepository.CreateUser(user);
            }
            else
            {
                user = _userRepository.GetUserByNameAndEmail(user);
            }

            User? personPartner = null;

            if (employeeInput.PeoplePartner != null)
            {
                personPartner = _userRepository.GetUserByName(employeeInput.PeoplePartner);
            }

            var subdivision = _subdivisionRepository.GetSubdivisionByName(employeeInput.Subdivision);
            var position = _positionRepository.GetPositionByName(employeeInput.Position);
            
            Employee employee = new()
            {
                UserId = user.UserId,
                SubdivisionId = subdivision.SubdivisionId,
                PositionId = position.PositionId,
                PeoplePartnerId = personPartner is null ? user.UserId : personPartner.UserId,
                OutOfOfficeBalance = employeeInput.OutOfOfficeBalance,
                Status = employeeInput.Status,
            };

            _employeeRepository.CreateEmployee(employee);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/employee-update/{employeeId:int}")]
        public IActionResult EmployeeUpdate(int employeeId)
        {
            var employeeToUpdate = _employeeRepository.GetEmployeesWithFullInfoById(employeeId);

            ViewBag.Subdivisions = GetSubdivisionItems(_subdivisionRepository.GetSubdivisions());
            ViewBag.Positions = GetPositionItems(_positionRepository.GetPositions());
            ViewBag.PeoplePartners = GetPartnerForEmployee(_employeeRepository.GetEmployeesWithFullInfo());

            return View(employeeToUpdate);
        }

        [HttpPost]
        [Route("/employee-update/{employeeId:int}")]
        public IActionResult EmployeesUpdate(int employeeId, EmployeeForView employeeToUpdate)
        {
            if (employeeToUpdate == null)
            {
                return BadRequest("Object is nullable");
            }

            if (employeeToUpdate.EmployeeId == employeeId)
            {
                Employee employee = _employeeRepository.GetEmployeeById(employeeId);

                User user = _userRepository.GetUserById(employee.UserId);

                var subdivision = _subdivisionRepository.GetSubdivisionByName(employeeToUpdate.Subdivision);
                var position = _positionRepository.GetPositionByName(employeeToUpdate.Position);
                var partner = _userRepository.GetUserByName(employeeToUpdate.PeoplePartner);

                if (user != null)
                {
                    user.FirstName = employeeToUpdate.FirstName;
                    user.LastName = employeeToUpdate.LastName;
                    user.PhoneNumber = employeeToUpdate.PhoneNumber;
                }

                if (employee != null)
                {
                    employee.SubdivisionId = subdivision.SubdivisionId;
                    employee.PositionId = position.PositionId;
                    employee.OutOfOfficeBalance = employeeToUpdate.OutOfOfficeBalance;
                    employee.PeoplePartnerId = partner.UserId;
                }

                _userRepository.UpdateUser(user);
                _employeeRepository.UpdateEmployee(employee);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/employee-delete/{employeeId:int}")]
        public IActionResult EmployeeDelete(int employeeId)
        {
            if (employeeId == 0)
            {
                return BadRequest("Object is nullable");
            }

            _employeeRepository.DeleteEmployeeById(employeeId);

            return RedirectToAction("Index");
        }

        private static IEnumerable<SelectListItem> GetSubdivisionItems(IEnumerable<Subdivision> subdivisions)
        {
            List<SelectListItem> subdivisionList = new List<SelectListItem>();

            foreach (Subdivision subdivision in subdivisions)
            {
                subdivisionList.Add(new SelectListItem { Text = subdivision.Name, Value = subdivision.Name });
            }

            return subdivisionList;
        }

        private static IEnumerable<SelectListItem> GetPositionItems(IEnumerable<Position> positions)
        {
            List<SelectListItem > positionList = new List<SelectListItem>();

            foreach (Position position in positions)
            {
                positionList.Add(new SelectListItem { Text = position.Name, Value = position.Name });
            }

            return positionList;
        }

        private static IEnumerable<SelectListItem> GetPartnerForEmployee(IEnumerable<EmployeeForView> peoplePartner)
        {
            List<SelectListItem> partnerList = new List<SelectListItem>();

            foreach (var person in peoplePartner)
            {
                if (person.Subdivision.ToLower() == "ceo" || person.Subdivision.ToLower() == "manager")
                {
                    partnerList.Add(new SelectListItem { Text = $"{person.FirstName} {person.LastName}", Value = $"{person.FirstName} {person.LastName}" });
                }
            }

            return partnerList;
        }
    }
}
