using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Entities.SQL
{
    public class SQLSampleEntity : EntityBase
    {
        [StringLength(100)]
        public string SampleField { set; get; }
    }
}
