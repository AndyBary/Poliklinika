using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetLaba2.Models
{
    public partial class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        public int Kabinet { get; set; }

        public int ZapisId { get; set; }

        public string Doctor_FIO { get; set; }

        public string Doctor_Speciality { get; set; }

        public virtual Zapis Zapis { get; set; }
    }
}
