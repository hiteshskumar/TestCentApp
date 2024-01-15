using System.Globalization;
using System;
using System.Threading.Tasks;
using ChargesWFM.UI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
namespace ChargesWFM.UI.Services
{
    public interface IQMSService
    {
       Task<string> GetPendingAccountdetailsForce(int EmployeeId);
    }

    public class QMSService : IQMSService
    {
        private readonly AuthStateProvider authenticationProvider;
        private readonly HttpClient httpClient;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly string qmsBaseHref;
        public QMSService(AuthenticationStateProvider _authenticationProvider,
         HttpClient _httpClient,
         IHttpClientFactory _httpClientFactory,
          IConfiguration configuration)
        {
            this.authenticationProvider = (AuthStateProvider)_authenticationProvider;
            this.httpClient = _httpClient;
            this.httpClientFactory = _httpClientFactory;
            qmsBaseHref = configuration["QMSBaseHref"]!;
        }

        public async Task<string> GetPendingAccountdetailsForce(int EmployeeId)
        {
            using var httpClient = httpClientFactory.CreateClient("QMS");
            var url = $"{qmsBaseHref}/getPendingAccountdetailsForce/employeeid/{EmployeeId}";
            return await ApiHelper.GetUsingMsgPackAsync<string>(url, httpClient, authenticationProvider.Token);
        }
    }
}