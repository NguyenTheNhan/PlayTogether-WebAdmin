using System;
using System.Threading.Tasks;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.Order;
using WebAdmin.Shared.Responses;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IOrderService
    {
        Task<PagedList<OrderDetail>> GetOrdersAsync(string userId, string status = null, DateTime? fromDate = null, DateTime? toDate = null, int pageNumber = 1, int pageSize = 10);
        Task<ApiResponse<OrderDetail>> GetByIdAsync(string id);
    }
}
