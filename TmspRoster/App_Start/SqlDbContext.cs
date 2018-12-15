using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TmspRoster.Models.Member;

namespace TmspRoster.App_Start
{
    public class SqlDbContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
    }
}