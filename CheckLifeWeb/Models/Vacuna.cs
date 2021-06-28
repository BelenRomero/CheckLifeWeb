using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckLifeWeb.Models
{
    [Table("Vacunas")]
    public class Vacuna
    {
        [Key]
        public int ID { get; set; }

        public int? CalendarioVacunaID { get; set; }
        public CalendarioVacuna CalendarioVacuna { get; set; }

        public DateTime FechaAplicada { get; set; }

        public string MarcaComercialLote { get; set; }

        //Sello y Firma
        public string SelloFirma {get; set;}

        public int MatriculaEnfermero { get; set; }

        public int PacienteID { get; set; }
        public Paciente Paciente { get; set; }

        public int? EstadoID { get; set; }
        public EstadoVacuna Estado { get; set; }

    }
}
