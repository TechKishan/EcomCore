using EcomCore.Interface;
using EcomCore.Services;
using Microsoft.Data.SqlClient;
using static EcomCore.Services.DBConnection;

namespace EcomCore.Models
{
    public class User 
    {
        public int UserID { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
