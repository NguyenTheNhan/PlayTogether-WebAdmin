using MudBlazor;
using System.Collections.Generic;

namespace WebAdmin.Pages.GameType
{
    public partial class GameType
    {
        private List<BreadcrumbItem> _breadcrumbItems => new()
        {
            new BreadcrumbItem("Trang chủ", "/index"),
            new BreadcrumbItem("Thể loại", "/gametypes", true)
        };
    }
}