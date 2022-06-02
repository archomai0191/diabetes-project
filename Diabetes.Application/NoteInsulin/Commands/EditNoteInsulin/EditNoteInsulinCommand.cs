﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Diabetes.Application.NoteInsulin.Commands.CreateNoteInsulin
{
    public class EditNoteInsulinCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public double InsulinValue { get; set; }
        public DateTime MeasuringDateTime { get; set; }
        public string InsulinType { get; set; }
        public string Comment { get; set; }
    }
}
