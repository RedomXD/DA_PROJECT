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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Desativa cascade delete entre Tarefa e Gestor
            modelBuilder.Entity<Tarefa>()
                .HasRequired(t => t.Gestor)
                .WithMany()
                .HasForeignKey(t => t.GestorID)
                .WillCascadeOnDelete(false);

            // Desativa cascade delete entre Tarefa e Programador
            modelBuilder.Entity<Tarefa>()
                .HasRequired(t => t.Programador)
                .WithMany()
                .HasForeignKey(t => t.ProgramadorID)
                .WillCascadeOnDelete(false);
        }


        // ESTA FUNCAO Recria a base de Dados semprpe que houver alguma alteração no modelo, permitindo este dar sempre run
        public Basededados() : base("name=Basededados")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Basededados>());
        }

    }

}
