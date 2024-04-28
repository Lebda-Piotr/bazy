﻿using bazy1.Models;
using bazy1.Utils;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Resultset;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace bazy1.ViewModels.Doctor.Pages
{
    public class AddMedicationViewModel : ViewModelBase, IDataErrorInfo
    {

        private string _amount, _dose, _name, _comments;
		private DateTime _date = DateTime.Now.Date;
        public Patient SelectedPatient { get; set; }
        public ICommand AddMedicineCommand { get; set; }
		private DoctorViewModel parentViewModel;
		private bool newMedicineAdded = false;
		public ICommand AddPrescriptionCommand { get; set; }
		public ICommand DeleteMedicationCommand { get; set; }
		public Medicine SelectedMedicine { get;set; }
		public ObservableCollection<Medicine> Medicines {
            get => medicines;
            set {
                medicines = value;
                OnPropertyChanged(nameof(Medicines));
            }
        }
		public Disease disease;

		public string Amount {
            get => _amount;
            set {
					_amount = value;
					needToValidate["Amount"] = true;
				OnPropertyChanged(nameof(Amount));
            }
        }
		public string Dose {
			get => _dose;
            set {
				_dose = value;
				
					needToValidate["Dose"] = true;
				
				OnPropertyChanged(nameof(Dose));
			}
		}

		public string Name {
			get => _name;
            set {				
				needToValidate["Name"] = true;				
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public string Comments {
			get => _comments;
            set {
				_comments = value;
				OnPropertyChanged(nameof(Comments));
			}
		}
		public DateTime Date {
			get => _date;
			set {
                Console.WriteLine("data:"+Date);
                needToValidate["Date"] = true;
				_date = value;
				OnPropertyChanged(nameof(Date));
			}
		}

		private ObservableCollection<Medicine> medicines = [];

		//walidacja
		private bool validate(string data) {
			return !string.IsNullOrEmpty(data) && !string.IsNullOrWhiteSpace(data);
		}
		//Używane żeby pola nie były walidowane od razu po uruchomieniu widoku
		public Dictionary<string, bool> needToValidate = [];
		public Dictionary<string, string> ErrorCollection { get; set; } = [];

		public string Error => null;
		public string this[string fieldName] {
			get {
				string emptyFieldMsg = "To pole nie może być puste";
				string result = null;
				if (fieldName == "Name" && needToValidate[fieldName])
				{
                    Console.WriteLine("wal");
                    if (!validate(Name)) result = emptyFieldMsg;
					else if (ErrorCollection.ContainsKey(fieldName))
						ErrorCollection.Remove(fieldName);
				}
				if (fieldName == "Dose" && needToValidate[fieldName])
				{
					if (!validate(Dose)) result = emptyFieldMsg;
					else if (ErrorCollection.ContainsKey(fieldName))
						ErrorCollection.Remove(fieldName);
				}
				if (fieldName == "Amount" && needToValidate[fieldName])
				{
                    if (!validate(Amount) || !float.TryParse(Amount, out float tempDose)) result = "Niepoprawny format";
					else if (ErrorCollection.ContainsKey(fieldName))
						ErrorCollection.Remove(fieldName);
				}
				if (fieldName == "Date" && needToValidate[fieldName])
				{
					if (Date == null|| Date.Date < DateTime.Now.Date) result = "Niepoprawna data";
					else if (ErrorCollection.ContainsKey(fieldName))
						ErrorCollection.Remove(fieldName);
				}


				if (ErrorCollection.ContainsKey(fieldName)) ErrorCollection[fieldName] = result;
				else if (result != null) ErrorCollection.Add(fieldName, result);
				OnPropertyChanged(nameof(ErrorCollection));

                Console.WriteLine(result);
                return result;
			}

		}

		public AddMedicationViewModel(Patient patient, Disease disease, DoctorViewModel parentViewModel) {
			this.parentViewModel = parentViewModel;
			Medicines = new(parentViewModel.Medicines);
			this.disease = disease;
			parentViewModel.Medicines = [];


			SelectedPatient = patient;
			//DbContext.Patients.Where(pat => pat.Id == SelectedPatient.Id).First().Prescriptions;
			//Walidacja wyłączona po załadowaniu widoku
			foreach (var field in GetType().GetProperties().
				Where(prop => prop.PropertyType.Name == "String" || prop.PropertyType.Name == "DateTime"))
				needToValidate.Add(field.Name, false);

			AddPrescriptionCommand = new BasicCommand(obj =>
			{
				if (Medicines.Count != 0)
				{
					//SelectedPatient.Diseases.Where(d => d.Id == disease.Id).First().Medicines.Add(Medicines.tol);
					SelectedPatient.Prescriptions.Add(new Prescription { Medicines = this.Medicines, DateOfPrescription = Date });
					Console.WriteLine("mesd:" + Medicines.Count());
					DbContext.SaveChanges();

					PrescriptionGenerator generator = new();
					//generator.generate();

                    medicines.Clear();
				}
			});

			DeleteMedicationCommand = new BasicCommand(obj =>
			{
				if(SelectedMedicine!= null) Medicines.Remove(SelectedMedicine);
			});

			AddMedicineCommand = new BasicCommand((obj) =>
            {
                //Zrobione po to żeby odświeżyć wartości i tym samym uruchomić walidację po kliknięciu przycisku

				if (ErrorCollection.Count == 0)
				{
					medicines.Add(new Medicine { Dose = Dose, Amount = int.Parse(Amount), Name = Name, Comments = Comments });
					Prescription prescription = new() { Medicines = medicines };

					foreach (var prop in needToValidate)
					{
						needToValidate[prop.Key] = false;
					}
					foreach(var error in ErrorCollection)
					{
						Console.WriteLine(error.Key + error.Value);
                    }
					ErrorCollection.Clear();
					newMedicineAdded = true;
                    foreach (var item in Medicines)
                    {
						//DbContext.Medicines.Add(item);
                    }
					//DbContext.SaveChanges();
					parentViewModel.Medicines = medicines.ToList();
					parentViewModel.CurrentViewModel = new AddMedicationViewModel(patient,disease,parentViewModel);
				}

				Console.WriteLine("romzd: " + ErrorCollection.Count());

			});
           // Prescription p1 = new() { }
            //"select dm.id from disease_medicine join patient p join diesease d where d. "
			//DbContext.Database.ExecuteSql($"insert into diesease_medicine values({disease.Id},LAST_INSERT_ID())");
           // DbContext.Database.ExecuteSql($"insert into patient_diesease values({disease.Id},{patient.Id})");
            //DbContext.SaveChanges();
            var ans = DbContext.Database.SqlQuery<string>($"select med.name from  ((patient p join patient_diesease pd on p.id=pd.patient_id) join diesease_medicine dm on pd.disease_id = dm.diesease_id) join medicine med on med.id = dm.medicine_id where p.id={patient.Id}");
           // Console.WriteLine("wyn: "ans );
        } 
    }
}
