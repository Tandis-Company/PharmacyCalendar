﻿using PharmacyCalendar.Domain.AggregatesModel.GroupAggregate;
using PharmacyCalendar.Domain.AggregatesModel.GroupAggregate.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyCalendar.Infrastructure.Repositories
{
    public class TechnicalOfficerRepository : BaseRepository<TechnicalOfficer>, ITechnicalOfficerRepository
    {
        public TechnicalOfficerRepository(PharmacyCalendarDbContext dbContext) : base(dbContext)
        {
        }
    }
}
