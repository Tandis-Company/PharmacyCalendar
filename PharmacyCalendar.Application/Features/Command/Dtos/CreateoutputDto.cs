using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyCalendar.Application.Features.Command.Dtos
{
    public class CreateoutputDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string NationalCode { get; set; }
    }
}
