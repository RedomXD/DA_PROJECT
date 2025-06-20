using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace iTasks
{
    class Basededados : DbContext
    {
        public DbSet<Utilizador> Utilizadors { get; set; }
        public DbSet<Gestor> Gestors { get; set; }
        public DbSet<Programador> Programadors { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<TipoTarefa> tipoTarefas { get; set; }
    }
}
