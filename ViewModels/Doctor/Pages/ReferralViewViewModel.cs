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
	public class ReferralViewViewModel : ViewModelBase {
		private string _date, _patientName, _filterText;
		private Referral _selectedReferral;
		private Models.Doctor doctor;
		private static string _pdfPath;
		private ICollectionView _referralsView;


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
				ReferralsView.Filter += (object referral) =>
				{
					var tempReferral = referral as Referral;
					return tempReferral.Date.Value.ToLongDateString().ToLower().Contains(FilterText.ToLower().Trim())
					|| tempReferral.Patient.Name.ToLower().Contains(FilterText.ToLower().Trim()) ||
					tempReferral.Patient.Surname.ToLower().Contains(FilterText.ToLower().Trim()) ||
					tempReferral.MedicalEntity.ToLower().Contains(FilterText.ToLower().Trim());
				};
				OnPropertyChanged(nameof(FilterText));

			}
		}
		public Referral SelectedReferral {
			get => _selectedReferral;
			set {
				_selectedReferral = value;
				PdfPath = SelectedReferral.Pdf;
				//_pdfPath = _selectedPrescription != null ? SelectedPrescription.Pdf : "";
				Console.WriteLine("wymm:" + PdfPath);
				OnPropertyChanged(nameof(SelectedReferral));
				//var filename = generator.generate(DbContext.Prescriptions.Include("Medicines").Include("Patient").Include("Patient.Addresses").Where(pr => pr.Id == SelectedPrescription.Id).First(), doctor);

			}
		}

		public ICollectionView ReferralsView { get; set; }


		public ReferralViewViewModel(List<Referral> referrals, Models.Doctor doctor) {
			this.doctor = doctor;
			ReferralsView = CollectionViewSource.GetDefaultView(referrals);
		}
	}
}
