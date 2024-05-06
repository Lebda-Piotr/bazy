using bazy1.Models;
using bazy1.Repositories;
using System;
using System.Windows;
using System.Windows.Input;

namespace bazy1.ViewModels.Receptionist.Pages
{
    public class AddPatientRViewModel : ViewModelBase
    {
        private string _firstName;
        private string _lastName;
        private string _pesel;
        private string _phoneNumber;
        private string _email;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Pesel
        {
            get => _pesel;
            set
            {
                _pesel = value;
                OnPropertyChanged(nameof(Pesel));
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public ICommand AddPatientCommand { get; }
        public ICommand CancelCommand { get; }

        public AddPatientViewModel()
        {
            AddPatientCommand = new BasicCommand(ExecuteAddPatientCommand);
            CancelCommand = new BasicCommand(ExecuteCancelCommand);
        }

        private void ExecuteAddPatientCommand(object obj)
        {
            PatientRepository patientRepository = new PatientRepository();
            Patient newPatient = new Patient
            {
                Name = FirstName,
                Surname = LastName,
                Pesel = Pesel,
                PhoneNumber = PhoneNumber,
                Email = Email
            };

            // Zapisz nowego pacjenta do bazy danych
            bool success = patientRepository.AddPatient(newPatient);
            if (success)
            {
                MessageBox.Show("Pacjent został dodany do bazy danych.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Wystąpił problem podczas dodawania pacjenta do bazy danych.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Czyść pola po dodaniu pacjenta
            ClearFields();
        }


        private void ExecuteCancelCommand(object obj)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Pesel = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
        }
    }
}
