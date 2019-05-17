using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetLaba2.Models
{
    public partial class Zapis
    {
        public Zapis()
        {
            Pacient = new HashSet<Pacient>();
        }

        [Key]
        public int ZapisId { get; set; }

        public string Zapis_date { get; set; }

        public string Zapis_time { get; set; }

        public int PacientId { get; set; }

        public int Kabinet { get; set; }

        public string Url { get; set; }

        public virtual ICollection<Pacient> Pacient { get; set; }

        public virtual ICollection<Doctor> Doctor { get; set; }
    }
}
