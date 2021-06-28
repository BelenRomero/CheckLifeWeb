using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckLifeWeb.Models
{
    [Table("Centro")]
    public class Vacunatorio
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Nombre { get; set; }

        public int CUIT { get; set; }

        [MaxLength(50)]
        public string Direccion { get; set; }

        [MaxLength(50)]
        public string Localidad { get; set; }

        [MaxLength(15)]
        public string Telefono { get; set; }

        public string Email { get; set; }

        public int? LoginID { get; set; }
        public Login Login { get; set; }

        [NotMapped]
        public string Password { get; set; } /*= "";*/
        [NotMapped]
        public string User { get; set; }

        //public ICollection<Enfermero> Enfermeros { get; set; }

    }
}
