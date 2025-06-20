using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace iTasks.Controllers
{
    class ControllerUtilizadores
    {
        public void GestorAdicionar(string nome, string username, string palavra_passe, Departamento departamento, bool gereutulizador)
        {
            using (var db = new Basededados())
            {
                var gestor = new Gestor { Nome = nome, Username = username, Password = palavra_passe, Departamento = departamento, GereUtilizadores = gereutulizador };

                db.Utilizadors.Add(gestor);
                db.SaveChanges();
            }
        }

        public void ProgramadorAdicionar (string nome, string username, string palavra_passe, NivelExperiencia nivelExperiencia, Gestor gestor)
        {
            using (var db = new Basededados())
            {
                var programador = new Programador { Nome = nome, Username = username, Password = palavra_passe, NivelExperiencia = nivelExperiencia, Gestor = gestor};

                db.Utilizadors.Attach(gestor);
                db.Utilizadors.Add(programador);
                db.SaveChanges();
            }
        }
        public List<Gestor> ListarGestores()
        {
            List<Gestor> ListaGestores = new List<Gestor>();
            using (var db = new Basededados())
            {
              var querygestores = from Gestores in db.Gestors select Gestores;
                foreach (var gestor in querygestores)
                {
                    ListaGestores.Add(gestor);
                }
                return ListaGestores;
            }
        }

        public List<Programador> ListarProgramadores()
        {
            List<Programador> ListaProgramador = new List<Programador>();
            using (var db = new Basededados())
            {
                var queryprogramadores = from Programador in db.Programadors select Programador;
                foreach (var programador in queryprogramadores)
                {
                    ListaProgramador.Add(programador);
                }
                return ListaProgramador;
            }
        }
    }

}