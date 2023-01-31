using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagerDal.ViewModels
{
    public class TimeSheetFilterVm
    {
        public string? UserCreatedEmail { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? EndDateFrom { get; set; }
        public long TaskId { get; set; }
        public long ProjectId { get; set; }
        public string TaskType { get; set; }
    }
}
