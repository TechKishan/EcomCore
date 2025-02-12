using EcomCore.Interface;
using EcomCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using static EcomCore.Services.DBConnection;

namespace EcomCore.Services
{
    public class UserService : IUser
    {
        private readonly DBConnection _dbConnection;

        public UserService(DBConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<MessageFor> AddUserInfo(User data)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@FullName", data.FullName),
                new SqlParameter("@Email", data.Email),
                new SqlParameter("@Password", data.Password),
                new SqlParameter("@Role", data.Role)
                };

                int rowsAffected = _dbConnection.ExecuteNonQuery("sp_AddUser", parameters);

                return new MessageFor
                {
                    Status = rowsAffected > 0 ? 1 : 0,
                    Message = rowsAffected > 0 ? "User added successfully" : "Failed to add user"
                };
            }
            catch (Exception ex)
            {
                return new MessageFor
                {
                    Status = 0,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<MessageFor> AddUsecvrInfo(User data)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(_dbConnection.GetConnectionString()))
                {
                    await sql.OpenAsync().ConfigureAwait(false);

                    // Check if email already exists
                    string checkQuery = "SELECT COUNT(1) FROM Users WITH(NOLOCK) WHERE Email = @Email";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, sql))
                    {
                        checkCmd.Parameters.Add(new SqlParameter("@Email", data.Email));
                        int existingUser = Convert.ToInt32(await checkCmd.ExecuteScalarAsync().ConfigureAwait(false));

                        if (existingUser > 0)
                        {
                            return new MessageFor
                            {
                                Status = 0,
                                Message = "Email already registered."
                            };
                        }
                    }

                    // Hash password before storing
                  

                    // Insert new user
                    using (SqlCommand cmd = new SqlCommand("SpAddUserProfile", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Fullname", data.FullName));
                        cmd.Parameters.Add(new SqlParameter("@Email", data.Email));
                        cmd.Parameters.Add(new SqlParameter("@Password", data.Password));  // Save hashed password
                        cmd.Parameters.Add(new SqlParameter("@Role", data.Role));

                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                    }

                    return new MessageFor
                    {
                        Status = 1,
                        Message = "User registered successfully."
                    };
                }
            }
            catch (Exception ex)
            {
         
                return new MessageFor
                {
                    Status = -1,
                    Message = $"Something went wrong: {ex.Message}"  // Return meaningful error message
                };
            }
        }
    }
}
