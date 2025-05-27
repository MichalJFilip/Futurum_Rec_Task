using Microsoft.EntityFrameworkCore;

namespace Recruitment_Task
{
    public class CampaignContext : DbContext
    {
        public DbSet<Campaign> Campaigns { get; set; }
        public CampaignContext(DbContextOptions<CampaignContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Campaign>()
                .Ignore(c => c.Keywords);

            base.OnModelCreating(modelBuilder);
        }

    }
}