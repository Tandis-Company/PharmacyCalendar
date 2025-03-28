﻿using PharmacyCalendar.Application.Features.Command;
using PharmacyCalendar.Domain.AggregatesModel.TechnicalOfficerAggregate.Enums;

namespace PharmacyCalendar.Application.Features.Dtos.WorkShiftDto
{
    public class CreateWorkShiftDto
    {
        public WorkShift WorkShift { get; set; }
        public Weekdays Weekdays { get; set; }
        public Guid TechnicalOfficerId { get; set; }
        public string? FullName { get; set; }
    }
}
