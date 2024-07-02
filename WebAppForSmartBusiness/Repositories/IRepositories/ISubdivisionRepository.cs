using WebAppForSmartBusiness.Models;

namespace WebAppForSmartBusiness.Repositories.IRepositories
{
    public interface ISubdivisionRepository
    {
        IEnumerable<Subdivision> GetSubdivisions();
        Subdivision GetSubdivisionById(int subdivisionId);
        Subdivision GetSubdivisionByName(string subdivisionName);
        Subdivision CreateSubdivision(Subdivision subdivision);
        Subdivision UpdateSubdivision(Subdivision subdivision);
        bool DeleteSubdivisionById(int subdivisionId);
        bool ExistsSubdivisionById(int subdivisionId);
        bool ExistsSubdivisionByName(string subdivisionName);
    }
}
