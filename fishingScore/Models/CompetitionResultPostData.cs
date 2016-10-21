using System;

namespace fishingScore.Models
{
    public class CompetitionResultPostData
    {
        public string Id { get; set; }

        public string CompetitionId { get; set; }

        public string ContestantId { get; set; }

        public int Order { get; set; }

    }
}