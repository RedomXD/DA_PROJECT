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
    public class Tarefa
    {

        public enum estadoatual
        {
            todo,
            doing,
            done
        }

        public int Id { get; set; }

        [Required]
        public string Descricao { get; set; }

        public estadoatual EstadoAtual { get; set; } = estadoatual.todo;

        public DateTime DataPrevistaInicio { get; set; }
        public DateTime DataPrevistaFim { get; set; }

        public DateTime DataRealInicio { get; set; }
        public DateTime DataRealFim { get; set; }
        public DateTime DataCreation { get; set; }

        public int Ordem { get; set; }
        public int StoryPoints { get; set; }

        public int GestorID { get; set; }
        public Gestor Gestor { get; set; }
        //public virtual Gestor Gestor { get; set; }


        public int TipoTarefaId { get; set; }
        public TipoTarefa TipoTarefa { get; set; }
        // public virtual TipoTarefa TipoTarefa { get; set; }


        public int ProgramadorId { get; set; }
        public Programador Programador { get; set; }
        // public virtual Programador Programador { get; set; }


        // tipo assim, n sei se é 100% necessario passar o id se ja passas o programador e o tipotarefa em cima mas yah

        public override string ToString()
        {
            return $"{Descricao} - {Programador.Nome} - Ordem: {Ordem}";

        }
    }

    // public enum EstadoAtual não tem de estar fora da class da Tarefa ?
    /*
    public enum estadoatual
    {
        todo,
        doing,
        done
    }
    */
}
