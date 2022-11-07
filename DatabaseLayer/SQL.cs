using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseLayer
{
    public class SQL
    {
        public static string Sql;
        public List<T> Get<T>(string sql)
        {
            using (IDbConnection c = new SqlConnection(Sql))
            {
                return c.Query<T>(sql).ToList();
            }
        }
        public T Single<T>(string sql)
        {
            using (IDbConnection c = new SqlConnection(Sql))
            {
                return c.QuerySingle<T>(sql);
            }
        }
        public T Value<T>(string sql)
        {
            using (IDbConnection c = new SqlConnection(Sql))
            {
                return c.QueryFirstOrDefault<T>(sql); 
            }
        }
        public void Run(string sql, object param = null)
        {
            using (IDbConnection c = new SqlConnection(Sql))
            {
                c.Execute(sql, param);
            }
        }
    }
}
