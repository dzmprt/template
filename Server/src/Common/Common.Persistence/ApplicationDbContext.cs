using System.Reflection;
using Common.Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using UM.Domain;
using VS.Domain;
using VS.Domain.FC;

namespace Common.Persistence;

internal sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    #region UM

    internal DbSet<ApplicationUser> ApplicationUsers { get; }

    internal DbSet<ApplicationUserRole> ApplicationUserRoles { get; }

    #endregion

    #region VS

    internal DbSet<BlobFile> BlobFiles { get; set; }

    internal DbSet<Image> Images { get; set; }
    
    // FC
    internal DbSet<Vote> Votes { get; set; }

    internal DbSet<VotesSet> VotesSet { get; set; }

    internal DbSet<Contest> Contests { get; set; }

    internal DbSet<ContestCategory> ContestCategory { get; set; }

    internal DbSet<Participant> Participants { get; set; }

    internal DbSet<Ticket> Tickets { get; set; }

    internal DbSet<ParticipantImage> ParticipantImages { get; set; }

    #endregion

    
    #region Ef

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //TODO: Refactoring
        #region Legacy

        modelBuilder.Entity<ParticipantImage>()
            .HasOne(e => e.Participant)
            .WithMany(e => e.ParticipantImages)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ParticipantImage>().HasKey(_ => new { _.ImageId, _.ParticipantId });

        modelBuilder.Entity<Contest>().Navigation(c => c.Owner).AutoInclude();
        modelBuilder.Entity<Contest>().Navigation(c => c.ContestCategories).AutoInclude();
        modelBuilder.Entity<ContestCategory>().Navigation(c => c.Participants).AutoInclude();
        
        modelBuilder
            .Entity<Participant>()
            .HasMany(e => e.ParticipantImages)
            .WithOne(e => e.Participant)
            .OnDelete(DeleteBehavior.ClientCascade);
        
        modelBuilder.Entity<Participant>().Navigation(c => c.Votes).AutoInclude();
        modelBuilder.Entity<Participant>().Navigation(c => c.Image).AutoInclude();
        modelBuilder.Entity<Participant>().Navigation(c => c.ParticipantImages).AutoInclude();
        modelBuilder.Entity<ParticipantImage>().Navigation(c => c.Image).AutoInclude();

        modelBuilder.Entity<Vote>().Navigation(c => c.VotesSet).AutoInclude();
        modelBuilder.Entity<VotesSet>().Navigation(c => c.Votes).AutoInclude();

        #endregion

        
        base.OnModelCreating(modelBuilder);
    }


    public async Task<IContextTransaction> BeginTransaction(CancellationToken cancellationToken)
    {
        return new ContextTransaction(await Database.BeginTransactionAsync(cancellationToken));
    }

    #endregion
}