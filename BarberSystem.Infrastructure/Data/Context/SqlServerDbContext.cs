using BarberSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberSystem.Infrastructure.Data.Context
{
    public class SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : DbContext(options)
    {
        public DbSet<IdentitySystem> identitysSystem { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<WorkSchedule> workSchedules { get; set; }
        public DbSet<Scheduling> schedulings { get; set; }
        public DbSet<ServiceProvided> servicesProvideds { get; set; }
        public DbSet<SchedulingService> schedulingServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentitySystem>()
                .OwnsOne(i => i.Email, email =>
                {
                    email.Property(e => e.Value)
                    .HasColumnName("Email")
                    .HasMaxLength(150)
                    .IsRequired();

                    email.HasIndex(e => e.Value).IsUnique();
                });

            modelBuilder.Entity<IdentitySystem>()
                .OwnsOne(i => i.Password, password =>
                {
                    password.Property(p => p.HashPassword)
                    .HasColumnName("Hash")
                    .IsRequired();

                    password.Property(p => p.SaltPassword)
                    .HasColumnName("Salt")
                    .IsRequired();
                });

            modelBuilder.Entity<Customer>()
                .OwnsOne(i => i.CPF, cpf =>
                {
                    cpf.Property(c => c.Value)
                    .HasColumnName("CPF")
                    .HasMaxLength(11)
                    .IsFixedLength()
                    .IsRequired();

                    cpf.HasIndex(c => c.Value).IsUnique();
                });

            modelBuilder.Entity<User>()
                .OwnsOne(i => i.CPF, cpf =>
                {
                    cpf.Property(c => c.Value)
                    .HasColumnName("CPF")
                    .HasMaxLength(11)
                    .IsFixedLength()
                    .IsRequired();

                    cpf.HasIndex(c => c.Value).IsUnique();
                });

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.IdentitySystem)
                .WithOne(c => c.Customer)
                .HasForeignKey<Customer>(c => c.IdentitySystemId)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasOne(u => u.IdentitySystem)
                .WithOne(u => u.User)
                .HasForeignKey<User>(u => u.IdentitySystemId)
                .IsRequired();

            modelBuilder.Entity<WorkSchedule>()
                .HasOne(u => u.User)
                .WithOne()
                .HasForeignKey<WorkSchedule>(w => w.UserId)
                .IsRequired();

            modelBuilder.Entity<Scheduling>(s =>
            {
                s.HasOne(sc => sc.Customer)
                .WithMany(sc => sc.Schedulings)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(sc => sc.User)
                .WithMany(sc => sc.Schedulings)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                s.HasMany(sc => sc.ServicesProvided)
                .WithMany(sc => sc.Schedulings)
                .UsingEntity<SchedulingService>(
                    sp => sp.HasOne<ServiceProvided>().WithMany(x => x.Services),
                    sc => sc.HasOne<Scheduling>().WithMany(x => x.Services));
            });

            modelBuilder.Entity<ServiceProvided>(s =>
            {
                s.Property(e => e.Value)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            });

            modelBuilder.Entity<SchedulingService>(s =>
            {
                s.Property(e => e.PriceAtMoment)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            });
        }
    }
}
