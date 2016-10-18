using System;
using System.ComponentModel;

namespace fishingScore.Models
{
	public class FishingViewModel
	{

		public string Title { get; set; }

	    public string Time { get; set; } = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        
	}

    public class ContestantViewModel
    {
        public string Name { get; set; }

        public string Number { get; set; }

        public string GroupNum { get; set; }

        public string Id { get; set; }
    }


    public class RoundScoreViewModel
    {

        
        public string Id { get; set; } 
        
        public ContestantViewModel Contestant { get; set; }

        public float Result { get; set; }

        public float Score { get; set; }
    }

    public class RoundScorePostViewModel
    {
        public string Id { get; set; }

        public string GroupNum { get; set; }

        public float Result { get; set; }
    }
}