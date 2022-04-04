using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;

namespace WebAdmin.Pages.Order
{
    public partial class OrderDetail
    {
        [Parameter]
        public string Id { get; set; }

        private List<BreadcrumbItem> _breadcrumbItems = new()
        {
            new BreadcrumbItem("Trang chủ", "/index"),
            new BreadcrumbItem("Tài khoản", "/hirers"),
            new BreadcrumbItem("Chi tiết thuê", "/hirers/order/{id}", true)
        };
    }
}