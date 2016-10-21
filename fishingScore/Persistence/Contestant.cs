using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fishingScore.Persistence
{
    /// <summary>
    ///     选手表
    /// </summary>
    [Table("Contestant")]
    public class Contestant
    {
        // Primary key 
        [Key]
        [Required]
        [StringLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString("D");

        [Display(Name = "姓名")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "编号")]
        [Required]
        public string Number { get; set; }

        [Display(Name = "所在小组")]
        [Required]
        public string GroupNum { get; set; }

        [Required]
        public string CompetitionId { get; set; }
    }
}