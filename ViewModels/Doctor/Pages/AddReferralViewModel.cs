using bazy1.Models;
using bazy1.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static bazy1.ViewModels.Doctor.Pages.AddMedicationViewModel;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.VisualBasic.FileIO;

namespace bazy1.ViewModels.Doctor.Pages {
	public class AddReferralViewModel : ViewModelBase, IDataErrorInfo{
		private string _info, _medicalEntity, _disease;
		private Patient _patient;
		public ICommand AddReferralCommand { get; set; }
		public List<string> MedicalEntities { get; set; } = [];


		public Dictionary<string, bool> needToValidate = [];
		public Dictionary<string, string> ErrorCollection { get; set; } = [];
		private bool validate(string data) {
			return !string.IsNullOrEmpty(data) && !string.IsNullOrWhiteSpace(data);
		}

		public string Error => null;
		public string this[string fieldName] {
			get {
				string emptyFieldMsg = "To pole nie może być puste";
				string result = null;
				if (fieldName == "Disease" && needToValidate[fieldName])
				{
					if (!validate(Disease)) result = emptyFieldMsg;
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


		public Patient Patient {
			get => _patient;
			set {
				_patient = value;
				OnPropertyChanged(nameof(Patient));
			}
		}

		public string Information {
			get => _info;
			set {
				_info = value;
				OnPropertyChanged(nameof(Information));
			}
		}

		public string MedicalEntity {
			get => _medicalEntity;
			set {
				_medicalEntity = value;
				OnPropertyChanged(nameof(MedicalEntity));
			}
		}

		public string Disease {
			get => _disease;
			set {
				needToValidate["Disease"] = true;
				_disease = value;
				OnPropertyChanged(nameof(Disease));
			}
		}

		private async void loadCSV(bool download) {
			if (download)
			{
				var httpClient = new HttpClientDownloadWithProgress("https://rpwdl.ezdrowie.gov.pl/Registry/Pobieranie?typ=Csv&rodzajRejestru=Rpm", "rpm.zip");

				httpClient.ProgressChanged += HttpClient_ProgressChanged;
				await httpClient.StartDownload();

				void HttpClient_ProgressChanged(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage) {
					Console.WriteLine(progressPercentage);

				}
			}
			ZipArchive archive = new ZipArchive(new FileStream("rpm.zip",FileMode.Open));
			archive.ExtractToDirectory("rpm", true);
            using (TextFieldParser csvParser = new TextFieldParser("rpm\\podmioty.csv"))
			{
				csvParser.CommentTokens = ["#"];
				csvParser.SetDelimiters([";"]);
				csvParser.HasFieldsEnclosedInQuotes = true;

				csvParser.ReadLine();

				while (!csvParser.EndOfData)
				{
					string[] fields = csvParser.ReadFields();
					string Name = fields[12];
					MedicalEntities.Add(Name);
                }
			}
		}


		public AddReferralViewModel(Models.Doctor doctor, Patient patient) {

			loadCSV(true);

			foreach (var field in GetType().GetProperties().
	Where(prop => prop.PropertyType.Name == "String" || prop.PropertyType.Name == "DateTime"))
				needToValidate.Add(field.Name, false);

			Patient = patient;
			Console.WriteLine(patient.Name);
            Console.WriteLine(doctor.Name);
            AddReferralCommand = new BasicCommand(obj => {

				Disease = Disease;
				foreach (var prop in needToValidate)
				{
					needToValidate[prop.Key] = true;
				}

				if (ErrorCollection.Count == 0)
				{
					DbContext.Database.ExecuteSql(
						$"insert into referral(code,date,disease, information, medical_entity, patient_id,doctor_id, doctor_user_id) values( (select left(replace(uuid(),'-',''),20)),date(now()),{Disease},{Information},{MedicalEntity},{Patient.Id},{doctor.Id},{doctor.UserId})");
					DbContext.SaveChanges();
					Console.WriteLine(DbContext.Referrals.ToList().Last().Code);
					ReferralGenerator.generate(DbContext.Referrals.ToList().Last());
				}
			});
			

		
		}
	}
}	
