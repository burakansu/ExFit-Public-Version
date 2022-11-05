using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseLayer
{
    public class SQL
    {
        string Sql = "";
        public List<T> Get<T>(string sql)
        {
            using (IDbConnection c = new SqlConnection(Sql))
            {
                return c.Query<T>(sql).ToList();
            }
        }
        public T GetSingle<T>(string sql)
        {
            using (IDbConnection c = new SqlConnection(Sql))
            {
                return c.QuerySingle<T>(sql);
            }
        }
        public void Execute(string sql, object param = null)
        {
            using (IDbConnection c = new SqlConnection(Sql))
            {
                c.Execute(sql, param);
            }
        }
    }
}
