namespace Ecobit.Domain
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EcobitDBEntities : DbContext
    {
        public EcobitDBEntities()
            : base("name=EcobitDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Account> Account { get; set; }
        public DbSet<Privilege> Privilege { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserPrivilege> UserPrivilege { get; set; }
    }
}
