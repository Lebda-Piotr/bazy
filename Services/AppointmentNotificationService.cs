using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using bazy1.Models;
using Microsoft.EntityFrameworkCore;

namespace bazy1.Services
{
    public class AppointmentNotificationService
    {
        private readonly Przychodnia9Context _dbContext;

        public AppointmentNotificationService(Przychodnia9Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // Pobierz nadchodzące wizyty na następny dzień
                var tomorrow = DateTime.Today.AddDays(1);
                var appointmentsTomorrow = await _dbContext.Appointments
                    .Include(a => a.Patient)
                    .Where(a => DateTime.Parse(a.Date) >= tomorrow && DateTime.Parse(a.Date) < tomorrow.AddDays(1))
                    .ToListAsync();

                foreach (var appointment in appointmentsTomorrow)
                {
                    // Sprawdź, czy pacjent ma włączone powiadomienia
                    if (appointment.Patient.NotificationsEnabled)
                    {
                        // Tutaj dodaj logikę wysyłania powiadomienia (np. SMS lub e-mail)
                        Console.WriteLine($"Wysłano powiadomienie o wizycie pacjentowi: {appointment.Patient.Name} {appointment.Patient.Surname}");
                    }
                    else
                    {
                        Console.WriteLine($"Pacjent {appointment.Patient.Name} {appointment.Patient.Surname} ma wyłączone powiadomienia.");
                    }
                }

                // Poczekaj 1 godzinę przed sprawdzeniem ponownie
                await Task.Delay(TimeSpan.FromHours(1), cancellationToken);
            }
        }
    }
}
