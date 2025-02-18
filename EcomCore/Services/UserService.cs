using EcomCore.Interface;
using EcomCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
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

        public async Task<MessageFor> AddUsesdadrInfo(User data)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@FullName", data.FullName),
                new SqlParameter("@Email", data.Email),
                new SqlParameter("@Password", Encrypt(data.Password)),
                new SqlParameter("@Role", data.Role)
                };

                _dbConnection.ExecuteNonQuery("SpAddUserProfile", parameters);

                return new MessageFor
                {
                    Status = 1 ,
                    Message =  "User added successfully"
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

        public async Task<MessageFor> AddUserInfo(User data)
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
                        cmd.Parameters.Add(new SqlParameter("@Password", Encrypt(data.Password)));  // Save hashed password
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
        private static readonly string Key = "A1B2C3D4E5F6G7H8I9J0K1L2M3N4O5P6"; // Must be 32 bytes
        private static readonly string IV = "A1B2C3D4E5F6G7H8"; // Must be 16 bytes

        public static string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.IV = Encoding.UTF8.GetBytes(IV);

                using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.IV = Encoding.UTF8.GetBytes(IV);

                using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}
