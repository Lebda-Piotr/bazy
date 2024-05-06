using bazy1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace bazy1.ViewModels.Doctor.Pages {
	public class PrescriptionsViewModel : ViewModelBase {
		private string _date, _patientName,_filterText;
		private Prescription _selectedPrescription;
		private PrescriptionGenerator generator = new();
		private Models.Doctor doctor;

		private ICollectionView _prescriptionsView;

		public string FilterText {
			get => _filterText;
			set {
				_filterText = value;

				//Wyszukiwanie po nazwie
				PrescriptionsView.Filter += (object prescription) =>
				{
					var tempPrescription = prescription as Prescription;
					return tempPrescription.DateOfPrescription.Value.ToLongDateString().ToLower().Contains(FilterText.ToLower().Trim())
					|| tempPrescription.Patient.Name.ToLower().Contains(FilterText.ToLower().Trim()) ||
					tempPrescription.Patient.Surname.ToLower().Contains(FilterText.ToLower().Trim());
				};
				OnPropertyChanged(nameof(FilterText));

			}
		}
		public Prescription SelectedPrescription {
			get => _selectedPrescription;
			set {
				_selectedPrescription = value;
                Console.WriteLine("wymm: ");
                OnPropertyChanged(nameof(SelectedPrescription));
				//var filename = generator.generate(DbContext.Prescriptions.Include("Medicines").Include("Patient").Include("Patient.Addresses").Where(pr => pr.Id == SelectedPrescription.Id).First(), doctor);
				Console.WriteLine("pdf: "+SelectedPrescription.Pdf);
            }
		}

		public ICollectionView PrescriptionsView{ get; set; }

		public PrescriptionsViewModel(List<Prescription> prescriptions, Models.Doctor doctor) {
			this.doctor = doctor;
            foreach (var item in prescriptions)
            {
                if(item.Patient.Name != null) Console.WriteLine(item.Patient.Name);
            }
           // Console.WriteLine(prescriptions[0].Patient.Name);
            PrescriptionsView = CollectionViewSource.GetDefaultView(prescriptions);


            
        }
	}
}
