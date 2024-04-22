using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kreta.Shared.Parameters;

namespace Kreta.Shared.Models
{
    public class NameBirthDay : FullNameParameter
    {
        public DateTime? Birthday { get; set; }
    }
}
