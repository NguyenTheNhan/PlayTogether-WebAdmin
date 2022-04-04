using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;

namespace WebAdmin.Pages.Charities
{
    public partial class CharityDetail
    {
        [Parameter]
        public string Id { get; set; }

        private List<BreadcrumbItem> _breadcrumbItems = new()
        {
            new BreadcrumbItem("Trang chủ", "/index"),
            new BreadcrumbItem("Tổ chức từ thiện", "/charities"),
            new BreadcrumbItem("Thông tin", "/charities/details/{id}", true)
        };
    }
}