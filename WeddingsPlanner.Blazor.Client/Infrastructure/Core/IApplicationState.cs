using System;

namespace WeddingsPlanner.Blazor.Client.Infrastructure.Core
{
    public interface IApplicationState
    {
        event Action OnUserDataChange;

        string SessionId { get; set; }

        bool IsLoggedIn { get; }

        string Username { get; set; }

        string UserToken { get; set; }
    }
}