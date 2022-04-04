using Microsoft.AspNetCore.Components;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared;
using WebAdmin.Shared.Models.Charities;

namespace WebAdmin.Components
{
    public partial class CharityDetails
    {
        [Inject]
        public ICharitiesService CharityService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public string Id { get; set; }

        [CascadingParameter]
        public Error Error { get; set; }


        private CharitiesSummary _model = new CharitiesSummary();
        private bool _isBusy = false;
        private string _fileName = string.Empty;

        private string _errorMessage = string.Empty;
    }
}