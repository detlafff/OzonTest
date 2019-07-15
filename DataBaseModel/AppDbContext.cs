using System.Collections.Generic;
using CoreLibrary.BusinessEntities;
using Microsoft.EntityFrameworkCore;

namespace DataBaseModel
{
    public class AppDbContext : DbContext
    {
        private readonly string _nameOrConnectionString;

        public AppDbContext(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_nameOrConnectionString);
        }

        public virtual DbSet<ExchangeRate> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var config = modelBuilder.Entity<ExchangeRate>();


            config.ToTable("ExchangeRates", "dbo")
                .HasKey(k => k.Id);

            config
                .Property(p => p.Id)
                .HasColumnName("ExchangeRateId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd()
                .IsRequired();

            config.Property(p => p.Amount)
                .HasColumnName("Amount")
                .HasColumnType("int")
                .IsRequired();

            config.Property(p => p.Currency)
                .HasColumnName("CurrencyId")
                .HasColumnType("int")
                .IsRequired();

            config.Property(p => p.Rate)
                .HasColumnName("Rate")
                .HasColumnType("decimal")
                .IsRequired();

            config.Property(p => p.Date)
                .HasColumnName("Date")
                .HasColumnType("date")
                .IsRequired();

            config.Property(p => p.CreateDate)
                .HasColumnName("CreateDate")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("getdate()")
                .IsRequired();

            config.Property(p => p.UpdateDate)
                .HasColumnName("UpdateDate")
                .HasColumnType("DATETIME")
                .IsRequired(false);
        }
    }
}