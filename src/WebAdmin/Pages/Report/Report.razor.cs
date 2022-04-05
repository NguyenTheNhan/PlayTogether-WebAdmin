using MudBlazor;
using System.Collections.Generic;

namespace WebAdmin.Pages.Report
{
    public partial class Report
    {
        private List<BreadcrumbItem> _breadcrumbItems => new()
        {
            new BreadcrumbItem("Trang chủ", "/index"),
            new BreadcrumbItem("Báo cáo", "/account-reports", true)
        };
    }
}