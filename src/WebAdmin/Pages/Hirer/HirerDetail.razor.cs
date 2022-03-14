using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;

namespace WebAdmin.Pages.Hirer
{
    public partial class HirerDetail
    {
        [Parameter]
        public string Id { get; set; }

        private List<BreadcrumbItem> _breadcrumbItems = new()
        {
            new BreadcrumbItem("Trang chủ", "/index"),
            new BreadcrumbItem("Hirer", "/hirers"),
            new BreadcrumbItem("Thông tin", "/hirers/{id}", true)
        };
    }
}