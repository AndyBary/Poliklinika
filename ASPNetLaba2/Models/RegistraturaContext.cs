using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ASPNetCoreApp.Models;

namespace ASPNetLaba2.Models
{
    public partial class RegistraturaContext : IdentityDbContext<User>
    {
        #region Constructor
        public RegistraturaContext(DbContextOptions<RegistraturaContext>
        options)
        : base(options)
        { }
        #endregion
        public virtual DbSet<Pacient> Pacient { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Zapis> Zapis { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Zapis>(entity =>
            {
                entity.Property(e => e.ZapisId).IsRequired();
            });

            modelBuilder.Entity<Pacient>(entity =>
            {
                entity.HasOne(d => d.Zapis)
                .WithMany(p => p.Pacient)
                .HasForeignKey(d => d.ZapisId);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasOne(d => d.Zapis)
               .WithMany(p => p.Doctor)
               .HasForeignKey(d => d.ZapisId);
            });

        }
    }
}
