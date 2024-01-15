using System;
using System.Threading.Tasks;
using ChargesWFM.UI.Models;
using Blazored.LocalStorage;

namespace ChargesWFM.UI.Services
{
    public interface ILocalStorageService
    {
        Task<T> GetAsync<T>(string key);
        Task<T> GetAsync<T>();
        Task SetAsync<T>(string key, T value);
        Task SetAsync<T>(T value);
        Task DeleteAsync(string key);
        Task DeleteAsync<T>();
        Task<bool> ContainsAsync(string key);
        Task<bool> ContainsAsync<T>();
    }

    public class LocalStorageService : ILocalStorageService
    {
        private readonly ILocalStorageService _localService;
        public LocalStorageService(ILocalStorageService localService)
        {
            _localService = localService;
        }

        // public async Task<ProjectGroup> GetProjectGroupAsync()
        // {
        //     return await GetAsync<ProjectGroup>();
        // }

        // public async Task SetProjectGroupAsync(ProjectGroup projectGroup)
        // {
        //     await SetAsync(projectGroup);
        // }

        // public async Task<Placement> GetPlacementAsync()
        // {
        //     return await GetAsync<Placement>();
        // }

        // public async Task SetPlacementAsync(Placement placement)
        // {
        //     await SetAsync(placement);
        // }

        public async Task SetAsync<T>(string key, T value)
        {
            if (value == null)
            {
                await DeleteAsync<T>();
            }
            else
            {
                await _localService.SetItemAsync(key, value);
            }
        }

        public async Task SetAsync<T>(T value)
        {
            var key = typeof(T).Name;
            await SetAsync(key, value);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await _localService.GetItemAsync<T>(key);
        }

        public async Task<T> GetAsync<T>()
        {
            return await GetAsync<T>(typeof(T).Name);
        }

        public async Task DeleteAsync(string key)
        {
            if (await _localService.ContainKeyAsync(key))
            {
                await _localService.RemoveItemAsync(key);
            }
        }

        public async Task DeleteAsync<T>()
        {
            var key = typeof(T).Name;
            await DeleteAsync(key);
        }

        public async Task<bool> ContainsAsync(string key)
        {
            return await _localService.ContainKeyAsync(key);
        }

        public async Task<bool> ContainsAsync<T>()
        {
            return await ContainsAsync(typeof(T).Name);
        }
    }
}