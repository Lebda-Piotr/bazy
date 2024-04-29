﻿using bazy1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bazy1.Models;

using dbm = bazy1.Models;
using System.Windows.Input;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;

namespace bazy1.ViewModels.Doctor.Pages
{
    public class PatientListViewModel : ViewModelBase
    {
        private User user;
        private Patient _selectedPatient;
        private dbm.Doctor doctor;
        private ObservableCollection<Patient> _patientsList;
        public ICommand ShowMedicalHistoryCommand { get; }
        public ICommand ShowAddDiseaseCommand { get; }


        public PatientListViewModel(User user, DoctorViewModel viewModel)
        {
            Console.WriteLine(user.Name + user.Surname + user.Id);
            this.user = user;
            doctor = DbContext.Doctors.Where(doctor => doctor.UserId == user.Id).First();
            //Console.WriteLine("dok: " + .Count());
            _patientsList = new(DbContext.Patients.Where(patient => patient.Doctors.Contains(doctor)).ToList());
            Console.WriteLine("cn:" + _patientsList.Count());
            Console.WriteLine(doctor.Offices.Count());
            Console.WriteLine($"id:{doctor.UserId}, {doctor.Id}");
            //doctor.Patients.Add(new Patient() { Name = "Pacjent #1"});
            //DbContext.SaveChanges();
            ShowMedicalHistoryCommand = new BasicCommand((object obj) => {
                Console.WriteLine(SelectedPatient.Name + SelectedPatient.Surname);
                if (SelectedPatient != null) viewModel.CurrentViewModel = new MedicalHistoryViewModel(SelectedPatient);
            });
            ShowAddDiseaseCommand = new BasicCommand((object obj) =>
            {
                if (SelectedPatient != null) viewModel.CurrentViewModel = new AddDiseaseViewModel(SelectedPatient);
            });

            Console.WriteLine(doctor.Name);


            //_patientsList = new(doctor.Patients);
            Console.WriteLine(_patientsList.Count());
        }

        public ObservableCollection<Patient> PatientsList
        {
            get => _patientsList;
            set
            {
                _patientsList = value;
                OnPropertyChanged(nameof(PatientsList));
            }
        }

        public Patient SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                _selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }
    }
}
