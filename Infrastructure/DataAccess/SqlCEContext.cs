using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using testProject.Models;

namespace testProject.Infrastructure.DataAccess
{
    public class SqlCEContext : DbContext
    {
        public SqlCEContext()
        {
        }

        public DbSet<User> Users { get; set; }
    }
}