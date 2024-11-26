using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;

namespace LISCareDataAccess.LISCareDbContext
{
    public class LISCareDbContext : DbContext
    {
        public LISCareDbContext()
        {

        }
        public LISCareDbContext(DbContextOptions<LISCareDbContext> options)
            : base(options)
        {

        }
        public object FromSql(string v, SqlParameter[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
