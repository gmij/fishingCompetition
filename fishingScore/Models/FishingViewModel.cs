using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace fishingScore.Models
{
	public class FishingViewModel
	{
        [Display(Name = "比赛名称")]
        public string Title { get; set; }
        [Display(Name = "比赛时间")]
        public string Time { get; set; } = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        
	}
}