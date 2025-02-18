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

        public string GetConnectionString()
        {
            return _connectionString;
        }
        public class MessageFor
        {
            public int Status { get; set; }
            public string? Message { get; set; }
        }

        public int ExecuteNonQuery(string SpName, SqlParameter[] para)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SpName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (para != null)
                        cmd.Parameters.AddRange(para);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public async Task<int> ExecuteNonQueryAsync(string SpName, SqlParameter[] para)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SpName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (para != null)
                        cmd.Parameters.AddRange(para);

                    await conn.OpenAsync(); // Use OpenAsync to open the connection asynchronously
                    return await cmd.ExecuteNonQueryAsync(); // Execute the command asynchronously
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
                    cmd.CommandType = CommandType.StoredProcedure;
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
