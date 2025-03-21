using EcomCore.Interface;
using EcomCore.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EcomCore.Services
{
    public class CategoriesService 
    {
        private readonly DBConnection _connection;
        public CategoriesService(DBConnection connection)
        {
            _connection = connection;
        }

      
    }
}
