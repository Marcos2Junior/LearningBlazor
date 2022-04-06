using Microsoft.AspNetCore.Components;

namespace HelloBlazor.Pages
{
    public class TimerBase : ComponentBase, IDisposable
    {
        private int valueTimer = 25 * 60;
        private int timeLeft;
        protected string classTimer;
        protected bool startIsDisabled;
        protected bool stopIsDisabled;
        protected bool resetIsDisabled;

        protected string remaining => TimeSpan.FromSeconds(timeLeft).ToString(@"mm\:ss");
        PeriodicTimer? timer;

        public TimerBase()
        {
            timeLeft = valueTimer;
        }

        protected async Task Start()
        {
            timer?.Dispose();
            startIsDisabled = true;
            stopIsDisabled = false;
            resetIsDisabled = false;
            timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

            while (await timer.WaitForNextTickAsync())
            {
                if (timeLeft > 0)
                {
                    timeLeft -= 1;
                    if(timeLeft <= 24*60)
                    {
                        classTimer = "text-danger";
                    }
                    Console.WriteLine("timer ticked");
                    await InvokeAsync(StateHasChanged);
                }
            }
        }

        protected async Task Reset()
        {
            timer?.Dispose();
            startIsDisabled = false;
            timeLeft = valueTimer;
            await InvokeAsync(StateHasChanged);
        }

        protected void Stop()
        {
            startIsDisabled = false;
            stopIsDisabled = true;
            timer?.Dispose();
        }
        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
