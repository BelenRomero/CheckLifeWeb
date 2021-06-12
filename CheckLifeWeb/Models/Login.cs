using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CheckLifeWeb.Models
{
    [Table("Login")]
    public class Login
    {
        [Key]
        public int ID { get; set; }

        public string User { get; set; }/* = 0;*/ //si es centro ingresa por cuit, si es paciente por dni y si es medico por matricula

        [MaxLength(8)]
        public string Password { get; set; } /*= "";*/

        [NotMapped]
        public string VerificarPassword { get; set; } /*= "";*/

        public int RolID { get; set; }
        public Rol Rol { get; set; } //Medico, paciente o centro

        public Login()
        {

        }

        public Login(string usuario, string contrasenia)
        {
            this.User = usuario;
            this.Password = contrasenia;
        }

        public static implicit operator Login(Task<Login> v)
        {
            throw new NotImplementedException();
        }

    }
}
