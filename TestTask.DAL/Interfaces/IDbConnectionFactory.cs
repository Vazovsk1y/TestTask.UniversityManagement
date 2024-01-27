using System.Data;

namespace TestTask.DAL.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection Create();
}