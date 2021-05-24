using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Entities
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<StatusEquipment> StatusEquipments { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
    }
}

