using bazy1.Models;
using bazy1.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace bazy1
{
    public partial class App : Application
    {
        private IHost _host;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<Przychodnia9Context>();
                    services.AddScoped<AppointmentNotificationService>();
                })
                .Build();

            StartBackgroundService();
        }

        private async void StartBackgroundService()
        {
            using (var serviceScope = _host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var notificationService = services.GetRequiredService<AppointmentNotificationService>();

                var cancellationTokenSource = new CancellationTokenSource();

                await Task.Factory.StartNew(async () =>
                {
                    await notificationService.RunAsync(cancellationTokenSource.Token);
                }, cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }
        }
    }
}
