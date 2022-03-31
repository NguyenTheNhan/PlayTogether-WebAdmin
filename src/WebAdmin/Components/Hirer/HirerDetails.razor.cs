using Microsoft.AspNetCore.Components;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Hirer;

namespace WebAdmin.Components
{
    public partial class HirerDetails
    {
        [Inject]
        public IHirerService UserService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public string Id { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }


        private UserDetail _model = new UserDetail();
        private bool _isBusy = false;
        private string _fileName = string.Empty;
        private string _null = " ";
        private string _errorMessage = string.Empty;

        //protected override async Task OnInitializedAsync()
        //{

        //    await FetchUserByIdAsync();
        //}

        //private async Task SubmitFormAsync()
        //{
        //    _isBusy = true;
        //    try
        //    {

        //        await UserService.ActiveAsync(_model.Id, !_model.IsActive);



        //        //success
        //        Navigation.NavigateTo("/hirers");
        //    }
        //    catch (ApiException ex)
        //    {
        //        _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        Error.HandleError(ex);

        //    }

        //    _isBusy = false;

        //}

        //private async Task FetchUserByIdAsync()
        //{
        //    _isBusy = true;

        //    try
        //    {
        //        var result = await UserService.GetByIdAsync(Id);
        //        _model = result;

        //    }
        //    catch (ApiException ex)
        //    {
        //        _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        //TODO: log the error
        //        Error.HandleError(ex);

        //    }

        //    _isBusy = false;
        //}
    }
}