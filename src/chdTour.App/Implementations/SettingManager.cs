using chd.UI.Base.Client.Implementations.Services.Base;
using chd.UI.Base.Contracts.Interfaces.Services;
using chd.UI.Base.Contracts.Interfaces.Services.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chdTour.App.Implementations
{
    public class SettingManager : BaseClientSettingManager<int, int>, ISettingManager
    {
        private readonly IConfiguration _configuration;

        public SettingManager(ILogger<SettingManager> logger, 
            IConfiguration configuration,
            IProtecedLocalStorageHandler protecedLocalStorageHandler,
            NavigationManager navigationManager) : base(logger, protecedLocalStorageHandler, navigationManager)
        {
            this._configuration = configuration;
        }
        public T? GetNativSetting<T>(string key) where T : class
        {
            if (Preferences.ContainsKey(key))
            {
                return Preferences.Default.Get<T>(key, default(T));
            }
            return default(T);
        }

        public void SetNativSetting<T>(string key, T value) where T : class
        {
            Preferences.Default.Set<T>(key, value);
        }

    }
    public interface ISettingManager : IBaseClientSettingManager
    {
        T? GetNativSetting<T>(string key) where T : class;
        void SetNativSetting<T>(string key, T value) where T : class;
    }
}
