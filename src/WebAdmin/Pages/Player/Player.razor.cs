using MudBlazor;
using System.Collections.Generic;

namespace WebAdmin.Pages.Player
{
    public partial class Player
    {
        private List<BreadcrumbItem> _breadcrumbItems => new()
        {
            new BreadcrumbItem("Trang chủ", "/index"),
            new BreadcrumbItem("Tài khoản Player", "/players", true)
        };
    }
}