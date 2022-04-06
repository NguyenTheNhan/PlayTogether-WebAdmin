using MudBlazor;
using System.Collections.Generic;

namespace WebAdmin.Pages
{
    public partial class Index
    {
        private List<BreadcrumbItem> _breadcrumbItems => new()
        { new BreadcrumbItem("Trang chủ", "/index", true), };

    }
}