using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalWardManager.Models
{
    public class Ward
    {
        public int Id { get; set; }
        public string Ward_name { get; set; } 
        public int Ward_Capacity { get; set; } // Total number of beds
        public int AvailableBeds { get; set; } // Beds available
        public int Doc_ID {  get; set; } // Reference of the doctor on ward
        public bool IsFull { get; set; } = false; // Status of the ward
    }
}
