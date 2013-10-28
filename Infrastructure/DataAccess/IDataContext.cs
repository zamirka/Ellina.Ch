using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testProject.Models;

namespace testProject.Infrastructure.DataAccess
{
    public interface IDataContext
    {
        IQueryable<User> Users { get; set; }
    }
}