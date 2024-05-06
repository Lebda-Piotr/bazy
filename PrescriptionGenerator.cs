using System;
using System.IO;
using bazy1.Models;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace bazy1 {
	public class PrescriptionGenerator {
		public string generate(Prescription prescription, Doctor doctor) {
            Console.WriteLine("genruje sie;");
            //ViewModels.ViewModelBase.DbContext.Doctors.Where(doc => doc.Patients.Contains(prescription.Patient)).Where(doc => )

            QuestPDF.Settings.License = LicenseType.Community;
			string fileTitle = Directory.GetCurrentDirectory() + "\\" + 
				prescription.Id + 
				DateTime.Now.Day.ToString() +
				DateTime.Now.Month + 
				DateTime.Now.Year.ToString() +
				".pdf";

			Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Size(PageSizes.EnvDL);

					page.Content().Table(table =>
					{
						table.ExtendLastCellsToTableBottom();
						table.ColumnsDefinition(columns =>
						{
							columns.RelativeColumn(2);
							columns.RelativeColumn(1);
						});

						table.Cell().Row(1).ColumnSpan(2).Border(1F).Text("Recepta").AlignLeft();
						table.Cell().Row(1).ColumnSpan(2).Text("\nSpecjalistyczna przychodnia lekarska\n25-155 Warszawa, ul. Kwiatowa 22," +
							"tel.:(42)655-47-64	\nfdj").AlignCenter();
						table.Cell().Row(1).ColumnSpan(2).Border(1F).Text("\n\n\n\nŚwiadczeniodawca").AlignLeft();


						table.Cell().Row(2).ColumnSpan(2).BorderLeft(1F).Text("Pacjent\n" +
							prescription.Patient.Name + " " + prescription.Patient.Surname + "\n" + prescription.Patient.Addresses.ElementAt(0).City +
							 prescription.Patient.Addresses.ElementAt(0).Street +" " +prescription.Patient.Addresses.ElementAt(0).BuildingNumber).AlignLeft();
						table.Cell().Row(3).ColumnSpan(2).BorderBottom(1F).BorderLeft(1F).Text("\n\n\n\nPESEL: " + prescription.Patient.Pesel).AlignLeft();

						table.Cell().Row(2).RowSpan(2).Column(2).Border(1F).Text("Uprawnienia dodatkowe\n").AlignLeft();
						table.Cell().Row(4).Column(1).BorderLeft(1F).Text("Rp");
						table.Cell().Row(4).Column(2).BorderRight(1F).Text("Odpłatność");

						for (int i = 0; i < 12; i++)
						{
							Medicine med = new();
							if (i < prescription.Medicines.Count) med = prescription.Medicines.ElementAt(i);
							table.Cell().Row((uint)i + 5).Column(1).BorderLeft(1F).Text(
								i < prescription.Medicines.Count ?
								(med.Amount > 1 ? med.Amount + "x" : "") + med.Name + "\n" + med.Comments : "⠀\n");
							table.Cell().Column(2).Row((uint)i + 5).BorderRight(1F).Text(i < prescription.Medicines.Count ? med.Fraction * 100 + "%" : "⠀");
						}

						table.Cell().Row(17).Column(1).Border(1F).Text("Data wystawienia\n\n" + prescription.DateOfPrescription.Value.ToShortDateString());
						table.Cell().Row(18).Column(1).Border(1F).Text("Data realizacji od dnia:\n\n");

						table.Cell().Row(17).RowSpan(2).Column(2).Border(1F).Text("Dane i podpis\n" + doctor.Name + " " + doctor.Surname + "\n" + "\nDane podmiotu drukującego");


					});
				});



			}).GeneratePdf(fileTitle);

			prescription.Pdf = fileTitle;
			//prescription.Pdf = doc.GenerateImages((int index) => { return DateTime.Now.ToString(); });
			//ViewModels.ViewModelBase.DbContext.Update(prescription.Medicines);

			//Console.WriteLine("pdff:"+prescription.Pdf);
			ViewModels.ViewModelBase.DbContext.SaveChanges();
			return fileTitle;
        }


	}
}

