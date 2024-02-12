using HospitalWardManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalWardManager
{
    internal class PatientAdmission
    {

        // Main Method 
        private static void Main(string[] args)
        {
            // Set constants
            // Doctors
            List<Doctor> doctors = new List<Doctor>(){
                new Doctor(){Id = 1, Title = "Doctor A", AssignedPatients = 0},
                new Doctor(){Id = 2, Title = "Doctor B", AssignedPatients = 0},
                new Doctor(){Id = 3, Title = "Doctor C", AssignedPatients = 0},
                new Doctor(){Id = 4, Title = "Doctor D", AssignedPatients = 0},
                new Doctor(){Id = 5, Title = "Doctor E", AssignedPatients = 0},
            };

            // Wards
            List<Ward> wards = new List<Ward>(){
                new Ward(){Id = 1, Ward_name = "Cardiology", Ward_Capacity = 15, Doc_ID = 1},
                new Ward(){Id = 2, Ward_name = "Renal", Ward_Capacity = 10, Doc_ID = 2},
                new Ward(){Id = 3, Ward_name = "A&E", Ward_Capacity = 20, Doc_ID = 3},
                new Ward(){Id = 4, Ward_name = "Critical Care", Ward_Capacity = 8, Doc_ID = 4}
            };

            #region Scenario 1
            Console.WriteLine("Scenario - Patient 1");
            // Get target ward information 
            var ward_S1 = wards.Where(x => x.Ward_name == "Cardiology").ToList().First();

            // Alter ward availability
            ward_S1.AvailableBeds = ward_S1.Ward_Capacity - 4;

            Console.WriteLine("Target ward is: " + ward_S1.Ward_name);
            Console.WriteLine("Current available beds on: " + ward_S1.Ward_name + ": " + ward_S1.AvailableBeds);

            // Patients
            Patient patient1 = new Patient() { Id = 1, patientName = "Patient 1", DoctorId = 1, DischargeLoc = "Home"};

            // Get doctor information
            var doc_S1 = doctors.Where(x => x.Id == patient1.DoctorId).ToList().First();

            //Admit patient
            AdmitPatient(patient1, ward_S1, doc_S1);
            DischargePatient(patient1, ward_S1, doc_S1);
            #endregion

            #region Scenario 2
            Console.WriteLine("Scenario - Patient 2");
            // Get target ward information 
            var ward1_S2 = wards.Where(x => x.Ward_name == "Renal").ToList().First();
            var ward2_S2 = wards.Where(x => x.Ward_name == "Cardiology").ToList().First();

            // Alter ward availability
            ward1_S2.AvailableBeds = ward1_S2.Ward_Capacity - 6;
            ward2_S2.AvailableBeds = ward2_S2.Ward_Capacity - 12;

            Console.WriteLine("Target wards are: " + ward1_S2.Ward_name + " & " + ward2_S2.Ward_name);
            Console.WriteLine("Current available beds on: " + ward1_S2.Ward_name + ": " + ward1_S2.AvailableBeds);
            Console.WriteLine("Current available beds on: " + ward2_S2.Ward_name + ": " + ward2_S2.AvailableBeds);

            // Get doctor information
            var doc1_S2 = doctors.Where(x => x.Id == ward1_S2.Doc_ID).ToList().First();
            var doc2_S2 = doctors.Where(x => x.Title == "Doctor A").ToList().First();

            // Patients
            Patient patient2 = new Patient() { Id = 2, patientName = "Patient 2", DischargeLoc = "Other"};

            //Admit patient
            AdmitPatient(patient2, ward1_S2, doc1_S2);

            // Transfer patient
            TransferPatient(patient2, ward1_S2, ward2_S2, doc1_S2, doc2_S2);

            // Discharge patient
            DischargePatient(patient2,ward2_S2,doc2_S2 );
            #endregion

            #region Scenario 3
            Console.WriteLine("Scenario - Patient 3");
            // Get target ward information 
            var ward1_S3 = wards.Where(x => x.Ward_name == "A&E").ToList().First();
            var ward2_S3 = wards.Where(x => x.Ward_name == "Critical Care").ToList().First();

            // Alter ward availability
            ward1_S3.AvailableBeds = ward1_S3.Ward_Capacity - 19;
            ward2_S3.AvailableBeds = ward2_S3.Ward_Capacity - 2;

            Console.WriteLine("Target wards are: " + ward1_S3.Ward_name + " & " + ward2_S3.Ward_name);
            Console.WriteLine("Current available beds on: " + ward1_S3.Ward_name + ": " + ward1_S3.AvailableBeds);
            Console.WriteLine("Current available beds on: " + ward2_S3.Ward_name + ": " + ward2_S3.AvailableBeds);

            // Patients
            Patient patient3 = new Patient() { Id = 3, patientName = "Patient 3", DoctorId = 3};

            // Get doctor information
            var doc1_S3 = doctors.Where(x => x.Id == patient3.DoctorId).ToList().First();
            var doc2_S3 = doctors.Where(x => x.Title == "Doctor D").ToList().First();

            //Admit patient
            AdmitPatient(patient3, ward1_S3, doc1_S3);

            // Transfer patient
            TransferPatient(patient3, ward1_S3, ward2_S3, doc1_S3, doc2_S3);
           
            // Set patient status to deseased 
            PatientDied(patient3);

            // Discharge patient
            DischargePatient(patient3, ward2_S3, doc2_S3);
            #endregion

            #region Scenario 4
            Console.WriteLine("Scenario - Patient 4");
            // Get target ward information 
            var ward_S4 = wards.Where(x => x.Ward_name == "Cardiology").ToList().First();
            
            // Alter ward availability
            ward_S4.AvailableBeds = ward_S4.Ward_Capacity - 15;

            Console.WriteLine("Target ward is: " + wards[0].Ward_name);
            Console.WriteLine("Current available beds on: " + ward_S1.Ward_name + ": " + ward_S1.AvailableBeds);

            // Patients
            Patient patient4 = new Patient() { Id = 1, patientName = "Patient 4", DoctorId = 5, DischargeLoc = "Home" };

            // Get doctor information
            var doc_S4 = doctors.Where(x => x.Id == ward1_S2.Doc_ID).ToList().First();

            // Admit patient
            AdmitPatient(patient4, ward_S4, doc_S4);
            #endregion

        }
        public static void AdmitPatient(Patient patient, Ward ward, Doctor doc)
        {
            // Check ward availablity
            if (ward.AvailableBeds > 0)
            {
                doc.AssignedPatients = doc.AssignedPatients + 1;
                patient.IsAdmitted = true;
                patient.AdmissionDate = DateTime.Now;
                patient.WardId = ward.Id;
                ward.AvailableBeds = ward.AvailableBeds - 1;

                Console.WriteLine("Patient has been admited to " + ward.Ward_name + " successfully!");
                Console.WriteLine("Patient's Info:");
                Console.WriteLine("Patient name: " + patient.patientName);
                Console.WriteLine("Admission time is: " + patient.AdmissionDate);
                Console.WriteLine("Doctor assigned: " + doc.Title);
                Console.WriteLine("Beds now available on "+  ward.Ward_name  + " " + ward.AvailableBeds);
                Console.WriteLine("___________________________________________________________________________________________");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Cannot admit patient, ward has reached capacity! :(");
                Console.ReadKey();
            }
        }

        public static void DischargePatient(Patient patient, Ward ward, Doctor doc)
        {
            if (patient.IsDeceased == false)
            {
                patient.IsAdmitted = false;
                patient.DischargeDate = DateTime.Now;
                doc.AssignedPatients = doc.AssignedPatients - 1;
                ward.AvailableBeds = ward.AvailableBeds + 1;

                Console.WriteLine("Patient has been discharged from " + ward.Ward_name + " to " + patient.DischargeLoc + " successfully!");
                Console.WriteLine("Discharge time is: " + patient.DischargeDate);
                Console.WriteLine("Beds available on " + ward.Ward_name + " " + ward.AvailableBeds);
                Console.WriteLine("___________________________________________________________________________________________");
                Console.ReadKey();
            }
            else
            {
                patient.IsAdmitted = false;
                doc.AssignedPatients = doc.AssignedPatients - 1;
                ward.AvailableBeds = ward.AvailableBeds + 1;
                Console.WriteLine("Patient status: " + patient.DischargeLoc);
                Console.WriteLine("Time of Death: " + patient.TimeOfDeath);
                Console.WriteLine("Beds available on " + ward.Ward_name + " " + ward.AvailableBeds);
                Console.WriteLine("___________________________________________________________________________________________");
                Console.ReadKey();
            }

        }
        public static void TransferPatient(Patient patient, Ward oldWard, Ward newWard, Doctor oldDoc, Doctor newDoc)
        {
            if (newWard.AvailableBeds > 0)
            {
                patient.IsTranfered = true;
                patient.TransferDate = DateTime.Now;
                patient.WardId = newWard.Id;
                oldWard.AvailableBeds += 1;
                oldDoc.AssignedPatients -= 1;
                newDoc.AssignedPatients += 1;

                Console.WriteLine("Transfer Complete!");
                Console.WriteLine("Beds now available on " + oldWard.Ward_name+": " + oldWard.AvailableBeds);
                AdmitPatient(patient, newWard, newDoc);
            }
            else
            {
                Console.WriteLine("Cannot transfer patient, ward has reached capacity! :(");
                Console.ReadKey();
            }
        }
        
        public static void PatientDied(Patient patient) {
            patient.IsDeceased = true;
            patient.DischargeLoc = "Died";
            patient.TimeOfDeath = DateTime.Now;
        }
    }
}