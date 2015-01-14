using System.Data.Entity;
using DynamicWebAPI.Models;

namespace DynamicWebAPI
{
    public class DynamicDbContext : DbContext
    {
        //Contructor for DbContext 
        public DynamicDbContext() : base("name=DynamicDbContext"){}

        public DbSet<Category> Category { get; set; }
    }
}