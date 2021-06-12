//Para desttingir si el usuario es medico, centtro o un usuario comun 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckLifeWeb.Models
{
    [Table("Rol")]
    public class Rol
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(15)]
        public string Descripcion { get; set; }
    }
}
