namespace Despro.Blazor.Base.Services
{
    public class AppService
    {
        public AppSettings Settings { get; } = new AppSettings();

        public Action OnSettingsUpdated;

        public void SettingsUpdated()
        {
            OnSettingsUpdated.Invoke();
        }
    }
}
