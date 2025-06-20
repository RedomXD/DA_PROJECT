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
    }
}