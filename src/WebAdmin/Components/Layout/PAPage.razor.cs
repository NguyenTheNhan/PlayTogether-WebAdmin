using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;

namespace WebAdmin.Components
{
    public partial class PAPage
    {

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public List<BreadcrumbItem> BreadcrumbItems { get; set; } = new();


    }
}