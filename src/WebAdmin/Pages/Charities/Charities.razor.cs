using MudBlazor;
using System.Collections.Generic;

namespace WebAdmin.Pages.Charities
{
    public partial class Charities
    {
        private List<BreadcrumbItem> _breadcrumbItems => new()
        {
            new BreadcrumbItem("Trang chủ", "/index"),
            new BreadcrumbItem("Tài khoản Charities", "/charities", true)
        };
    }
}