using bazy1.Models;
using bazy1.Repositories;
using bazy1.ViewModels.Receptionist.Pages;
using System;
using System.Windows;

namespace bazy1.Views.Receptionist.Pages
{
    public partial class AddAppointmentWindow : Window
    {
        private readonly AppointmentRepository appointmentRepository;

        public AddAppointmentWindow()
        {
            InitializeComponent();
            appointmentRepository = new AppointmentRepository();
            DataContext = new AppointmentViewModel(); // Utwórz instancję widoku modelu
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobierz widok modelu z kontekstu danych
            var viewModel = (AppointmentViewModel)DataContext;

            // Utwórz nową wizytę na podstawie danych wprowadzonych przez użytkownika
            var newAppointment = new Appointment
            {
                DateTime = viewModel.SelectedAppointment.ToString("yyyy-MM-dd HH:mm"),
                Goal = viewModel.Goal,
                NotificationId = viewModel.NotificationId,
                PatientId = viewModel.PatientId
            };

            // Dodaj nową wizytę do bazy danych
            bool success = appointmentRepository.AddAppointment(newAppointment);
            if (success)
            {
                MessageBox.Show("Wizyta została dodana pomyślnie.");
                Close(); // Zamknij okno po dodaniu wizyty
            }
            else
            {
                MessageBox.Show("Wystąpił błąd podczas dodawania wizyty.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Zamknij okno po kliknięciu przycisku Anuluj
        }
    }
}
