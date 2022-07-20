using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WeightTrack.Data.DbModels
{
    public class WeightEntry
    {
        [Key]
        public int Id { get; set; }
        public double Weight { get; set; }
        public DateTime EntryDate { get; set; }
        [Comment("An optional description of the weight entry")]
        public string? Note { get; set; }
    }
}
