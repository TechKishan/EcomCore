using Microsoft.Data.SqlClient;
using System.Data;

namespace EcomCore.Services
{
    public class DBConnection
    {
        private readonly string _connectionString;

        public DBConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Connection")
                                ?? throw new ArgumentNullException(nameof(configuration), "Connection string not found.");
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        // Executes INSERT, UPDATE, DELETE stored procedures
        public int ExecuteNonQuery(string SpName, SqlParameter[] para)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SpName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure; 
                    if (para != null)
                        cmd.Parameters.AddRange(para);
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Database Error: {ex.Message}"); // Replace with proper logging
                return -1; // Return -1 on failure
            }
        }

        // Executes SELECT stored procedures
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

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database Error: {ex.Message}"); // Replace with proper logging
            }
            return dt;
        }
    }
}
        // Move MessageFor to a separate class
        public class MessageFor
        {
            public int Status { get; set; }
            public string? Message { get; set; }
        }


