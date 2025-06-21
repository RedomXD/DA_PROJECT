using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Controllers
{
    public class ControllerTipoTarefa
    {
        public bool TipoTarefaAdicionar(string descricao)
        {
            // Validação
            if (string.IsNullOrWhiteSpace(descricao))
            {
                //throw new ArgumentException("A descrição não pode estar vazia.");
                return false;
            }

            using (var db = new Basededados())
            {
                // Validaçoa se ja existe a tipitarefa
                bool exist = db.tipoTarefas.Any(t => t.TipoTarefaDesc == descricao);
                if (exist)
                {
                    //throw new InvalidOperationException("Tipo de Tarefa já existe.");
                    return false;
                }

                var tipoTarefa = new TipoTarefa {TipoTarefaDesc = descricao};
                db.tipoTarefas.Add(tipoTarefa);
                db.SaveChanges();
                return true;
            }
        }

        public bool AtualizarTipoTarefa(int id, string novaDescricao)
        {

            if (string.IsNullOrWhiteSpace(novaDescricao))
            {
                return false;
            }
            

            using (var db = new Basededados())
            {
                var tipoTarefa = db.tipoTarefas.FirstOrDefault(t => t.TipoTarefaId == id);
                if (tipoTarefa == null)
                {
                    return false;
                }

                tipoTarefa.TipoTarefaDesc = novaDescricao;
                db.SaveChanges();

                return true;
            }
        }

        public bool ApagarTipoTarefa(int id)
        {
            using (var db = new Basededados())
            {
                var tipoTarefa = db.tipoTarefas.FirstOrDefault(t => t.TipoTarefaId == id);
                if (tipoTarefa == null)
                {
                    return false;
                }

                db.tipoTarefas.Remove(tipoTarefa);
                db.SaveChanges();

                return true;
            }
        }

        public List<TipoTarefa> ListaTipoTarefa()
        {
            List<TipoTarefa> ListaTipoTarefa = new List<TipoTarefa>();
            using (var db = new Basededados())
            {
                var querytipotarefa = from TipoTarefa in db.tipoTarefas select TipoTarefa;
                foreach (var tipoTarefa in querytipotarefa)
                {
                    ListaTipoTarefa.Add(tipoTarefa);
                }
                return ListaTipoTarefa;
            }
        }
    }

}
