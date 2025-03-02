using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Framework.Contracts;

namespace PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Contracts
{
    public interface ITechnicalOfficerRepository : IWriteRepository<TechnicalOfficer>, IReadRepository<TechnicalOfficer>
    {

    }
}
