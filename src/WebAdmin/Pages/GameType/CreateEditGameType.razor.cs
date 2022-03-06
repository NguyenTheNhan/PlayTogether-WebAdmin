using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;

namespace WebAdmin.Pages.GameType
{
    public partial class CreateEditGameType
    {
        [Parameter]
        public string Id { get; set; }

        private List<BreadcrumbItem> _breadcrumbItems = new()
        {
            new BreadcrumbItem("Trang chủ", "/index"),
            new BreadcrumbItem("Thể loại", "/gametypes"),
            new BreadcrumbItem("Tạo mới thể loại", "/gametypes/form", true)
        };
    }
}