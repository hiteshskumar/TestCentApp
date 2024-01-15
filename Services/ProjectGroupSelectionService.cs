using System;
using System.Threading.Tasks;
using ChargesWFM.UI.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace ChargesWFM.UI.Services
{
    public interface IProjectGroupSelectionService
    {
        event EventHandler ChangeActiveStatusPlacement;

        event EventHandler ChangeActiveStatusProjectGroup;

        event EventHandler ChangePlacement;

        event EventHandler<ProcessClientChangedEventArgs> ProcessClientChanged;

        event EventHandler<ProjectGroupChangedEventArgs> ProjectGroupChanged;

        event EventHandler<ClientLoginChangedEventArgs> ClientLoginChanged;

        event EventHandler<ClientLoginVisibilityChangedEventArgs> ClientLoginVisibilityChanged;

        Task Refresh();

        Task SetPlacement(int placementId);

        Task SetActiveStatusProjectGroup(NexgenProjectGroups projectGroup, ProcessClients processClient);

        void OnProcessClientChanged(ProcessClients processClient);

        void OnProjectGroupChanged(NexgenProjectGroups projectGroup);

        void OnClientLoginChanged(string clientLogin);

        Task VisibleClient(bool isVisible);
    }

    public class ProjectGroupSelectionService : IProjectGroupSelectionService
    {
        private readonly AuthenticationStateProvider authprovider;
        private readonly ILocalStorageService localStorageService;

        public ProjectGroupSelectionService(AuthenticationStateProvider authProvider, ILocalStorageService localStorageService)
        {
            authprovider = authProvider;
            this.localStorageService = localStorageService;
        }

        public event EventHandler ChangeActiveStatusPlacement;

        public event EventHandler ChangeActiveStatusProjectGroup;
        
        public event EventHandler ChangePlacement;

        public event EventHandler<ProcessClientChangedEventArgs> ProcessClientChanged;

        public event EventHandler<ProjectGroupChangedEventArgs> ProjectGroupChanged;

        public event EventHandler<ClientLoginChangedEventArgs> ClientLoginChanged;

        public event EventHandler<ClientLoginVisibilityChangedEventArgs> ClientLoginVisibilityChanged;

        public Task VisibleClient(bool isVisible)
        {
            ClientLoginVisibilityChanged?.Invoke(this, new ClientLoginVisibilityChangedEventArgs { IsVisible = isVisible });
            return Task.CompletedTask;
        }

        public async Task Refresh()
        {
            await localStorageService.DeleteAsync("AuthenticatedUser");
            await authprovider.GetAuthenticationStateAsync();
        }
        
        public Task SetActiveStatusProjectGroup(NexgenProjectGroups projectGroup, ProcessClients processClient)
        {
            ChangeActiveStatusProjectGroup?.Invoke(this, new ChangeProjectGroupActiveStatusEventArgs { ProjectGroup = projectGroup, ProcessClient = processClient });
            return Task.CompletedTask;
        }

        public Task SetPlacement(int placementId)
        {
            ChangePlacement?.Invoke(this, new ChangePlacementEventArgs { PlacementId = placementId });
            return Task.CompletedTask;
        }

        public void OnProcessClientChanged(ProcessClients processClient)
        {
            ProcessClientChanged?.Invoke(this, new ProcessClientChangedEventArgs { ProcessClient = processClient });
        }

        public void OnProjectGroupChanged(NexgenProjectGroups projectGroup)
        {
            ProjectGroupChanged?.Invoke(this, new ProjectGroupChangedEventArgs { ProjectGroup = projectGroup });
        }

        public void OnClientLoginChanged(string clientLogin)
        {
            ClientLoginChanged?.Invoke(this, new ClientLoginChangedEventArgs { ClientLogin = clientLogin });
        }
    }

    public class ChangeProjectGroupActiveStatusEventArgs : EventArgs
    {
        public NexgenProjectGroups? ProjectGroup { get; set; }
        
        public ProcessClients? ProcessClient { get; set; }
    }

    public class ChangePlacementEventArgs : EventArgs
    {
        public int PlacementId { get; set; }
    }

    public class ProcessClientChangedEventArgs : EventArgs
    {
        public ProcessClients ProcessClient { get; set; }
    }

    public class ProjectGroupChangedEventArgs : EventArgs
    {
        public NexgenProjectGroups ProjectGroup { get; set; }
    }
    public class ClientLoginChangedEventArgs : EventArgs
    {
        public string ClientLogin { get; set; }
    }

    public class ClientLoginVisibilityChangedEventArgs : EventArgs
    {
        public bool IsVisible { get; set; }
    }
}
