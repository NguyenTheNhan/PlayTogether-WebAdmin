using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;
using WebAdmin;
using WebAdmin.Shared;
using WebAdmin.Components;
using MudBlazor;
using Blazored.FluentValidation;

namespace WebAdmin.Pages.GameType
{
    public partial class CreateEditGameType
    {
        [Parameter]
        public string Id { get; set; }

        private List<BreadcrumbItem> _breadcrumbItems = new()
        {
            new BreadcrumbItem("Trang chủ", "/index"),
            new BreadcrumbItem("Thể loại", "/gametypes"),
            new BreadcrumbItem("Tạo mới thể loại", "/gametypes/form", true)
        };
    }
}