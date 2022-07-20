using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WeightTrack.Data.DbModels
{
    public class WeightTarget
    {
        [Key]
        public int Id { get; set; }
        public double TargetWeight { get; set; }
        public DateTime TargetDate { get; set; }
        [Comment("An optional description of the target entry")]
        public string? Note { get; set; }
        public int Active { get; set; }
    }
}
