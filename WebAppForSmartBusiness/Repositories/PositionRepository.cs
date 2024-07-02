using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebAppForSmartBusiness.Models;
using WebAppForSmartBusiness.Repositories.IRepositories;

namespace WebAppForSmartBusiness.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly string _connectionString;

        public PositionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Position CreatePosition(Position position)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO Positions (Name) " +
                    $"VALUES ('{position.Name}')";

                return db.Query<Position>(sqlQuery).FirstOrDefault();
            }
        }

        public bool DeletePositionById(int positionId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<object>($"DELETE FROM Positions WHERE PositionId = {positionId}; SELECT CAST (SCOPE_IDENTITY() AS int);") is null; 
            }
        }

        public bool ExistsPositionById(int positionId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Position>($"SELECT * FROM Positions WHERE PositionId = {positionId}").Any();
            }
        }

        public bool ExistsPositionByName(string namePosition)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Position>($"SELECT * FROM Positions WHERE Name = '{namePosition}';").Any();
            }
        }

        public Position GetPositionById(int positionId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Position>($"SELECT * FROM Positions WHERE PositionId = {positionId}").FirstOrDefault();
            }
        }

        public Position GetPositionByName(string namePosition)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Position>($"SELECT * FROM Positions WHERE Name = '{namePosition}'").FirstOrDefault();
            }
        }

        public IEnumerable<Position> GetPositions()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Position>($"SELECT * FROM Positions").ToList();
            }
        }

        public Position UpdatePosition(Position position)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"UPDATE Positions SET Name = '{position.Name}' " +
                    $"WHERE PositionId = {position.PositionId}";

                db.Execute(sqlQuery);

                return db.Query<Position>($"SELECT * FROM Positions WHERE PositionId = {position.PositionId}").FirstOrDefault();
            }
        }
    }
}
