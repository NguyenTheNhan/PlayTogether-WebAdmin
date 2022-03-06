using MudBlazor;
using System.Collections.Generic;

namespace WebAdmin.Pages.Hirer
{
    public partial class Hirer
    {
        private List<BreadcrumbItem> _breadcrumbItems => new()
        {
            new BreadcrumbItem("Trang chủ", "/index"),
            new BreadcrumbItem("Tài khoản Hirer", "/hirers", true)
        };
    }
}