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
using Microsoft.JSInterop;
using WebAdmin;
using WebAdmin.Shared;
using MudBlazor;

namespace WebAdmin.Shared
{
    public partial class Error
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }

        public void HandleError(Exception ex)
        {
            Snackbar.Configuration.SnackbarVariant = Variant.Filled;
            Snackbar.Add("Something went wrong! Please try again later", Severity.Error);

            Console.WriteLine($"{ex.Message} at {DateTime.Now }");

        }
    }
}