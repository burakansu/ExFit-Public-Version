using System.Data;
using System.Data.SqlClient;

namespace DatabaseLayer
{
    public class SQL
    {
        public SqlConnection Connection;
        public static string Sql = "data source=.\\SQLEXPRESS;Initial Catalog=ExFit_Database;Integrated Security=true";
        public SqlCommand Command = new SqlCommand();
        public SqlDataAdapter Mega_Adapter = null;
     
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
        public DataTable GetDataTable(string sql)
        {
            Open();
            DataTable tbl = new DataTable();
            Mega_Adapter = new SqlDataAdapter(sql, Connection);
            Mega_Adapter.Fill(tbl);
            Close();
            return tbl;
        }
        public int Count_Database(string sql)
        {
            Open();
            SqlCommand Command = new SqlCommand(sql, Connection);
            Int32 count = (Int32)Command.ExecuteScalar();
            Close();
            return count;
        }
    }
}
