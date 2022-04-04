using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;

namespace WebAdmin.Pages.Charities
{
    public partial class CreateCharity
    {
        [Parameter]
        public string Id { get; set; }

        private List<BreadcrumbItem> _breadcrumbItems = new()
        {
            new BreadcrumbItem("Trang chủ", "/index"),
            new BreadcrumbItem("Tổ chức từ thiện", "/charities"),
            new BreadcrumbItem("Đăng ký", "/charities/details/form", true)
        };
    }
}