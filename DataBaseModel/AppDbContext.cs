using DataBaseModel.Entities;
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
            var config = modelBuilder.Entity<ExchangeRate>()
                .ToTable("blogs"); ;

            config.ToTable("ExchangeRate", "dbo")
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
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }

    
}