using bazy1.Models;
using bazy1.Models.Part;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace bazy1.ViewModels.Doctor.Pages {
	public class MedicalHistoryViewModel : ViewModelBase {
		private ObservableCollection<Disease> diesasesList;
		public Disease _selectedDisease;
		private ObservableCollection<DiseasePart> diesasesPartList = [];

		private Patient patient;

		public ICommand ShowAddMedicationCommand { get; set; }

		private List<Medicine> _medicines;
		private string _medicineDetails;
		private string? _filterText;
		//public ICommand ShowAddMedicationCommand { get; set; }


		public MedicalHistoryViewModel(Patient patient, Disease disease, DoctorViewModel viewModel) {
			this.patient = patient;

			ShowAddMedicationCommand = new BasicCommand(obj => {
				viewModel.CurrentViewModel = new AddMedicationViewModel(patient,disease, viewModel);
			});
			string nazwa = "Witamina K";
			//DbContext.Database.ExecuteSql($"insert into medicine(name) values({nazwa})");
			//"select dm.id from disease_medicine join patient p join diesease d where d. "
			//DbContext.Database.ExecuteSql($"insert into diesease_medicine values({disease.Id},LAST_INSERT_ID())");
			//DbContext.Database.ExecuteSql($"insert into patient_diesease values({disease.Id},{patient.Id})");
			DbContext.SaveChanges();
			var ans = DbContext.Database.SqlQuery<string>($"select med.name from  ((patient p join patient_diesease pd on p.id=pd.patient_id) join diesease_medicine dm on pd.disease_id = dm.diesease_id) join medicine med on med.id = dm.medicine_id where p.id={patient.Id}");
			ans.ToList().ForEach(System.Console.WriteLine);
			
			Console.Write("pacjent:"+patient.Name);
			_medicines = [];
			if (SelectedDisease != null)
			{

                //_medicines.Add(new Medicine {Name = res[] })
            }
			Console.WriteLine("medcs: " + _medicines.Count());
			//DbContext.Patients.ForEachAsync(pat => Console.WriteLine("wsz: "+pat.Diseases.Count));
			//diesasesList = new(DbContext.Patients.Where(patient => patient.Id == this.patient.Id).First().Diseases);
			diesasesList = new(DbContext.Diseases.Where(dis => dis.Patients.Contains(patient)));

            foreach (var d in diesasesList)
            {
                Console.WriteLine(d.Medicines.Count);
            }

            //Console.WriteLine("chr: "+diesasesList.Count());
            DiseasesView = CollectionViewSource.GetDefaultView(diesasesList);
		}

		public ObservableCollection<Disease> DiseasesList {
			get => diesasesList;
			set {
				diesasesList = value;
				OnPropertyChanged(nameof(DiseasesList));
			}
		}

		public Disease SelectedDisease {
			get => _selectedDisease;
			set {
                _selectedDisease = value;
				//res.ForEach(Console.WriteLine);
				//res2.ForEach(Console.WriteLine);
				OnPropertyChanged(nameof(SelectedDisease));
				foreach(var disease in DiseasesList)
				{
					//disease.Medicines.Add()
				}
                foreach (var item in patient.Diseases)
                {
                    Console.WriteLine("imle: "+item.Medicines.Count);
                }
            }
		}

		private ICollectionView diseasesView;

		public string FilterText { 
			get => _filterText;
			set{
				_filterText = value;

				//Wyszukiwanie po nazwie
				DiseasesView.Filter += (object disease) =>
				{
					var tempDisease = disease as Disease;
					return tempDisease.Name.ToLower().Contains(FilterText.ToLower().Trim());
				};
				OnPropertyChanged(nameof(FilterText));
				
			} 
		}

		public ICollectionView DiseasesView { get => diseasesView; set => diseasesView = value; }
		public string Medicines {
			get {
				string tempMedicines = "";
                for(int i=0;i<_medicines.Count;i++)
                {
					tempMedicines += $"{_medicines[i].Name}{(i!= _medicines.Count -1 ? "," : "")}";
                }
				return tempMedicines;
            }
		}

		public string MedicineDetails {
			get{
				var names = DbContext.Database.SqlQueryRaw<string>("select med.name from patient_diesease pd join prescription pr on pr.patient_id=pd.patient_id" +
" join prescription_medicine pm on pm.prescription_id = pr.id" +
$" join medicine med on med.id = pm.medicine_id where pd.disease_id={SelectedDisease.Id}").ToList();
				string tempMedicines = "";
				for (int i = 0; i < names.Count; i++)
				{
					var dosages = DbContext.Database.SqlQueryRaw<string>("select med.dose from patient_diesease pd join prescription pr on pr.patient_id=pd.patient_id" +
		" join prescription_medicine pm on pm.prescription_id = pr.id" +
		$" join medicine med on med.id = pm.medicine_id where pd.disease_id={SelectedDisease.Id}").ToList();
					tempMedicines += $"{names[i]}";
				}
				return tempMedicines;

			}
			set => _medicineDetails = value; 
		}
	}
}
