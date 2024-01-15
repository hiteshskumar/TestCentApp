using Microsoft.AspNetCore.Authorization;

namespace ChargesWFM.UI.Policies
{
    public interface IAccessRequirement : IAuthorizationRequirement
    {
        string Module { get; }
    }
}