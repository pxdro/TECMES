namespace TECMES.Client.Services.ReloadServices
{
    public interface IReloadService
    {
        void RegisterEvent(string eventName, Action handler);

        void RaiseEvent(string eventName);
    }
}
