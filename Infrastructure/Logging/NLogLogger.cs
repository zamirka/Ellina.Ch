using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testProject.Infrastructure.Logging
{
    public class NLogLogger : ILogger
    {
        private Logger _logger;

        public NLogLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Error(string message, Exception ex)
        {
            _logger.ErrorException(message, ex);
        }
    }
}