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


        //Vericação de Username Único , Rule 3 do enunciado
        public bool UsernameExist(string username)
        {
            using (var db = new Basededados())
            {
                return db.Utilizadors.Any(u => u.Username == username);
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



        //Novas funçoes Atualizar e Apagar conforme o que era pedido pelo CRUD
        // Para o Gestor
        public void AtualizarGestor(int id, string nome, string username, string password, Departamento departamento, bool gereUtilizadores)
        {
            using (var db = new Basededados())
            {
                var gestor = db.Gestors.Find(id);

                if (gestor == null)
                    throw new InvalidOperationException("Gestor não encontrado.");

                gestor.Nome = nome;
                gestor.Username = username;
                gestor.Password = password;
                gestor.Departamento = departamento;
                gestor.GereUtilizadores = gereUtilizadores;

                db.SaveChanges();
            }
        }

        public void ApagarGestor(int id)
        {
            using (var db = new Basededados())
            {
                var gestor = db.Gestors.Include(g => g.Id).FirstOrDefault(g => g.Id == id);

                if (gestor == null)
                    throw new InvalidOperationException("Gestor não encontrado.");

                db.Utilizadors.Remove(gestor);
                db.SaveChanges();
            }
        }


        // Agora para o Programador
        public void AtualizarProgramador(int id, string nome, string username, string password, NivelExperiencia nivel, Gestor gestor)
        {
            using (var db = new Basededados())
            {
                var programador = db.Programadors.Find(id);

                if (programador == null)
                    throw new InvalidOperationException("Programador não encontrado.");

                programador.Nome = nome;
                programador.Username = username;
                programador.Password = password;
                programador.NivelExperiencia = nivel;

                // Anexar o gestor para evitar conflito de tracking
                db.Utilizadors.Attach(gestor);
                programador.Gestor = gestor;

                db.SaveChanges();
            }
        }

        public void ApagarProgramador(int id)
        {
            using (var db = new Basededados())
            {
                var programador = db.Programadors.FirstOrDefault(p => p.Id == id);

                if (programador == null)
                    throw new InvalidOperationException("Programador não encontrado.");

                db.Utilizadors.Remove(programador);
                db.SaveChanges();
            }
        }


    }

}