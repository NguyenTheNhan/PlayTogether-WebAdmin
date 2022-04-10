﻿using System;
using System.Threading.Tasks;
using WebAdmin.Shared.Models;
using WebAdmin.Shared.Models.Order;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface IOrderService
    {
        Task<PagedList<OrderDetail>> GetOrdersAsync(string userId, string status = null, DateTime? fromDate = null, DateTime? toDate = null, int pageNumber = 1, int pageSize = 10);
        Task<OrderDetail> GetByIdAsync(string id);
    }
}
