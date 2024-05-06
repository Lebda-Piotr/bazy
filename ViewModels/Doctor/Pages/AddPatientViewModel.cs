using bazy1.Models;
using bazy1.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using dbm = bazy1.Models;

namespace bazy1.ViewModels.Doctor.Pages {
	public class AddPatientViewModel : ViewModelBase, IDataErrorInfo {
		private DoctorViewModel parentViewModel;
		private string _name, _surname, _pesel, _secondName, _sex="M", _email, _phone;
		private string _city, _postalCode, _street, _buildingNumber;
		private string _city2, _postalCode2, _street2, _buildingNumber2;

		private List<string> _sexes = ["K", "M"];
		private DateTime _birthDate = DateTime.Now;
		private dbm.Doctor doctor;
		private bool _sameAddress = true;
		private Visibility _secondAddressVisible = Visibility.Hidden;

		//Używane żeby pola nie były walidowane od razu po uruchomieniu widoku
		public Dictionary<string, bool> needToValidate = [];
			

		public ICommand ShowPatientListCommand { get; set; }
		public ICommand AddNewPatientCommand { get; set; }
		public string Name {
			get => _name;
			set {
				needToValidate["Name"] = true;
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public string Surname {
			get => _surname;
			set {
				needToValidate["Surname"] = true;
				_surname = value;
				OnPropertyChanged(nameof(Surname)); 
			}
		}
		public string Pesel {
			get => _pesel;
			set {
				needToValidate["Pesel"] = true;

				_pesel = value;
				OnPropertyChanged(nameof(Pesel));
			}
		}
		public string SecondName {
			get => _secondName;
			set {
				_secondName = value;
				OnPropertyChanged(nameof(SecondName));
			}
		}
		
		public string Sex {
			get => _sex;
			set {
				needToValidate["Sex"] = true;
				_sex = value;
				OnPropertyChanged(nameof(Sex));
			}
		}
		public List<string> Sexes { get => _sexes;
			set {
				_sexes = value;
				OnPropertyChanged(nameof(Sexes)); 
			}
		}
		public DateTime BirthDate {
			get => _birthDate;
			set {
				needToValidate["BirthDate"] = true;
				_birthDate = value;
				OnPropertyChanged(nameof(BirthDate));
			}
		}
		public string Email {
			get => _email;
			set {
				needToValidate["Email"] = true;
				_email = value;
				OnPropertyChanged(nameof(Email));
			}
		}
		public string PhoneNumber{
			get => _phone;
			set {
				needToValidate["PhoneNumber"] = true;
				_phone = value;
				OnPropertyChanged(nameof(PhoneNumber));
			}
		}
		//adresy
		public string City {
			get => _city;
			set {
				needToValidate["City"] = true;
				_city = value;
				OnPropertyChanged(nameof(City));
			}
		}
		public string Street {
			get => _street;
			set {
				needToValidate["Street"] = true;
				_street = value;
				OnPropertyChanged(nameof(Street));
			}
		}
		public string PostalCode {
			get => _postalCode;
			set {
				needToValidate["PostalCode"] = true;
				_postalCode = value;
				OnPropertyChanged(nameof(PostalCode));
			}
		}
		public string BuildingNumber {
			get => _buildingNumber;
			set {
				needToValidate["BuildingNumber"] = true;
				_buildingNumber = value;
				OnPropertyChanged(nameof(BuildingNumber));
			}
		}

		public string City2 {
			get => _city2;
			set {
				needToValidate["City2"] = true;
				_city2 = value;
				OnPropertyChanged(nameof(City2));
			}
		}
		public string Street2 {
			get => _street2;
			set {
				needToValidate["Street2"] = true;
				_street2 = value;
				OnPropertyChanged(nameof(Street2));
			}
		}
		public string PostalCode2 {
			get => _postalCode2;
			set {
				needToValidate["PostalCode2"] = true;
				_postalCode2 = value;
				OnPropertyChanged(nameof(PostalCode2));
			}
		}
		public string BuildingNumber2 {
			get => _buildingNumber2;
			set {
				needToValidate["BuildingNumber2"] = true;
				_buildingNumber2 = value;
				OnPropertyChanged(nameof(BuildingNumber2));
			}
		}
		public bool SameAddress {
			get => _sameAddress;
			set {
				_sameAddress= value;
                Console.WriteLine(SameAddress);
                if (_sameAddress) SecondAddressVisible = Visibility.Hidden;
				else SecondAddressVisible = Visibility.Visible;
				OnPropertyChanged(nameof(SameAddress));
			}
		}
		public Visibility SecondAddressVisible {
			get => _secondAddressVisible;
			set {
				_secondAddressVisible = value;
				OnPropertyChanged(nameof(SecondAddressVisible));
			}
		}


		//Walidacja pól
		public Dictionary<string, string> ErrorCollection { get; set; } = [];

		public string Error => null;

		public string this[string fieldName] {
			get {
				string emptyFieldMsg = "To pole nie może być puste";
				string result = null;
				if (fieldName == "Name" && needToValidate[fieldName])
				{
					if (!validate(Name)) result = emptyFieldMsg;
					else if (ErrorCollection.ContainsKey(fieldName))
						ErrorCollection.Remove(fieldName);
				}
				if(fieldName == "Surname" && needToValidate[fieldName])
				{
					if (!validate(Surname)) result = emptyFieldMsg;
					else if (ErrorCollection.ContainsKey(fieldName))
						ErrorCollection.Remove(fieldName);
				}
				if(fieldName == "BirthDate" && needToValidate[fieldName])
				{
                    if (BirthDate == null || BirthDate.Year < 1800 || BirthDate.Date.Year > DateTime.Now.Year || BirthDate.DayOfYear > DateTime.Now.DayOfYear)
                    result = "Niepoprawna data";
					else if (ErrorCollection.ContainsKey(fieldName))
						ErrorCollection.Remove(fieldName);
				}
				if(fieldName == "Pesel" && needToValidate[fieldName])
				{
					if (!PeselValidator.isValid(Pesel)) result = "Niepoprawny PESEL";
					else if (ErrorCollection.ContainsKey(fieldName))
						ErrorCollection.Remove(fieldName);
				}
				if (fieldName == "Sex" && needToValidate[fieldName])
                {
					if (!validateList(Sex, Sexes)) result = "Nie wybrano płci";
					else if (ErrorCollection.ContainsKey(fieldName))
						ErrorCollection.Remove(fieldName);
				}
				if(fieldName == "Email" && needToValidate[fieldName])
				{
					if(!string.IsNullOrEmpty(Email) && !string.IsNullOrWhiteSpace(Email))
					{
						MailAddress mail;
						if (!MailAddress.TryCreate(Email, out mail)) result = "Niepoprawny format";
						else if (ErrorCollection.ContainsKey(fieldName)) 
						ErrorCollection.Remove(fieldName); ;
					}


				}
				if (fieldName == "PhoneNumber" && needToValidate[fieldName])
				{
					if (!string.IsNullOrEmpty(PhoneNumber) && !string.IsNullOrWhiteSpace(PhoneNumber))
					{
						if (!Regex.Match(PhoneNumber, "^[0-9\\-\\+]{9,15}$").Success)
							result = "Niepoprawny format";
						else if (ErrorCollection.ContainsKey(fieldName))
						ErrorCollection.Remove(fieldName);
					}

				}
				if (fieldName == "City" && needToValidate[fieldName])
				{
					if (!validate(City)) result = emptyFieldMsg;
					else if (ErrorCollection.ContainsKey(fieldName))
						ErrorCollection.Remove(fieldName);
				}
				if (fieldName == "City2" && needToValidate[fieldName])
				{
						if (!validate(City2) && !SameAddress) result = emptyFieldMsg;
						else if (ErrorCollection.ContainsKey(fieldName))
						ErrorCollection.Remove(fieldName);
					

				}
				if (fieldName == "Street" && needToValidate[fieldName])
				{
					if (!validate(Street)) result = emptyFieldMsg;
					else if (ErrorCollection.ContainsKey(fieldName))
						ErrorCollection.Remove(fieldName);
				}
				if (fieldName == "Street2" && needToValidate[fieldName])
				{
						if (!validate(Street2) && !SameAddress) result = emptyFieldMsg;
						else if (ErrorCollection.ContainsKey(fieldName))
						ErrorCollection.Remove(fieldName);
					
				}
				if (fieldName == "BuildingNumber" && needToValidate[fieldName])
				{
					if (string.IsNullOrEmpty(BuildingNumber) || string.IsNullOrWhiteSpace(BuildingNumber)) result = emptyFieldMsg;
					else if (ErrorCollection.ContainsKey(fieldName))
						ErrorCollection.Remove(fieldName);
				}
				if (fieldName == "BuildingNumber2" && needToValidate[fieldName])
				{
					if (string.IsNullOrEmpty(BuildingNumber) || string.IsNullOrWhiteSpace(BuildingNumber) && !SameAddress) result = emptyFieldMsg;
						else if (ErrorCollection.ContainsKey(fieldName))
						ErrorCollection.Remove(fieldName);
					

				}
				if (fieldName == "PostalCode" && needToValidate[fieldName])
				{
					
					if (string.IsNullOrEmpty(PostalCode) || string.IsNullOrWhiteSpace(PostalCode) || !Regex.Match(PostalCode, "^[0-9]{2}-[0-9]{3}$").Success)
						result = "Niepoprawny format";
					else if (ErrorCollection.ContainsKey(fieldName))
						ErrorCollection.Remove(fieldName);
				}
				if (fieldName == "PostalCode2" && needToValidate[fieldName])
				{
						if ( (string.IsNullOrEmpty(PostalCode2) || string.IsNullOrWhiteSpace(PostalCode2) || (!Regex.Match(PostalCode2, "^[0-9]{2}-[0-9]{3}$").Success)) && !SameAddress) result = "Niepoprawny format";
					else if (ErrorCollection.ContainsKey(fieldName))
						ErrorCollection.Remove(fieldName);
					
					
				}

				if (ErrorCollection.ContainsKey(fieldName))ErrorCollection[fieldName] = result;
				else if (result != null) ErrorCollection.Add(fieldName, result);
				OnPropertyChanged(nameof(ErrorCollection));

				return result;
			}

		}

		private bool validate(string data) {
			return !string.IsNullOrEmpty(data) && !string.IsNullOrWhiteSpace(data) && !data.Any(char.IsDigit);
		}
		private bool validateList(string data,List<string> list) {
			return !string.IsNullOrEmpty(data) && !string.IsNullOrWhiteSpace(data)
				&& list.Contains(data);
		}
		public AddPatientViewModel(dbm.Doctor doctor, DoctorViewModel parentViewModel) {

			//Walidacja wyłączona po załadowaniu widoku
			foreach (var field in GetType().GetProperties().
				Where(prop => prop.PropertyType.Name == "String" || prop.PropertyType.Name == "DateTime"))
				needToValidate.Add(field.Name, false);


			
			this.doctor = doctor;
			ShowPatientListCommand = new BasicCommand(obj => parentViewModel.ShowPatientListViewCommand.Execute(obj)); ;
			AddNewPatientCommand = new BasicCommand((object obj) =>
			{
                foreach (var err in ErrorCollection)
                {
					Console.WriteLine("słownik: "+err.Key + err.Value);
                }

				//Zrobione po to żeby odświeżyć wartości i tym samym uruchomić walidację po kliknięciu przycisku
				Name = Name;
				Surname = Surname;
				Pesel = Pesel;
				Email = Email;
				PhoneNumber = PhoneNumber;
				SecondName = SecondName;
				Street2 = Street2;
				Street = Street;
				City = City;
				City2 = City2;
				PostalCode = PostalCode;
				PostalCode2 = PostalCode2;
				BuildingNumber = BuildingNumber;
				BuildingNumber2 = BuildingNumber2;

                foreach (var prop in needToValidate)
                {
					needToValidate[prop.Key] = true;
                }
				
                var doc = DbContext.Doctors.Where(doc => this.doctor.Id == doc.Id).First();
                Console.WriteLine(ErrorCollection.Count);
                if (ErrorCollection.Count == 0)
				{
					Console.WriteLine("heh ok");
					Patient patient = new Patient();

					if (validate(Email))
					{
						MailAddress tempMail;
						if (MailAddress.TryCreate(Email, out tempMail)) patient.Email = Email;
					}
					patient.Name = Name;
					patient.Surname = Surname;
					patient.SecondName = SecondName;
					patient.BirthDate = DateTime.Parse(BirthDate.ToString());
					patient.Sex = Sex;
					patient.PhoneNumber = PhoneNumber;
					patient.Addresses.Add(new Address() { City = this.City, PostalCode = this.PostalCode, Street = this.Street, BuildingNumber = this.BuildingNumber, Type="Zamieszkania" });
					if (SecondAddressVisible == Visibility.Visible) patient.Addresses.Add(new Address() { City = this.City2, PostalCode = this.PostalCode2, Street = this.Street2, BuildingNumber = this.BuildingNumber2, Type="Zameldowania" });
					patient.Pesel = Pesel;
					doctor.Patients.Add(patient);
					DbContext.SaveChanges();
				}
				

				
				//doc.Patients.Add(patient);
			});
		}
	}
}
