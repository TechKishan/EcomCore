using EcomCore.Models;
using static EcomCore.Services.DBConnection;

namespace EcomCore.Interface
{
    public interface IUser
    {
        Task<MessageFor> AddUserInfo(User data);
    }
}
