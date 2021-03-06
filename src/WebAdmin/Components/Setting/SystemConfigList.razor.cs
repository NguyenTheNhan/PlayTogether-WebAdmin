using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.SystemConfig;

namespace WebAdmin.Components
{
    public partial class SystemConfigList
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
        private bool _check { get; set; } = false;
        private string _errorMessage = string.Empty;

        private List<SystemConfigDetail> _systemConfigs = new();


        protected async override Task OnInitializedAsync()
        {
            await GetSystemConfigsAsync();
            await CheckStatusAsync();

        }

        private async Task GetSystemConfigsAsync(string query = "", int pageNumber = 1, int pageSize = 10)
        {
            _isBusy = true;
            try
            {
                var result = await SystemConfigService.GetSystemConfigAsync(query, pageNumber, pageSize);
                _isBusy = false;
                _systemConfigs = result.Content.ToList();


            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
                Error.HandleError(_errorMessage);
            }
            catch (Exception ex)
            {
                Error.HandleError(ex);
            }


        }


        private async Task CheckStatusAsync()
        {
            var result = await HirerService.GetHirersAsync(null, null, null, 1, 1);
            foreach (var item in result.Content)
            {
                if (item.Status.Contains("Maintain"))
                {
                    _check = true;

                }
                else
                {
                    _check = false;

                }
            }

        }

        public async Task Reload()
        {
            //foreach (var item in _systemConfigs)
            //{
            //    MessagingCenter.Send(this, "send_reload", item);

            //}
            await GetSystemConfigsAsync();

        }

        private async Task SubmitFormAsync()
        {

            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Xác nhận lưu các thay đổi?");
            parameters.Add("ButtonText", "Lưu");
            parameters.Add("Color", Color.Primary);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<ConfirmationDialog>("Xác nhận", parameters, options);

            var confirmationResult = await dialog.Result;
            if (!confirmationResult.Cancelled)
            {
                try
                {
                    await SystemConfigService.MultiUpdate(_systemConfigs);

                    Error.HandleSuccess("Cập nhật thành công");

                    await GetSystemConfigsAsync();
                    await SystemConfigService.NotifyAll("Cập nhật hệ thống", "Các thông số cài đặt đã được thay đổi trong hệ thống. " +
                                                                             $"Thời hạn chấp nhận yêu cầu: {_systemConfigs[0].Value} phút. " +
                                                                             $"Thời hạn tiền nhận thuê khả dụng: {_systemConfigs[1].Value} giờ." +
                                                                             $"Thời hạn cho phép đánh giá và báo cáo: {_systemConfigs[2].Value} giờ. " +
                                                                             $"Phi giao dịch: {_systemConfigs[3].Value * 100}%", "");
                    StateHasChanged();

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
            //else
            //{
            //    try
            //    {
            //        await GetSystemConfigsAsync();
            //    }
            //    catch (ApiException ex)
            //    {
            //        _errorMessage = ex.ApiErrorResponse.Message;
            //        Error.HandleError(_errorMessage);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        Error.HandleError(ex);

            //    }
            //}
        }

    }
}