using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fishingScore.Persistence
{
    [Table("RoundScore")]
    public class RoundScore
    {
        public RoundScore()
        {
        }

        public RoundScore(int round, Contestant contestant)
        {
            CompetitionId = contestant.CompetitionId;
            ContestantId = contestant.Id;
            Round = round;
            Contestant = contestant;
        }

        // Primary key 
        public string Id { get; set; } = Guid.NewGuid().ToString("D");

        [ForeignKey("Competition")]
        public string CompetitionId { get; set; }

        public virtual Competition Competition { get; set; }

        [Display(Name = "回合")]
        public int Round { get; set; }

        public virtual Contestant Contestant { get; set; }

        [ForeignKey("Contestant")]
        public string ContestantId { get; set; }

        [Column(TypeName = "FLOAT")]
        [Display(Name = "成绩")]
        public float? Result { get; set; }

        [Column(TypeName = "FLOAT")]
        [Display(Name = "得分")]
        public float? Score { get; set; }
    }
}