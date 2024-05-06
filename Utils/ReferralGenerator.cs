using bazy1.Models;
using bazy1.ViewModels;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Modes.Gcm;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy1.Utils {
	public class ReferralGenerator {
		public static string generate(Referral referral) {
			Console.WriteLine("genruje sie;");

			FontManager.RegisterFont(File.OpenRead("LibreBarcode39Text-Regular.ttf"));
			QuestPDF.Settings.License = LicenseType.Community;
			string fileTitle = Directory.GetCurrentDirectory() + "\\" + "skierowanie" +
				referral.Id +
				DateTime.Now.Day.ToString() +
				DateTime.Now.Month +
				DateTime.Now.Year.ToString() +
				".pdf";

			Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Size(PageSizes.EnvDL);
					page.Margin(5F);
					page.Content().Table(table =>
					{
					table.ExtendLastCellsToTableBottom();
					table.ColumnsDefinition(columns =>
					{
						columns.RelativeColumn(1);
						columns.RelativeColumn(1);
					});

					table.Cell().Row(1).ColumnSpan(2).Text("Skierowanie do").AlignCenter();
					table.Cell().Row(2).ColumnSpan(2).Text(referral.MedicalEntity).Bold().AlignCenter();
					table.Cell().Row(3).ColumnSpan(2).Text(referral.Code).AlignCenter().FontFamily("Libre Barcode 39 Text").FontSize(25);
					table.Cell().Row(4).Column(2).Text("Wystawiono " + referral.Date.Value.ToShortDateString()).AlignRight();
					table.Cell().Row(5).ColumnSpan(2).LineHorizontal(1F);
					table.Cell().Row(6).Column(1).AlignLeft().Text("Pacjent").Bold();
					table.Cell().Row(6).Column(2).AlignLeft().Text(referral.Patient.Name + " " + referral.Patient.Surname + "\nPESEL " + referral.Patient.Pesel + "\nur. " + referral.Patient.BirthDate.Value.ToShortDateString() + "r.\n");
					table.Cell().Row(7).Column(1).Text("Wystawca ").Bold();
					table.Cell().Row(7).Column(2).AlignLeft().Text("Specjalistyczna przychodnia lekarska\n25 - 155 Warszawa, ul.Kwiatowa 22");
					table.Cell().Row(8).ColumnSpan(2).LineHorizontal(1F);
					table.Cell().Row(9).Column(1).AlignLeft().Text("Rozpoznanie").Bold();
					table.Cell().Row(10).Column(1).Text(referral.Disease).AlignLeft();
					table.Cell().Row(11).Column(1).Text("\nInne informacje").AlignLeft().Bold();
					table.Cell().Row(12).Column(1).Text(referral.Information);
					table.Cell().Row(13).ColumnSpan(2).LineHorizontal(1F);
					table.Cell().Row(14).Column(1).Text("Osoba wystawiająca").AlignLeft().Bold();
					table.Cell().Row(15).AlignLeft().Text(referral.Doctor.Name + " " + referral.Doctor.Surname);
					});
				});



			}).GeneratePdf(fileTitle);
			Console.WriteLine("skonczul");
			referral.Pdf = fileTitle;
			//prescription.Pdf = doc.GenerateImages((int index) => { return DateTime.Now.ToString(); });
			//ViewModels.ViewModelBase.DbContext.Update(prescription.Medicines);

			//Console.WriteLine("pdff:"+prescription.Pdf);
			ViewModels.ViewModelBase.DbContext.SaveChanges();
			return fileTitle;
		}

	}
}
