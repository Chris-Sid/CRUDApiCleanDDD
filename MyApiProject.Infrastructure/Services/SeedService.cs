using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApiProject.Infrastructure.Services
{
    public class SeedService
    {
        private readonly IConfiguration _config;

        public SeedService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SeedAdminAsync()
        {
            var connStr = _config.GetConnectionString("PostgreSQL");
            await using var conn = new NpgsqlConnection(connStr);
            await conn.OpenAsync();

            const string sql = @"
            INSERT INTO admins (Id, Username, Passwordhash, Role)
            VALUES (@Id, @Username, @Passwordhash, @Role)
            ON CONFLICT (username) DO NOTHING;
        ";

            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("Id", Guid.Parse("d3e2bff3-4cbf-42ab-bd8b-dc6fbd3ec7a1"));
            cmd.Parameters.AddWithValue("Username", "admin");
            cmd.Parameters.AddWithValue("Passwordhash", "$2a$11$1jAqiPyb5QPxdjH6HIJ3WuaGLnrI8yhFVeOg6ctUuhVOW7Cl4bUPK");
            cmd.Parameters.AddWithValue("Role", "Admin");

            await cmd.ExecuteNonQueryAsync();
        }
    }

}
