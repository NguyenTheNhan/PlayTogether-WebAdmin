using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;

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