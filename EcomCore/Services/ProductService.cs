using EcomCore.Interface;
using EcomCore.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using static EcomCore.Services.DBConnection;

namespace EcomCore.Services
{
    public class ProductService : IProducts
    {
        private readonly DBConnection _dbConnection;

        public ProductService(DBConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<MessageFor> AddProduct(Products data)
        {
            SqlConnection sql = new SqlConnection(_dbConnection.GetConnectionString());
            SqlCommand cmd = new SqlCommand("Sp_Products", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Action", "INSERT"));
            cmd.Parameters.Add(new SqlParameter("@Name", data.Name));
            cmd.Parameters.Add(new SqlParameter("@Description", data.Description));
            cmd.Parameters.Add(new SqlParameter("@Price", data.Price));
            cmd.Parameters.Add(new SqlParameter("@Stock", data.Stock));
            //cmd.Parameters.Add(new SqlParameter("@CategoryID",data.CategoryID));
           await sql.OpenAsync();
           await cmd.ExecuteNonQueryAsync();
            await sql.CloseAsync();
            return new MessageFor
            {
                Status = 1,
                Message = "Product added."
            };
        }


        /*
         public async Task<MessageFor> AddProduct(Products data)
{
    using (SqlConnection sql = new SqlConnection(_dbConnection.GetConnectionString()))
    {
        // Ensure the connection is opened asynchronously
        await sql.OpenAsync();

        using (SqlCommand cmd = new SqlCommand("Sp_Products", sql))
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // Add parameters to the command
            cmd.Parameters.Add(new SqlParameter("@Action", "INSERT"));
            cmd.Parameters.Add(new SqlParameter("@Name", data.Name));
            cmd.Parameters.Add(new SqlParameter("@Description", data.Description));
            cmd.Parameters.Add(new SqlParameter("@Price", data.Price));
            cmd.Parameters.Add(new SqlParameter("@Stock", data.Stock));

            // If you decide to use CategoryID, uncomment this line
            // cmd.Parameters.Add(new SqlParameter("@CategoryID", data.CategoryID));

            try
            {
                // Execute the command asynchronously
                await cmd.ExecuteNonQueryAsync();

                return new MessageFor
                {
                    Status = 1,
                    Message = "Product added."
                };
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., connection issues, SQL errors)
                return new MessageFor
                {
                    Status = 0,
                    Message = $"Error: {ex.Message}"
                };
            }
        }
    }
}

         */

        public async Task<DataTable> GetProduct()
        {
            SqlConnection sql = new SqlConnection(_dbConnection.GetConnectionString());
            SqlCommand cmd = new SqlCommand("Sp_Products", sql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Action", "GET_ALL"));

            DataTable table = new DataTable();
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            await sql.OpenAsync();
            adt.Fill(table);
            await sql.CloseAsync();
            return table;

        }

        public async Task<MessageFor> UpdateProduct(Products data)
        {
            try
            {
                SqlParameter[] sql = new SqlParameter[] {
                new SqlParameter("@Action", "UPDATE"),
                new SqlParameter("@Name", data.Name),
                new SqlParameter("@Description", data.Description),
                new SqlParameter("@Price", data.Price),
                new SqlParameter("@Stock", data.Stock),
                new SqlParameter("@ProductID", data.ProductID),

            };
                
                _dbConnection.ExecuteNonQuery("Sp_Products", sql);

                return new MessageFor()
                {
                    Status = 1,
                    Message = "Update Successfully"
                };
            }

            catch (Exception ex)
            {
                return new MessageFor()
                {
                    Status = 0,
                    Message = "Something went wrong."
                };
            }
        }
        public async Task<DataTable> GetProductById(Products data)
        {
            try
            {
                SqlConnection sql = new SqlConnection(_dbConnection.GetConnectionString());
                

                SqlCommand cmd = new SqlCommand("Sp_Products", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductID", data.ProductID));
                cmd.Parameters.Add(new SqlParameter("@Action", "GET_BY_ID"));
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                await sql.OpenAsync();
                adapter.Fill(dt);
                await sql.CloseAsync();
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
