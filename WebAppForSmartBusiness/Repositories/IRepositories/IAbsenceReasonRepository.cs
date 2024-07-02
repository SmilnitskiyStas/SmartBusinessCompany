using WebAppForSmartBusiness.Models;

namespace WebAppForSmartBusiness.Repositories.IRepositories
{
    public interface IAbsenceReasonRepository
    {
        IEnumerable<AbsenceReason> GetAbsenceReasons();
        AbsenceReason GetAbsenceReasonById(int absenceId);
        AbsenceReason GetAbsenceReasonByName(string absenceName);
        AbsenceReason CreateAbsenceReason(AbsenceReason absenceReason);
        AbsenceReason UpdateAbsenceReason(AbsenceReason absenceReason);
        bool DeleteAbsenceReasonById(int absenceId);
        bool ExistsAbsenceReasonById(int absenceId);
    }
}
