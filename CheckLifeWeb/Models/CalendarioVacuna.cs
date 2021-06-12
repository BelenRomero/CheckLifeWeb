using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CheckLifeWeb.Models
{
    [Table("CalendarioVacuna")]
    public class CalendarioVacuna
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Descripcion { get; set; }
    }
}
