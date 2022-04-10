using MudBlazor;
using System.Collections.Generic;

namespace WebAdmin.Pages.Feedback
{
    public partial class Feedback
    {

        private List<BreadcrumbItem> _breadcrumbItems = new()
        {
            new BreadcrumbItem("Trang chủ", "/index"),
            new BreadcrumbItem("Đề xuất", "/feedbacks", true),
        };
    }
}