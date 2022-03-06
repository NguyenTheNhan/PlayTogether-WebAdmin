using MudBlazor;
using System.Collections.Generic;

namespace WebAdmin.Pages.Game
{
    public partial class Game
    {
        private List<BreadcrumbItem> _breadcrumbItems => new()
        {
            new BreadcrumbItem("Trang chủ", "/index"),
            new BreadcrumbItem("Games", "/games", true)
        };
    }
}