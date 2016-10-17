using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;

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
}