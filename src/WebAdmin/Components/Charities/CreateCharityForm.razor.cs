using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Charities;

namespace WebAdmin.Components
{
    public partial class CreateCharityForm
    {
        [Inject]
        public ICharitiesService CharitiesService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private CharityCreate _model = new CharityCreate();
        private bool _isBusy = false;
        private string _fileName = string.Empty;
        private string _errorMessage = string.Empty;


        private async Task SubmitFormAsync()
        {
            _isBusy = true;
            try
            {
                await CharitiesService.CreateAsync(_model);
                //success
                Navigation.NavigateTo("/charities");
                Error.HandleSuccess("Đăng ký tổ chức");
            }
            catch (ApiException ex)
            {
                if (_model.Password.Contains("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"))
                {
                    if (_model.Password.Equals(_model.ConfirmPassword) == true)
                    {
                        _errorMessage = ex.ApiErrorResponse.Errors.FirstOrDefault();
                    }
                    else
                    {
                        _errorMessage = "Mật khẩu và xác nhận mật khẩu phải trùng nhau";
                    }
                }
                else
                {
                    _errorMessage = "Mật khẩu phải phải chứa ít nhất 1 chữ thường, 1 chữ hoa, 1 ký tự đặc biệt và độ dài ít nhất là 8 ký tự.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Error.HandleError(ex);

            }

            _isBusy = false;

        }
    }
}