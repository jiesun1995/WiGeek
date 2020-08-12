using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using WiGeek.Domain.DepartmentAggregate;
using WiGeek.Domain.MarriageAggregate;
using WiGeek.Domain.MedicalRecordsAggregate;
using WiGeek.Domain.OrderAggregate;
using WiGeek.Domain.ValueObject;
using WiGeek.Domain.WardAggregate;
using WiGeek.Domain.WorkAggregate;

namespace WiGeek.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class WiGeekDbContext: AbpDbContext<WiGeekDbContext>
    {
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<Marriage> Marriages { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<PhysicalSigns> PhysicalSigns { get; set; }
        public DbSet<MedicalRecords> MedicalRecords { get; set; }

        public WiGeekDbContext(DbContextOptions<WiGeekDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);           
        }
    }
}
