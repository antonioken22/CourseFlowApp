using System.Configuration;
using System.Data.SqlClient;

namespace CourseFlow.Repositories
{
    public abstract class RepositoryBase
    {
        protected readonly string _connectionString;
        public RepositoryBase()
        {
            // _connectionString = "Server=(local) ; Database = CourseFlow; Integrated Security=true";
            _connectionString = ConfigurationManager.ConnectionStrings["CourseFlow"].ConnectionString;
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
