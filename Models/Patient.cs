using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalWardManager.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string patientName { get; set; }
        public string DischargeLoc { get; set; }    
        public int DoctorId {  get; set; }
        public int WardId { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime DischargeDate { get; set; } 
        public DateTime TimeOfDeath { get; set; }   
        public DateTime TransferDate {  get; set; }
        public bool IsDeceased {  get; set; } = false;
        public bool IsAdmitted { get; set; } = false;
        public bool IsTranfered { get; set; } = false;  
    }
}
