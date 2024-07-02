using WebAppForSmartBusiness.Models.Helpers;
using WebAppForSmartBusiness.Repositories.IRepositories;

namespace WebAppForSmartBusiness.Repositories
{
    public class CreateCommandForFilterRepository : ICreatecommandForFilterRepository
    {
        private readonly ISubdivisionRepository _subdivisionRepository; 
        private readonly IPositionRepository _positionRepository;
        private readonly IUserRepository _userRepository;

        public CreateCommandForFilterRepository(ISubdivisionRepository subdivisionRepository, IPositionRepository positionRepository, IUserRepository userRepository)
        {
            _subdivisionRepository = subdivisionRepository;
            _positionRepository = positionRepository;
            _userRepository = userRepository;
        }


        public string CommandForFilter(EmployeeForView employee)
        {
            string sqlQuery = "";

            if (employee.FirstName is not null)
            {
                var user = _userRepository.GetUserByName($"{employee.FirstName} {employee.LastName}");

                sqlQuery += $"AND e.FullName = {user.UserId} ";
            }

            if (employee.Subdivision is not null)
            {
                var subdivision = _subdivisionRepository.GetSubdivisionByName(employee.Subdivision);

                sqlQuery += $"AND e.Subdivision = {subdivision.SubdivisionId} ";
            }

            if (employee.Position is not null)
            {
                var position = _positionRepository.GetPositionByName(employee.Position);

                sqlQuery += $"AND e.Position = {position.PositionId} ";
            }

            if (employee.PeoplePartner is not null)
            {
                var partner = _userRepository.GetUserByName(employee.PeoplePartner);

                sqlQuery += $"AND e.PeoplePartner = {partner.UserId} ";
            }

            if (employee.OutOfOfficeBalance > 0)
            {
                sqlQuery += $"AND e.OutOfOfficeBalance = {employee.OutOfOfficeBalance} ";
            }

            if (employee.PhoneNumber is not null)
            {
                sqlQuery += $"AND e.Phone = '{employee.PhoneNumber}' ";
            }

            if (employee.Status is not null)
            {
                sqlQuery += $"AND e.Status = '{employee.Status}' ";
            }

            return sqlQuery;
        }
    }
}
