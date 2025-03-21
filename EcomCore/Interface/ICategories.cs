using EcomCore.Models;
using System.Data;
using static EcomCore.Services.DBConnection;

namespace EcomCore.Interface
{
    public interface ICategories
    {
        Task<DataTable> GetCategories(Categories data);
        Task<MessageFor> Insert(Categories data);
        Task<MessageFor> Update(Categories data);
    }
}
