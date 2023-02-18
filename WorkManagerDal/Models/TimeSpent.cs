using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagerDal.Models
{
    public class TimeSpent : BaseEntity
    {
        public User? UserCreated { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? Comment { get; set; }
        public DateTime? DateFrom { get; set; }
        public double HoursCount { get; set; }
        public Feature? Feature { get; set; }
        public Bug? Bug { get; set; }
    }
}
