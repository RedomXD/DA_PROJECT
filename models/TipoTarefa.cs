using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTasks
{
    public  class TipoTarefa
    {
        [Key]
        public int TipoTarefaId { get; set; }

        [Required]
        public string TipoTarefaDesc { get; set; }

        public override string ToString()
        {
            return $"{TipoTarefaDesc}";
        }
    }
}
