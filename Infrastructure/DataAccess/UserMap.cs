using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using testProject.Models;

namespace testProject.Infrastructure.DataAccess
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {

        }
    }
}