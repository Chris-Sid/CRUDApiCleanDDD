using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Infrastructure.Logging
{
    public interface IAppLogger<T>
    {
        void LogError(Exception ex, string message);
        void LogInfo(string message);
    }
}
