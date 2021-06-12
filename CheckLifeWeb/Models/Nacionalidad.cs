using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CheckLifeWeb.Models
{
    [Table("Nacionalidad")]
    public class Nacionalidad
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(30)]
        public string Descripcion { get; set; }
    }
}
