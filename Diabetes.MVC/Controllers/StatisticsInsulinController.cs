﻿using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Diabetes.MVC.Models;
using Diabetes.Application.Statistics.Commands;
using System.Linq;
using System.Globalization;
using Diabetes.Domain.Enums;
using System;
using System.Threading.Tasks;
using Diabetes.MVC.Extensions;

namespace Diabetes.MVC.Controllers
{
    public class StatisticsInsulinController : Controller
    {
        private readonly IMediator _mediator;

        public StatisticsInsulinController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var viewModel = new StatisticsInsulinViewModel
            {
                ReturnUrl = returnUrl,
            };

            Func<InsulinType, bool> IFilter = a => true;

            Func<DateTime, bool> DFilter = a => (DateTime.Now.Date - a.Date).Days < 7 && (DateTime.Now.Date - a.Date).Days >= 0;

            var command = new GetInsulinCommand
            {
                InsulinFilter = IFilter,
                DateFilter = DFilter
            };

            var itemsList = await _mediator.Send(command);
            itemsList = itemsList.Where(a => a.UserId == User.GetId()).ToList();

            viewModel.Categorical = itemsList.Select(a => a.MeasuringDateTime.ToString("dd.MM.yyyy HH:mm")).ToList();
            viewModel.Values = itemsList.Select(a => Math.Round(a.Value.Value, 2)).ToList();
            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> InsulinGraphics(StatisticsInsulinViewModel viewModel)
        {
            if (viewModel.CustomDate == null) viewModel.CustomDate = DateTime.Now.ToString("yyyy-MM-dd");
            Func<InsulinType, bool> IFilter = viewModel.InsulinAdditional switch
            {
                "1" => a => a == InsulinType.Short,
                "2" => a => a == InsulinType.Long,
                _ => a => true,
            };

            Func<DateTime, bool> DFilter = viewModel.InsulinTimePeriod switch
            {
                "1" => a => (DateTime.Now.Year - a.Year) * 12 +
                DateTime.Now.Month - a.Month +
                (DateTime.Now.Day >= a.Day ? 0 : -1) == 0,
                "2" => a => a.Date == DateTime.Now.Date,
                "3" => a => a.Date == DateTime.ParseExact(viewModel.CustomDate, "yyyy-MM-dd",
                CultureInfo.InvariantCulture).Date,
                "4" => a => true,
                _ => a => (DateTime.Now.Date - a.Date).Days < 7 && (DateTime.Now.Date - a.Date).Days >= 0,
            };

            var command = new GetInsulinCommand
            {
                InsulinFilter = IFilter,
                DateFilter = DFilter
            };

            var itemsList = await _mediator.Send(command);
            itemsList = itemsList.Where(a => a.UserId == User.GetId()).ToList();

            viewModel.Categorical = itemsList.Select(a => a.MeasuringDateTime.ToString("dd.MM.yyyy HH:mm")).ToList();
            viewModel.Values = itemsList.Select(a => Math.Round(a.Value.Value, 2)).ToList();
            return View(viewModel);
        }
    }
}