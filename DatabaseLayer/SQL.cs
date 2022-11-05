using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseLayer
{
    public class SQL
    {
        public SqlConnection Connection;
        public static string Sql = "";
        public SqlCommand Command = new SqlCommand();
        public SqlDataAdapter Mega_Adapter = null;

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
        public DataTable GetTBL(string sql)
        {
            Open();
            DataTable tbl = new DataTable();
            Mega_Adapter = new SqlDataAdapter(sql, Connection);
            Mega_Adapter.Fill(tbl);
            Close();
            return tbl;
        }
        public void Open()
        {
            Connection = new SqlConnection(Sql);
            Command.Connection = Connection;
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.Open();
            }
        }
        public void Close()
        {
            if (Connection.State != System.Data.ConnectionState.Closed)
            {
                Connection.Close();
            }
            Connection.Dispose();
        }
    }
}
