using System;

namespace Solaris.BlockExplorer.Domain.Models
{
    public class DifficultyData
    {
        public DateTime Time { get; set; }
        public decimal Difficulty { get; set; }
        public long Height { get; set; }
    }
}
