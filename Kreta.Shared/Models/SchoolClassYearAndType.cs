using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kreta.Shared.Models
{
    public class SchoolClassYearAndType
    {
        public int SchoolYear { get; set; } = -1;
        public SchoolClassType SchoolClassType { get; set; } = SchoolClassType.ClassA;
    }
}
