using MudBlazor;
using System.Collections.Generic;

namespace WebAdmin.Pages.Setting
{
    public partial class SettingPage
    {
        private List<BreadcrumbItem> _breadcrumbItems => new()
        {
            new BreadcrumbItem("Trang chủ", "/index"),
            new BreadcrumbItem("Cài đặt", "/settings", true)
        };
    }
}