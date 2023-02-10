using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficManagementSystem.UI.Infrastructure.Managers
{
    public class AppStateManager
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                NotifyStateChanged();
            }
        }

        private string? _currentUsername;
        public string? CurrentUsername
        {
            get => _currentUsername;
            set
            {
                _currentUsername = value;
                NotifyStateChanged();
            }
        }

        
        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
