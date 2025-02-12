using Microsoft.Data.SqlClient;
using System.Data;

namespace EcomCore.Services
{
    public class DBConnection
    {
        private readonly string _connectionString;

        public DBConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Connection");
        }

        public int ExecuteNonQuery(string SpName, SqlParameter[] para)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SpName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // 📌 Stored Procedure Mode
                    if (para != null)
                        cmd.Parameters.AddRange(para);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable ExecuteGetData(string SpName, SqlParameter[] para)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SpName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // 📌 Set as Stored Procedure
                    if (para != null)
                        cmd.Parameters.AddRange(para);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }


    }
}
