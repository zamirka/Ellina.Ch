using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using testProject.Models;

namespace testProject.Infrastructure.DataAccess
{
    public partial class SqlCEContext : DbContext, IDataContext
    {
        static SqlCEContext()
        {
            Database.SetInitializer<SqlCEContext>(null);
        }

        public SqlCEContext()
            : base("Name=SqlCEContext")
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}