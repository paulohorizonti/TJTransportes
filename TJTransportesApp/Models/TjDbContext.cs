using System.Data.Entity;

namespace TJTransportesApp.Models
{
    [DbConfigurationType(typeof(MySql.Data.EntityFramework.MySqlEFConfiguration))]
    public class TjDbContext : DbContext
    {
        public TjDbContext() : base("name=TjDbContext") { }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Departamento> Departamentos { get; set; }
        public virtual DbSet<Perfil> Perfis { get; set; }
        public virtual DbSet<Acesso> Acessos { get; set; }

        public virtual DbSet<Conta> Contas { get; set; }
    }

}