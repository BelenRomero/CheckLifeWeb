using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckLifeWeb.Models
{
    [Table("Medico")]
    public class Medico
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo DNI es requerido.")]
        public int DNI { get; set; }

        //[MaxLength(50)]
        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        [StringLength(50)]
        public string Nombre { get; set; }

        //[MaxLength(50)]
        [Required(ErrorMessage = "El campo Apellido es requerido.")]
        [StringLength(50)]
        public string Apellido { get; set; }

        public int Edad { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public int? NacionalidadID { get; set; }
        public Nacionalidad Nacionalidad { get; set; }

        //[RegularExpres*In("expression")]
        public string Email { get; set; }

        public int? LoginID { get; set; }
        public Login Login { get; set; }

        [NotMapped]
        public string Password { get; set; } /*= "";*/
        [NotMapped]
        public string User { get; set; }

        public string Telefono { get; set; }

        public int Matricula { get; set; }

        public ICollection<Paciente> Pacientes { get; set; }
    }
}
