using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetIdentiy.Models
{
    public class BaseEntity
    {
        public int AddBy { get; set; }
        public DateTime AddDate { get; set; }
    }
}
