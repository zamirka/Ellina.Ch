using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testProject.Infrastructure.Logging
{
    public interface ILogger
    {
        void Info(string message);
        void Error(string message, Exception ex);
    }
}