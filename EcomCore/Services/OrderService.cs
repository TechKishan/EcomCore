using EcomCore.Interface;
using EcomCore.Models;
using Microsoft.Data.SqlClient;
using static EcomCore.Services.DBConnection;

namespace EcomCore.Services
{
    public class OrderService : IOrder
    {

        private readonly DBConnection _dbConnection;

        public OrderService(DBConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<MessageFor> OrderInsert(Orders data)
        {
            try
            {
                SqlConnection sql = new SqlConnection(_dbConnection.GetConnectionString());
                SqlCommand cmd = new SqlCommand("Sp_Orders", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Action", "INSERT"));
                cmd.Parameters.Add(new SqlParameter("@UserId", data.UserId));
                cmd.Parameters.Add(new SqlParameter("@ShippingAddress", data.ShippingAddress));
                cmd.Parameters.Add(new SqlParameter("@TotalAmount", data.TotalAmount));
               await sql.OpenAsync();
                cmd.ExecuteNonQuery();
                await sql.CloseAsync();
                return new MessageFor
                {
                    Status = 1,
                    Message = "Order created Successfully."
                };
            }
            catch (Exception ex)
            {
                return new MessageFor
                {
                    Status = 0,
                    Message = "Something went wrong.",
                };
            }
        }
        public async Task<MessageFor> OrderUpdate(Orders data)
        {
            try
            {
                SqlConnection sql = new SqlConnection(_dbConnection.GetConnectionString());
                SqlCommand cmd = new SqlCommand("Sp_Orders", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Action", "UPDATE"));
                cmd.Parameters.Add(new SqlParameter("@OrderId", data.OrderId));
                cmd.Parameters.Add(new SqlParameter("@OrderStatus", data.OrderStatus));
                cmd.Parameters.Add(new SqlParameter("@PaymentStatus", data.PaymentStatus));
               await sql.OpenAsync();
                cmd.ExecuteNonQuery();
               await sql.CloseAsync();
                return new MessageFor
                {
                    Status = 1,
                    Message = "Order updated Successfully."
                };
            }
            catch (Exception ex)
            {
                return new MessageFor
                {
                    Status = 0,
                    Message = "Something went wrong.",
                };
            }
        }
        public async Task<MessageFor> OrderDelete(Orders data)
        {
            try
            {
                SqlConnection sql = new SqlConnection(_dbConnection.GetConnectionString());
                SqlCommand cmd = new SqlCommand("Sp_Orders", sql);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Action", "DELETE"));
                cmd.Parameters.Add(new SqlParameter("@OrderId", data.OrderId));

                await sql.OpenAsync();
                cmd.ExecuteNonQuery();
                await sql.CloseAsync();
                return new MessageFor
                {
                    Status = 1,
                    Message = "Order Delete Successfully."
                };
            }
            catch (Exception ex)
            {
                return new MessageFor
                {
                    Status = 0,
                    Message = "Something went wrong.",
                };
            }
        }
    }
}
