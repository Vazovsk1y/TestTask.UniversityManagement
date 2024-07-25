using System.Data;

namespace TestTask.DAL.PostgreSQL.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection Create();
}