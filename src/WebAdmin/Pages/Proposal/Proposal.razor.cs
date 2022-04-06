using MudBlazor;
using System.Collections.Generic;

namespace WebAdmin.Pages.Proposal
{
    public partial class Proposal
    {

        private List<BreadcrumbItem> _breadcrumbItems = new()
        {
            new BreadcrumbItem("Trang chủ", "/index"),
            new BreadcrumbItem("Đề xuất", "/proposals", true),
        };
    }
}