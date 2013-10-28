using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using testProject.Models;

namespace testProject
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DbSet<User> Users { get; set; }
    }
}