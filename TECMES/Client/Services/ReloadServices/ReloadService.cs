namespace TECMES.Client.Services.ReloadServices
{
    public class ReloadService : IReloadService
    {
        private readonly Dictionary<string, List<Action>> eventHandlers = new();

        public void RegisterEvent(string eventName, Action handler)
        {
            if (!eventHandlers.ContainsKey(eventName))
            {
                eventHandlers[eventName] = new List<Action>();
            }

            eventHandlers[eventName].Add(handler);
        }

        public void RaiseEvent(string eventName)
        {
            if (eventHandlers.TryGetValue(eventName, out var handlers))
            {
                foreach (var handler in handlers)
                {
                    handler?.Invoke();
                }
            }
        }
    }
}