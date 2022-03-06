using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;

namespace WebAdmin.Pages.Game
{
    public partial class CreateEditGame
    {
        [Parameter]
        public string Id { get; set; }

        private List<BreadcrumbItem> _breadcrumbItems = new()
        {
            new BreadcrumbItem("Trang chủ", "/index"),
            new BreadcrumbItem("Games", "/games"),
            new BreadcrumbItem("Thông tin Game", "/games/form", true)
        };
    }
}