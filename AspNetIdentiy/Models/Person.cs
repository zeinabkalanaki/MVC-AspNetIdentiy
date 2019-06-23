using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetIdentiy.Models
{
    public class Person : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
