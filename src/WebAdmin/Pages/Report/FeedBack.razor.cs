using MudBlazor;
using System.Collections.Generic;

namespace WebAdmin.Pages.Report
{
    public partial class FeedBack
    {
        private List<BreadcrumbItem> _breadcrumbItems => new()
        {
            new BreadcrumbItem("Trang chủ", "/index"),
            new BreadcrumbItem("Tài khoản", "/feedback-reports", true)
        };
    }
}