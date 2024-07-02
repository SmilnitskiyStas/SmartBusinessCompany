using WebAppForSmartBusiness.Models;

namespace WebAppForSmartBusiness.Repositories.IRepositories
{
    public interface IPositionRepository
    {
        IEnumerable<Position> GetPositions();
        Position GetPositionById(int positionId);
        Position GetPositionByName(string namePosition);
        Position CreatePosition(Position position);
        Position UpdatePosition(Position position);
        bool DeletePositionById(int positionId);
        bool ExistsPositionById(int positionId);
        bool ExistsPositionByName(string namePosition);
    }
}
