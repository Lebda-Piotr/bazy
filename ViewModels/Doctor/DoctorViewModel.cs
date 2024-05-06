using bazy1.Models;
using bazy1.Repositories;
using bazy1.Utils;
using bazy1.ViewModels.Admin.Pages;
using bazy1.ViewModels.Doctor.Pages;
using bazy1.Views.Doctor.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static bazy1.ViewModels.Doctor.Pages.AddMedicationViewModel;
using FirstLoginViewModel = bazy1.ViewModels.Doctor.Pages.FirstLoginViewModel;

namespace bazy1.ViewModels.Doctor
{

    public class DoctorViewModel : ViewModelBase
    {

        private ViewModelBase? _currentViewModel;
        private string _caption;
        private bool _firstLogin = true;
        private string errorMessage;
        private FirstLoginViewModel? _firstLoginViewModel;
        private string _tag;
        public ICommand ShowDashboardLoggedInCommand { get; }
        public ICommand Klik { get; set; }



        //komendy dla wszystkich widoków w oknie
        public ICommand ShowDashboardViewCommand { get; }

        private void ExecuteShowDashboardViewCommand(object? obj)
        {
            //Jeżeli użytkownik loguje się po raz pierwszy wyświetl widok zmiany hasła, jeśli nie - ekran główny
            //_currentUser.Surname = "dsds";

            Console.WriteLine("executeshowdashobard ");

			//Console.WriteLine(_currentUser.FirstLogin + CurrentUser.Login);
			if (_currentUser.FirstLogin)
			{

	CurrentViewModel = new FirstLoginViewModel(_currentUser);	
			}
			else
			{
				if (CurrentViewModel is PrescriptionsViewModel viewModel)
				{
				}
				CurrentViewModel = new Pages.DashboardViewModel(_currentUser);
			}	
			Caption2 = "Ekran główny";
		}

        public ICommand ShowPatientListViewCommand { get; }

		private void ExecuteShowPatientListViewCommand(object obj) {
			
			if (!_currentUser.FirstLogin || !_firstLogin) //Jeśli użytkownik nie loguje się pierwszy raz -> zmienił hasło -> daj dostęp do przycisków
			{			
				Console.WriteLine("nazwisko:"+CurrentUser.Surname);
				//Ustawiamy adres osadzonej przeglądarki wyświetlającej recepty, tak żeby nie był nullem
				if (CurrentViewModel is PrescriptionsViewModel viewModel)
				{

				}
				//Ustawiamy viewmodel dla widoku listy użytkowników
				CurrentViewModel = new PatientListViewModel(_currentUser,this);
				Caption2 = "Lista pacjentów";
				Console.WriteLine("dasdas");
			}
			
		}

		private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
			throw new NotImplementedException();
		}

		public ICommand ShowReferralViewCommand { get; }
		private void ExecuteShowReferralViewCommand(object obj) {
		if(!_currentUser.FirstLogin || !_firstLogin)
			{
				CurrentViewModel = new ReferralViewViewModel(DbContext.Referrals.Include("Doctor").Include("Patient").Where(r => r.DoctorUserId == _currentUser.Id).ToList(), DbContext.Doctors.Where(doc => doc.UserId == _currentUser.Id).First());
			}
		}

		public ICommand ShowScheduleViewCommand { get; }

        private void ExecuteShowScheduleViewCommand(object obj)
        {

            if (!_currentUser.FirstLogin != !_firstLogin)
            {
                //Ustawiamy viewmodel dla widoku listy użytkowników
                CurrentViewModel = new ScheduleViewModel();
                Caption2 = "Terminarz";
            }
        }


        private User _currentUser;
        private IUserRepository _userRepository;

        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

		public DoctorViewModel() {
			ShowReferralViewCommand = new BasicCommand(ExecuteShowReferralViewCommand);
			ShowPrescriptionViewCommand = new BasicCommand(ExecuteShowPrescriptionViewCommand);
			Klik = new BasicCommand((object obj) => CurrentUser.Surname = "lmao");
			Console.WriteLine("nowy model");
			
			_userRepository = new UserRepository();
			CurrentUser = new User();
			loadCurrentUser();
			ExecuteShowDashboardViewCommand(null);
			ShowDashboardViewCommand = new BasicCommand(ExecuteShowDashboardViewCommand);
			ShowPatientListViewCommand = new BasicCommand(ExecuteShowPatientListViewCommand);
			ShowScheduleViewCommand = new BasicCommand(ExecuteShowScheduleViewCommand);
			ShowDashboardLoggedInCommand = new BasicCommand((object obj) => {

				
				FirstLoginViewModel = (FirstLoginViewModel)CurrentViewModel;
				if (!string.IsNullOrEmpty(FirstLoginViewModel.Password) && !string.IsNullOrEmpty(FirstLoginViewModel.PasswordRepeat))
				{
					if (FirstLoginViewModel.Password.Equals(FirstLoginViewModel.PasswordRepeat))
					{
						CurrentViewModel = new Pages.DashboardViewModel(_currentUser);
						_firstLogin = false;
					}
				}
			} );
		}


        //Znajdź w bazie użytkownika o danych podanych w polu logowania i ustaw jako właściwość CurrentUser
        private void loadCurrentUser()
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
                }
            }
            catch (Exception ex)
            {
                // Obsługa wyjątku - możesz zalogować błąd lub podjąć inne działania
                Console.WriteLine("Błąd podczas ładowania bieżącego użytkownika: " + ex.Message);
            }
        }

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
                Console.WriteLine("model: " + CurrentViewModel.ToString());
            }
        }

        public string Caption2
        {
            get => _caption;

            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption2));
            }
        }

        public string Tag
        {
            get => _tag; set
            {
                _tag = value;
                OnPropertyChanged(nameof(Tag));
            }
        }

		public string ErrorMessage { get => errorMessage; set {
				errorMessage = value;
				OnPropertyChanged(nameof(ErrorMessage));
			}
		}
		public List<Medicine> Medicines { get; set; } = [];
		public Dictionary<Medicine,MedicinePart> MedicineDataGrid { get; set; } = [];

        public FirstLoginViewModel FirstLoginViewModel
        {
            get => _firstLoginViewModel; set
            {
                _firstLoginViewModel = value;
                OnPropertyChanged(nameof(FirstLoginViewModel));
            }
        }
    }
}
