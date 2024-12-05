using Microsoft.EntityFrameworkCore;
using XX_MountainBrigade.Models;

public class XXMBContext : DbContext
{
    public XXMBContext(DbContextOptions<XXMBContext> options) : base(options) { }

    public DbSet<Company> tblCompany { get; set; }
    public DbSet<Rank> tblRanks { get; set; }
    public DbSet<Personnel> tblPersonnel { get; set; }
    public DbSet<Regiment> tblRegiments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
    }

  
     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define primary keys
        modelBuilder.Entity<Company>().HasKey(c => c.CoyId);
        modelBuilder.Entity<Rank>().HasKey(r => r.RankId);
        modelBuilder.Entity<Personnel>().HasKey(p => p.PersId);
        modelBuilder.Entity<Regiment>().HasKey(r => r.RegId);

        // Configure relationships
        modelBuilder.Entity<Personnel>()
            .HasOne(p => p.Company)
            .WithMany(c => c.Personnel)
            .HasForeignKey(p => p.CoyId)
            .OnDelete(DeleteBehavior.Restrict); // Changed from Cascade to Restrict

        modelBuilder.Entity<Regiment>()
            .HasOne(r => r.Company)
            .WithMany(c => c.Regiments)
            .HasForeignKey(r => r.CoyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Regiment>()
            .HasMany(r => r.Personnel)
            .WithOne(p => p.Regiment)
            .HasForeignKey(p => p.RegimentId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Personnel>()
            .HasOne(p => p.Rank)
            .WithMany(r => r.Personnel)
            .HasForeignKey(p => p.RankId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletion for Rank
 
    modelBuilder.Entity<Company>().HasData(
            new Company { CoyId = 1, CoyName = "A Coy" },
            new Company { CoyId = 2, CoyName = "B Coy" },
            new Company { CoyId = 3, CoyName = "C Coy" }
        );

        // Seed data for tblRanks
        modelBuilder.Entity<Rank>().HasData(
            new Rank { RankId = 1, RankName = "Gunner" },
            new Rank { RankId = 2, RankName = "Rfn" },
            new Rank { RankId = 3, RankName = "Sepoy" },
            new Rank { RankId = 4, RankName = "Ln Naik" },
            new Rank { RankId = 5, RankName = "Havildar" },
            new Rank { RankId = 6, RankName = "Sub Major" }
        );

        // Seed data for tblRegiments
        modelBuilder.Entity<Regiment>().HasData(
            new Regiment { RegId = 1, RegimentName = "1st Mountain Regiment", CoyId = 1 },
            new Regiment { RegId = 2, RegimentName = "2nd Mountain Regiment", CoyId = 2 },
            new Regiment { RegId = 3, RegimentName = "3rd Mountain Regiment", CoyId = 3 }
        );

        // Seed data for tblPersonnel
        modelBuilder.Entity<Personnel>().HasData(
            new Personnel
            {
                PersId = 1,
                PersNo = "P001",
                CoyId = 1,
                RankId = 1,
                RegimentId = 1, // Linked to Regiment 1
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Now.AddYears(-30),
                PermanentAddress = "123 Main St, City, Country",
                TypeOfPersonnel = "Regular"
            },
            new Personnel
            {
                PersId = 2,
                PersNo = "P002",
                CoyId = 2,
                RankId = 2,
                RegimentId = 2, // Linked to Regiment 2
                FirstName = "Jane",
                LastName = "Smith",
                DateOfBirth = DateTime.Now.AddYears(-25),
                PermanentAddress = "456 Oak St, City, Country",
                TypeOfPersonnel = "Regular"
            },
            new Personnel
            {
                PersId = 3,
                PersNo = "P003",
                CoyId = 1,
                RankId = 1,
                RegimentId = 1, // Linked to Regiment 1
                FirstName = "Michael",
                LastName = "Johnson",
                DateOfBirth = DateTime.Now.AddYears(-28),
                PermanentAddress = "789 Pine St, City, Country",
                TypeOfPersonnel = "Regular"
            },
            new Personnel
            {
                PersId = 4,
                PersNo = "P004",
                CoyId = 2,
                RankId = 2,
                RegimentId = 2, // Linked to Regiment 2
                FirstName = "Emily",
                LastName = "Williams",
                DateOfBirth = DateTime.Now.AddYears(-32),
                PermanentAddress = "101 Maple St, City, Country",
                TypeOfPersonnel = "Regular"
            }
        );
    }
}
