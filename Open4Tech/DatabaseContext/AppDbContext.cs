using KidsVaccineReminder.Model;
using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsVaccineReminder.DatabaseContext
{
    public class AppDbContext: DbContext
    {
        
        public AppDbContext() : base("name=ConnectionString") 
        {
            
        }

        public DbSet<UserModel> UserModel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<AppDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
    
}
