using CheckListApi.Models;
using CheckListWPF.Resources.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CheckListWPF.Resources.Helpers
{
    public class CheckListEndpoint : ICheckListEndpoint
    {
        private readonly IApiHelper _apiHelper;

        public CheckListEndpoint(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<IEnumerable<Canvas>> GetUserCanvasList()
        {
            List<Canvas> output;

            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync(_apiHelper.ApiClient.BaseAddress + "CheckList"))
            {
                if (response.IsSuccessStatusCode)
                {
                    output = await response.Content.ReadFromJsonAsync<List<Canvas>>();

                    return output;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }
    }
}
