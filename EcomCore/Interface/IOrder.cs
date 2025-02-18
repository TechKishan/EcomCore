using EcomCore.Models;
using static EcomCore.Services.DBConnection;

namespace EcomCore.Interface
{
    public interface IOrder
    {
        public Task<MessageFor> OrderInsert(Orders data);
        public Task<MessageFor> OrderUpdate(Orders data);
        public Task<MessageFor> OrderDelete(Orders data);
    }
}
