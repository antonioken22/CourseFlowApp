using System.Configuration;
using System.Data.OleDb;

namespace CourseFlow.Repositories
{
    public abstract class RepositoryBase
    {
        protected readonly string _connectionString;
        public RepositoryBase()
        {
            // _connectionString = "Server=(local) ; Database = CourseFlow; Integrated Security=true";
            _connectionString = ConfigurationManager.ConnectionStrings["CourseFlowZ"].ConnectionString;
        }

        protected OleDbConnection GetConnection()
        {
            return new OleDbConnection(_connectionString);
        }
    }
}
