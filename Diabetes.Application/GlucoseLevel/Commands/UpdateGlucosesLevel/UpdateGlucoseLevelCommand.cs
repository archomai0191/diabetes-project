﻿using System;
using Diabetes.Domain.Normalized.Enums;
using MediatR;

namespace Diabetes.Application.GlucoseLevel.Commands.UpdateGlucosesLevel
{
    public class UpdateGlucoseLevelCommand:IRequest<Unit>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Comment { get; set; }
        public double ValueInMmol { get; set; }
        public MeasuringTimeType MeasuringTimeType { get; set; }
        public DateTime MeasuringDateTime { get; set; }
    }
}
