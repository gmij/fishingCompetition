using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using fishingScore.Models;

namespace fishingScore.Persistence
{
	public class FishingContext : DbContext
	{
		public FishingContext()
			: base("name=fishing")
		{
		}

		public virtual DbSet<Competition> Competitions { get; set; }

        public virtual DbSet<Contestant> Contestants { get; set; }

        public virtual DbSet<RoundScore> RoundScores { get; set; }
	    public virtual  DbSet<CompetitionResult> CompetitionResults { get; set; }

	    protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Competition>()
				.Property(e => e.Id)
				.IsUnicode(false);
		    modelBuilder.Entity<Contestant>()
		        .Property(e => e.Id);
		    modelBuilder.Entity<RoundScore>()
		        .Property(e => e.Id)
                ;
            
		}


	    public override Task<int> SaveChangesAsync()
	    {
            try
            {
                return base.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                string msg = "";
                foreach (var error in ex.EntityValidationErrors)
                {
                    msg += string.Join("\r\n", error.ValidationErrors.Select(err => err.ErrorMessage));
                }
                throw new ArgumentException(msg);
            }
            
	    }

	    public override int SaveChanges()
	    {
	        try
	        {
	            return base.SaveChanges();
	        }
	        catch (DbEntityValidationException ex)
	        {
	            string msg = "";
	            foreach (var error in ex.EntityValidationErrors)
	            {
	                msg += string.Join("\r\n", error.ValidationErrors.Select(err => err.ErrorMessage));
	            }
                throw new ArgumentException(msg);
	        }

	    }
	}
    [Table("CompetitionResult")]
    public class CompetitionResult
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("D");

        public string CompetitionId { get; set; }

        public string ContestantId { get; set; }

        [ForeignKey("ContestantId")]
        public Contestant Contestant { get; set; }

        [ForeignKey("CompetitionId")]
        public Competition Competition { get; set; }

        public int Order { get; set; }
    }
}