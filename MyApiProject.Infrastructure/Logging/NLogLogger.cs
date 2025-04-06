using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Infrastructure.Logging
{
    public class NLogLogger<T> : IAppLogger<T>
    {
        private static readonly ILogger _logger = LogManager.GetLogger(typeof(T).FullName);

        public void LogError(Exception ex, string message)
        {
            _logger.Error(ex, message);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }
    }
}
