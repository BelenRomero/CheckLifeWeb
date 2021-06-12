using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CheckLifeWeb.Models
{
    [Table("Enfermero")]
    public class Enfermero
    {
        [Key]
        [Column("ID")]
        public int Matricula { get; set; }

        [MaxLength(50)]
        public string Nombre{ get; set; }

        [MaxLength(50)]
        public string Apellido{ get; set; }

        public Vacunatorio Vacunatorio { get; set; }
    }
}
