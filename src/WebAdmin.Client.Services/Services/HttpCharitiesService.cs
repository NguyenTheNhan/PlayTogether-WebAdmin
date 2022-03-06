using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared.Models.Charities;

namespace WebAdmin.Client.Services.Services
{
    public class HttpCharitiesService : ICharitiesService
    {
        public Task<CharitiesSummary> CreateAsync(CharitiesSummary model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<CharitiesSummary> EditAsync(CharitiesSummary model)
        {
            throw new NotImplementedException();
        }

        public Task<CharitiesSummary> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CharitiesSummary>> GetCharitiessAsync(string query = null, int pageNumber = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }
    }
}
