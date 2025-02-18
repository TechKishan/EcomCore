using EcomCore.Models;
using System.Data;
using static EcomCore.Services.DBConnection;

namespace EcomCore.Interface
{
    public interface IProducts
    {
        Task<MessageFor> AddProduct(Products data);
        Task<DataTable> GetProduct();
        Task<MessageFor> UpdateProduct(Products data);
        Task<DataTable> GetProductById(Products data);
    }
}
