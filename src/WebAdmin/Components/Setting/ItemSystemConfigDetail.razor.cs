using AKSoftware.Blazor.Utilities;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Exceptions;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.SystemConfig;

namespace WebAdmin.Components
{
    public partial class ItemSystemConfigDetail
    {
        [Inject]
        public ISystemConfigService SystemConfigService { get; set; }

        [Parameter]
        public SystemConfigDetail Item { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }

        private string _name { get; set; }

        private bool check = false;

        public bool _isBusy = false;

        private float _value = 0;

        private string _errorMessage = string.Empty;


        private async Task GetItemAsync()
        {
            _errorMessage = string.Empty;
            try
            {

                _isBusy = true;
                //Call Api to edit ToDo Item
                var result = await SystemConfigService.GetByIdAsync(Item.Id);
                Item = result.Content;
                // MessagingCenter.Send(this, "item_config_edited", Item);

                //Notify the parent about the edited item
                //await OnItemEdited.InvokeAsync(result.Content);
            }
            catch (ApiException ex)
            {
                //TODO: Handle error globally
                _errorMessage = ex.ApiErrorResponse.Message;
                Error.HandleError(_errorMessage);
            }
            catch (Exception ex)
            {

                //TODO: Handle error globally
                Error.HandleError(ex);
            }
            _isBusy = false;
        }

        protected override async Task OnInitializedAsync()
        {
            await GetItemAsync();
            switch (Item.No)
            {
                case 1:
                    _name = "Thời hạn chấp nhận yêu cầu";
                    break;
                case 2:
                    _name = "Thời hạn tiền nhận thuê khả dụng";
                    break;
                case 3:
                    _name = "Thời hạn cho phép đánh giá và báo cáo";
                    break;
                case 4:
                    _name = "Phi giao dịch";
                    break;
            }
        }
        protected override void OnInitialized()
        {

            MessagingCenter.Subscribe<SystemConfigList, SystemConfigDetail>(this, "send_save", async (sender, args) =>
            {
                await GetItemAsync();
                StateHasChanged();
            });
            MessagingCenter.Subscribe<SystemConfigList, SystemConfigDetail>(this, "send_reload", async (sender, args) =>
            {
                await GetItemAsync();
                StateHasChanged();
            });
        }
    }
}