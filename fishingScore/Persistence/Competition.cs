using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace fishingScore.Persistence
{
    /// <summary>
    ///     比赛表
    /// </summary>
    [Table("competition")]
    public class Competition
    {
        // Primary key 
        [StringLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString("D");

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Column(TypeName = "date")]
        public DateTime Time { get; set; }
    }

    /// <summary>
    ///     选手表
    /// </summary>
    [Table("Contestant")]
    public class Contestant
    {
        // Primary key 
        [Required]
        [StringLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString("D");

        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string GroupNum { get; set; }

        [Required]
        public string CompetitionId { get; set; }
    }

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


        public string Id { get; set; } = Guid.NewGuid().ToString("D");

        public string CompetitionId { get; set; }

        public int Round { get; set; }


        public Contestant Contestant { get; }


        public string ContestantId { get; set; }


        public float Result { get; set; }

        public float Score { get; set; }
    }


    public class ContestantCompetitionScore
    {
        public ContestantCompetitionScore(params RoundScore[] scores)
        {
            foreach (var score in scores)
                RoundScores.Add(score);
        }

        public IList<RoundScore> RoundScores { get; set; } = new List<RoundScore>();

        public float TotalScore
        {
            get { return RoundScores.Sum(s => s.Score); }
        }

        public float TotalResult => RoundScores.Sum(s => s.Result);

        public int Order { get; set; }

        public string Name => RoundScores.First().Id;
    }
}