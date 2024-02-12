using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalWardManager.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public int AssignedPatients { get; set; } = 0;
        public string Title { get; set; }
    }
}
