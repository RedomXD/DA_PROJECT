using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTasks
{
    public enum NivelExperiencia
    {
        Junior,
        Senior
    }

    public enum Departamento
    {
        IT,
        Marketing,
        Administracao
    }

    public class Utilizador
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class Gestor : Utilizador
    {
        public Departamento Departamento { get; set; }

        public bool GereUtilizadores { get; set; }

        public override string ToString()
        {
            return $"{Nome} - {Departamento}";
        }
    }
    public class Programador : Utilizador
    {
        public NivelExperiencia NivelExperiencia { get; set; }
        public int GestorID { get; set; }

        [ForeignKey("GestorID")]
        public Gestor Gestor { get; set; }

        public override string ToString()
        {
            return $"{Nome}";
        }
    }
}
