using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Globalization;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;

namespace WebAdmin.Components
{
    public partial class MaintainServer
    {

        [Inject]
        public ISystemConfigService SystemConfigService { get; set; }
        [Inject]
        public IHirerService HirerService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private bool _isBusy { get; set; } = false;
        private string _errorMessage = string.Empty;
        private DateTime? _date { get; set; } = null;
        private TimeSpan? _time = new TimeSpan(08, 00, 00);
        private string tmp { get; set; }


        public async Task Reload()
        {

            // await GetSystemConfigsAsync();

        }

        private async Task SendNotiAsync()
        {
            _errorMessage = "";
            if (_date == null)
            {
                _errorMessage = "Vui lòng nhập ngày bảo trì muốn thông báo";
                return;
            }
            if ((DateTime.Compare((DateTime)_date, DateTime.Now.Date) <= 0) || (DateTime.Compare((DateTime.Now.Date.AddDays(5)), (DateTime)_date) < 0))
            {
                _errorMessage = "Ngày bảo trì không hợp lệ. \n Thời gian bảo trì dự định cần được thông báo trước ít nhất 1 ngày và tối đa 5 ngày.";
                return;
            }
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Xác nhận gửi thông báo bảo trì lúc '" + _time + " ngày " + _date?.ToString("d", CultureInfo.GetCultureInfo("en-GB")) + "' ?");
            parameters.Add("ButtonText", "Xác nhận");
            parameters.Add("Color", Color.Primary);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>("Gửi thông báo", parameters, options);

            var confirmationResult = await dialog.Result;
            if (!confirmationResult.Cancelled)
            {
                try
                {

                    var result = await HirerService.GetHirersAsync(null, null, null, 1, 100000);

                    await SystemConfigService.NotifyAll("Thông báo bảo trì hệ thống", "Hệ thống sẽ bảo trì lúc '" + _time + " ngày " + _date?.ToString("d", CultureInfo.GetCultureInfo("en-GB")) +
                                                      "'. Thời gian bảo trì dự tính là 30 phút." +
                                                      " Vui lòng không sử dụng dịch vụ của hệ thống trong khoảng thời gian này để tránh những " +
                                                      " sai sót và mất mát không cần thiết. PlayTogether xin cảm ơn!", "");
                    foreach (var item in result.Content)
                    {
                        await SystemConfigService.SendEmail(item.Email, "Thông báo bảo trì hệ thống",
                                                      "Hệ thống sẽ bảo trì lúc '" + _time + " ngày " + _date?.ToString("d", CultureInfo.GetCultureInfo("en-GB")) +
                                                      "'. Thời gian bảo trì dự tính là 30 phút." +
                                                      " Vui lòng không sử dụng dịch vụ của hệ thống trong khoảng thời gian này để tránh những " +
                                                      " sai sót và mất mát không cần thiết. PlayTogether xin cảm ơn!");
                    }

                    Error.HandleSuccess("Gửi thông báo thành công");


                }
                catch (ApiException ex)
                {
                    _errorMessage = ex.ApiErrorResponse.Message;
                    Error.HandleError(_errorMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Error.HandleError(ex);

                }
            }
            else
            {
                try
                {

                }
                catch (ApiException ex)
                {
                    _errorMessage = ex.ApiErrorResponse.Message;
                    Error.HandleError(_errorMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Error.HandleError(ex);

                }
            }
        }
    }
}

