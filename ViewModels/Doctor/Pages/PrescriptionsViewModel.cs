using bazy1.Models;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace bazy1.ViewModels.Doctor.Pages {
	public class PrescriptionsViewModel : ViewModelBase {
		private Prescription _selectedPrescription;
		private PrescriptionGenerator generator = new();
		private Models.Doctor doctor;
		private ICollectionView _prescriptionsView;


		public string PdfPath {
			get => _pdfPath;
			set {
				if (value != null)
				{
					_pdfPath = value;
					OnPropertyChanged(nameof(PdfPath));
				}
			}
		}
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
			}
			// Console.WriteLine(prescriptions[0].Patient.Name);
			PrescriptionsView = CollectionViewSource.GetDefaultView(prescriptions);


            
        }
	}
}
