using System;
using System.Threading;
using System.Threading.Tasks;

namespace corporate_messenger_client.Models.Timers
{
    public static class PollingTask
    {
        public static void StartTask(Action action, int seconds, CancellationToken token)
        {
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    action();
                    await Task.Delay(TimeSpan.FromSeconds(seconds), token);
                }
            }, token);
        }
    }
}