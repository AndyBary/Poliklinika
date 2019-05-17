using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetLaba2.Models
{
    public partial class Pacient
    {
        [Key]
        public int PacientId { get; set; }

        public int Polis_number { get; set; }

        public int ZapisId { get; set; }

        public string Pacient_FIO { get; set; }

        public string Adres { get; set; }

        public virtual Zapis Zapis { get; set; }
    }
}
