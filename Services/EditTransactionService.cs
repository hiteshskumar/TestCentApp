using System;
using System.Threading.Tasks;
using ChargesWFM.UI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ChargesWFM.UI.Services
{
    public interface IEditTransactionService
    {
        Task<ResultResponse> ModifyTransactionAsync(EmployeeAccountDetails data);
        Task<ResultResponse> SaveAccounts(EmployeeAccountDetails data);
       Task<ResultResponse> UpdateSkipReason(SkipReasonModel data);
    }

    public class EditTransactionService : IEditTransactionService
    {
        private readonly AuthStateProvider authenticationProvider;
        private readonly HttpClient httpClient;
        public EditTransactionService(AuthenticationStateProvider authenticationProvider, HttpClient httpClient)
        {
            this.authenticationProvider = (AuthStateProvider)authenticationProvider;
            this.httpClient = httpClient;
        }

        public async Task<ResultResponse> ModifyTransactionAsync(EmployeeAccountDetails data)
        {
            var token = authenticationProvider.Token;
            const string url = "/EditProduction/UpdateTransaction";
            return await ApiHelper.PostUsingMsgPackAsync<ResultResponse>(url, data, httpClient, token);
        }
        public async Task<ResultResponse> SaveAccounts(EmployeeAccountDetails data)
        {
            var token = authenticationProvider.Token;
            const string url = "/Production/SaveAccounts";
            return await ApiHelper.PostUsingMsgPackAsync<ResultResponse>(url, data, httpClient, token);
        }
        public async Task<ResultResponse> UpdateSkipReason(SkipReasonModel data)
        {
            var token = authenticationProvider.Token;
            const string url = "/Production/UpdateSkipReason";
            return await ApiHelper.PostUsingMsgPackAsync<ResultResponse>(url, data, httpClient, token);
        }
    }
}