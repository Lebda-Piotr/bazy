using bazy1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bazy1.Models;
using QuestPDF;
using dbm = bazy1.Models;
using System.Windows.Input;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel;
using System.Windows.Data;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace bazy1.ViewModels.Doctor.Pages {
	public class PatientListViewModel : ViewModelBase {
		private User user;
		private Patient _selectedPatient;
		private dbm.Doctor doctor;
		private ObservableCollection<Patient> _patientsList;
		public ICommand ShowMedicalHistoryCommand { get; }
		public ICommand ShowAddDiseaseCommand { get; }
		public ICommand AddPatientCommand { get; set; }
		public ICommand PatientDeleteCommand { get; set; }
		public ICommand ShowAddMedicationCommand { get; set; }

		public string PatientDetails{
			get {
				string adressess = "", info = "";
				if (SelectedPatient != null)
				{
					Console.WriteLine(DbContext.Addresses.Count());
					var tempPatient = DbContext.Patients.Where(pat => pat.Id == SelectedPatient.Id).First();
					if (tempPatient.SecondName != null) info += "Drugie imię: " + tempPatient.SecondName + "\n";
					Console.WriteLine("ile: " + DbContext.Addresses.Where(adr => adr.Patients.Contains(tempPatient)).Count());
					DbContext.Addresses.Where(adr => adr.Patients.Contains(tempPatient)).ToList().
						ForEach(adr => adressess += adr.City + " " + adr.PostalCode + " ul." + adr.Street + " " + adr.BuildingNumber + "\n");
					info = $"Data urodzenia: {tempPatient.BirthDate.Value.ToShortDateString()}\n";
					if (tempPatient.PhoneNumber != null) info += "Telefon: " + tempPatient.PhoneNumber + "\n";
					if (tempPatient.Email != null) info += "Email: " + tempPatient.Email + "\n";
					info += "Adresy:" + adressess;
					info += "Przyjmowane leki:\n";
					string tempDoses = "";
					var names = DbContext.Database.SqlQueryRaw<string>("select med.name from patient_diesease pd join prescription pr on pr.patient_id=pd.patient_id" +
				" join prescription_medicine pm on pm.prescription_id = pr.id" +
				$" join medicine med on med.id = pm.medicine_id where pd.patient_id={SelectedPatient.Id}").ToList();

					var dosages = DbContext.Database.SqlQueryRaw<string>("select med.dose from patient_diesease pd join prescription pr on pr.patient_id=pd.patient_id" +
" join prescription_medicine pm on pm.prescription_id = pr.id" +
$" join medicine med on med.id = pm.medicine_id where pd.patient_id={SelectedPatient.Id} ").ToList();
					string tempMedicines = "";

					for (int i = 0; i < names.Count; i++)
					{
						tempMedicines += $"{names[i]}: {dosages[i]}\n";
					}
					info += tempMedicines;


				}
				return info;
			}
			set {
			}
		}
		private ICollectionView patientsView;
		private string _filterText;

		public string FilterText {
			get => _filterText;
			set {
				_filterText = value;

				//Wyszukiwanie po nazwie
				PatientView.Filter += (object patient) =>
				{
					var tempPatient = patient as Patient;
					return tempPatient.Name.ToLower().Contains(FilterText.ToLower().Trim()) ||
					tempPatient.Surname.ToLower().Contains(FilterText.ToLower().Trim()) ||
					tempPatient.Pesel.ToString().ToLower().Contains(FilterText.ToLower().Trim());

				};
				OnPropertyChanged(nameof(FilterText));

			}
		}

		public ICollectionView PatientView { get => patientsView; set => patientsView = value; }


		public PatientListViewModel(User user, DoctorViewModel viewModel) {

			ShowAddMedicationCommand = new BasicCommand(obj => {
				viewModel.CurrentViewModel = new AddMedicationViewModel(SelectedPatient,null,viewModel);
			});
			//PrescriptionGenerator p = new();
			//p.generate();

			AddPatientCommand = new BasicCommand(obj => viewModel.CurrentViewModel = new AddPatientViewModel(doctor,viewModel));
			PatientDeleteCommand = new BasicCommand(obj =>
			{
				DbContext.Database.ExecuteSql($"Delete from patient_diesease where patient_id = {SelectedPatient.Id}");
				DbContext.Database.ExecuteSql($"Delete from patient_address where patient_id = {SelectedPatient.Id}");
				DbContext.Database.ExecuteSql($"Delete from doctor_has_patient where patient_id = {SelectedPatient.Id}");
				DbContext.SaveChanges();
				viewModel.CurrentViewModel = new PatientListViewModel(user, viewModel);
			});

			Console.WriteLine(user.Name + user.Surname+user.Id);
			this.user = user;
			doctor = DbContext.Doctors.Where(doctor => doctor.UserId == user.Id).First();
			//Console.WriteLine("dok: " + .Count());
			_patientsList = new(DbContext.Patients.Where(patient => patient.Doctors.Contains(doctor)).ToList());
			PatientView = CollectionViewSource.GetDefaultView(_patientsList);
			Console.WriteLine("cn:"+_patientsList.Count());
			Console.WriteLine(doctor.Offices.Count());
			Console.WriteLine($"id:{doctor.UserId}, {doctor.Id}");
			//doctor.Patients.Add(new Patient() { Name = "Pacjent #1"});
			//DbContext.SaveChanges();
			ShowMedicalHistoryCommand = new BasicCommand((object obj) => {
				Console.WriteLine(SelectedPatient.Name + SelectedPatient.Surname);
				Console.WriteLine("sel: " + DbContext.Patients.Where(pat => pat.Id == SelectedPatient.Id).First().Diseases.Count);
				if (SelectedPatient != null) viewModel.CurrentViewModel = new MedicalHistoryViewModel(DbContext.Patients.Where(pat => pat.Id == SelectedPatient.Id).First(),null,viewModel);
				});
			ShowAddDiseaseCommand = new BasicCommand((object obj) =>
			{
				if (SelectedPatient != null) viewModel.CurrentViewModel = new AddDiseaseViewModel(DbContext.Patients.Where(pat => pat.Id == SelectedPatient.Id).First()); 
			});

			Console.WriteLine(doctor.Name);


			//_patientsList = new(doctor.Patients);
			Console.WriteLine(_patientsList.Count());
		}

		public ObservableCollection<Patient> PatientsList {
			get => _patientsList; 
			set{
				_patientsList = value;
				OnPropertyChanged(nameof(PatientsList));
			}
		}

		public Patient SelectedPatient {
			get => _selectedPatient;
			set{
			 _selectedPatient = value;
				OnPropertyChanged(nameof(SelectedPatient));
			}
		}
	}
}
