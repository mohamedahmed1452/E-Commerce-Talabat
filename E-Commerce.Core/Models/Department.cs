using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Models
{
    public class Department:ProductBase
    {
        public string Name { get; set; }
        public DateOnly DateOfCreation { get; set; }
    }
}
