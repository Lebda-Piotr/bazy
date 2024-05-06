using bazy1.Repositories;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;

namespace bazy1.ViewModels
{
    class LoginViewModel : ViewModelBase
    {


        private string _username;
        private string _password;
        private string _errorMessage;
        private string _loginMessage;
        private string _passwordMessage;

        public ICommand LoginCommand { get; }
        private IUserRepository userRepository;

        public event EventHandler LoginCompleted;

        //Jeśli nie udało się zalogować - okno cały czas widoczne
        private bool isVisible = true;

        public LoginViewModel() {
			File.WriteAllText("C:\\utut.txt", "dsfsdfsd");
            Console.WriteLine(File.ReadAllText("C:\\utut.txt"));

			var prescription = new Prescription() {Doctor = new Models.Doctor() { Name = "Jan", Surname="Kowalski", PhoneNumber = "2234569797",
                Specializations = new List<Specialization>() {new Specialization { Name = "Chirurg" } } },
                Patient = new Patient() { Pesel = "02658769845", Name="Adam", Surname="Nowak",
                Addresses = new List<Address>() { new Address() { City = "Warszawa", BuildingNumber = "124", Street = "Prosta" } } },
                Medicines = new List<Medicine>() { new Medicine() { Fraction = 1F, Name="Trexan", Amount = 2, Dose="10 mg",Comments="1 raz na dobę przez 3 miesiące" },
                new Medicine() {Name="Naproxen", Amount = 2, Dose = "20ml", Comments="3 razy dziennie po posiłku", Fraction=0.5F } },
                DateOfPrescription = DateTime.Now

                
            };
			userRepository = new UserRepository();

            //Wygeneruj pierwsze konto admina
            ((UserRepository)userRepository).adminGenerate();
            //ustawiamy komendę dla viewmodelu
            LoginCommand = new BasicCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        }
        private bool CanExecuteLoginCommand(object obj)
        {
            return !(string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password));
        }

        private void ExecuteLoginCommand(object obj)
        {
            var isValid = userRepository.authenticate(new System.Net.NetworkCredential(Username, Password));
            if (isValid)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);
                LoginCompleted(this, new UserEventArgs(userRepository.findByUsername(Username).Type, userRepository.findByUsername(Username).FirstLogin));// Użytkownik zalogowany, wywołaj komendę z LoginCompleted -> ukryj okno
            }
            else
            {
                ErrorMessage = "Niepoprawne dane logowania";
            }
        }

        //Przy zmianie którejś ze składowych wywołać OnPropertyChanged
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
            }
        }

        public string LoginMessage
        {
            get => _loginMessage;
            set
            {
                _loginMessage = value;
                OnPropertyChanged(nameof(LoginMessage));
            }
        }
        public string PasswordMessage
        {
            get => _passwordMessage;
            set
            {
                _passwordMessage = value;
                OnPropertyChanged(nameof(PasswordMessage));
            }
        }
    }

}