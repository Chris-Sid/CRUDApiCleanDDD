using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string? GenerateJwtToken(string username, string password);
    }
}
