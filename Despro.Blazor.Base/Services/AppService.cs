namespace Despro.Blazor.Base.Services
{
    public class AppService
    {
        public AppSettings Settings { get; set; } = new AppSettings();

        public Action OnSettingsUpdated;

        public void SettingsUpdated()
        {
            OnSettingsUpdated.Invoke();
        }
    }
}
