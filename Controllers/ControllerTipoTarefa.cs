using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Controllers
{
    class ControllerTipoTarefa
    {
        public void TipoTarefaAdicionar(string descricao)
        {
            using (var db = new Basededados())
            {
                var TipoTarefa = new TipoTarefa {TipoTarefaDesc = descricao};
                db.tipoTarefas.Add(TipoTarefa);
                db.SaveChanges();
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
