using bazy1.Models;
using bazy1.Repositories;
using bazy1.ViewModels.Receptionist.Pages;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using bazy1.Views.Receptionist.Pages;
using System.Threading;
using bazy1.Models.Repositories;
using bazy1.ViewModels.Doctor.Pages;

namespace bazy1.ViewModels.Receptionist
{
    public class ReceptionistViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        private string _caption;
        private User _currentUser;
        private IUserRepository _userRepository;
        private List<Patient> _patients;
        public PatientRepository _patientRepository;

        public ICommand ShowPatientRegistrationCommand { get; }
        public ICommand ShowAppointmentManagementCommand { get; }

        public ReceptionistViewModel()
        {
            _userRepository = new UserRepository();
            _patientRepository = new PatientRepository();
            CurrentUser = new User();
            LoadCurrentUser();
            ShowPatientRegistrationCommand = new BasicCommand(ExecuteShowPatientRegistrationCommand);
            ShowAppointmentManagementCommand = new BasicCommand(ExecuteShowAppointmentManagementCommand);
            ExecuteShowPatientRegistrationCommand(null); // Wyświetlanie domyślnego widoku po uruchomieniu aplikacji
        }

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public string Caption
        {
            get => _caption;
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public List<Patient> Patients // Dodaj właściwość do przechowywania listy pacjentów
        {
            get => _patients;
            set
            {
                _patients = value;
                OnPropertyChanged(nameof(Patients));
            }
        }

        private void ExecuteShowPatientRegistrationCommand(object obj)
        {
            // Ustawiamy widok rejestracji pacjenta
            CurrentViewModel = new AddPatientViewModel();
            Caption = "Rejestracja pacjenta";
        }

        private void ExecuteShowAppointmentManagementCommand(object obj)
        {
            // Ustawiamy widok zarządzania wizytami
            CurrentViewModel = new AppointmentViewModel();
            Caption = "Zarządzanie wizytami";
        }

        private void LoadCurrentUser()
        {
            try
            {
                User? user = _userRepository.findByUsername(Thread.CurrentPrincipal.Identity.Name);
                if (user != null)
                {
                    CurrentUser.Id = user.Id;
                    CurrentUser.Name = user.Name;
                    CurrentUser.Surname = user.Surname;
                    CurrentUser.Login = user.Login;
                    CurrentUser.Type = user.Type;
                    CurrentUser.Password = user.Password;
                    CurrentUser.FirstLogin = user.FirstLogin;
                    Console.Write("da: " + CurrentUser.Name + CurrentUser.Surname);

                    Patients = _patientRepository.GetPatients();
                }
            }
            catch (Exception ex)
            {
                // Obsługa wyjątku - możesz zalogować błąd lub podjąć inne działania
                Console.WriteLine("Błąd podczas ładowania bieżącego użytkownika: " + ex.Message);
            }
        }
    }
}





