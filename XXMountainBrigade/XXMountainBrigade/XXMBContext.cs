using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using XXMountainBrigade.Models;

public class XXMBContext : DbContext
{
    public XXMBContext(DbContextOptions<XXMBContext> options) : base(options) { }

    public DbSet<Company> tblCompany { get; set; }
    public DbSet<Rank> tblRanks { get; set; }
    public DbSet<Personnel> tblPersonnel { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.ConfigureWarnings(warnings =>
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define primary keys
        modelBuilder.Entity<Company>().HasKey(c => c.CoyId);
        modelBuilder.Entity<Rank>().HasKey(r => r.RankId);
        modelBuilder.Entity<Personnel>().HasKey(p => p.PersId);

        // Configure relationships with cascade delete (Optional)
        modelBuilder.Entity<Personnel>()
            .HasOne(p => p.Company)
            .WithMany(c => c.Personnel)
            .HasForeignKey(p => p.CoyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Personnel>()
            .HasOne(p => p.Rank)
            .WithMany(r => r.Personnel)
            .HasForeignKey(p => p.RankId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed sample data for Companies and Ranks
        modelBuilder.Entity<Company>().HasData(
            new Company { CoyId = 1, CoyName = "Company A" },
            new Company { CoyId = 2, CoyName = "Company B" }
        );

        modelBuilder.Entity<Rank>().HasData(
            new Rank { RankId = 1, RanName = "Private" },
            new Rank { RankId = 2, RanName = "Sergeant" }
        );

        // Sample Personnel (ensure the CoyId and RankId match those in your data)
        modelBuilder.Entity<Personnel>().HasData(
            new Personnel
            {
                PersId = 1,
                PersNo = "P001",
                CoyId = 1, // Linked to Company A
                RankId = 1, // Linked to Private rank
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Now.AddYears(-30), // 30 years old
                PermanentAddress = "123 Main St, City, Country"
            },
            new Personnel
            {
                PersId = 2,
                PersNo = "P002",
                CoyId = 2, // Linked to Company B
                RankId = 2, // Linked to Sergeant rank
                FirstName = "Jane",
                LastName = "Smith",
                DateOfBirth = DateTime.Now.AddYears(-25), // 25 years old
                PermanentAddress = "456 Oak St, City, Country"
            },
            new Personnel
            {
                PersId = 3,
                PersNo = "P003",
                CoyId = 1, // Linked to Company A
                RankId = 1, // Linked to Private rank
                FirstName = "Michael",
                LastName = "Johnson",
                DateOfBirth = DateTime.Now.AddYears(-28), // 28 years old
                PermanentAddress = "789 Pine St, City, Country"
            },
            new Personnel
            {
                PersId = 4,
                PersNo = "P004",
                CoyId = 2, // Linked to Company B
                RankId = 2, // Linked to Sergeant rank
                FirstName = "Emily",
                LastName = "Williams",
                DateOfBirth = DateTime.Now.AddYears(-32), // 32 years old
                PermanentAddress = "101 Maple St, City, Country"
            }
        );
    }

}
