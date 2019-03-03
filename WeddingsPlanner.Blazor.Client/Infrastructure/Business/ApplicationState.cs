using System;
using WeddingsPlanner.Blazor.Client.Infrastructure.Core;

namespace WeddingsPlanner.Blazor.Client.Infrastructure.Business
{
    public class ApplicationState : IApplicationState
    {
        private string _userToken;

        private string _username;

        public ApplicationState()
        {
            SessionId = Guid.NewGuid().ToString();
        }

        public event Action OnUserDataChange;

        public string SessionId { get; set; }

        public bool IsLoggedIn => UserToken != null;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnUserDataChange?.Invoke();
            }
        }

        public string UserToken
        {
            get => _userToken;
            set
            {
                _userToken = value;
                OnUserDataChange?.Invoke();
            }
        }
    }
}