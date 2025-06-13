using EquityScheduleCalculator.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EquityScheduleCalculator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new EquityModel
            {
                ReservationDate = DateTime.Today
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CalculateEquity(EquityModel model)
        {
            if (ModelState.IsValid)
            {
                var schedule = new List<EquityScheduleEntry>();

                double currentRemainingBalance = model.SellingPrice;

                double principalPerPeriod = model.SellingPrice / model.Term;

                for (int i = 1; i <= model.Term; i++)
                {
                    double interestForThisPeriod;
                    double principalPaymentForThisPeriod;
                    double insuranceForThisPeriod;

                    if (i == model.Term)
                    {
  
                        principalPaymentForThisPeriod = 0;
                        interestForThisPeriod = 0;
                        insuranceForThisPeriod = 0;
                    }
                    else
                    {
                        interestForThisPeriod = (currentRemainingBalance - principalPerPeriod) * 0.05;
                        principalPaymentForThisPeriod = principalPerPeriod;
                        insuranceForThisPeriod = 0.01 * principalPaymentForThisPeriod;
                    }

                    double totalPaymentForThisPeriod = principalPaymentForThisPeriod + interestForThisPeriod + insuranceForThisPeriod;


                    currentRemainingBalance -= principalPaymentForThisPeriod;


                    if (i == model.Term)
                    {
                        currentRemainingBalance = 0;
                    }

                    schedule.Add(new EquityScheduleEntry
                    {
                        Balance = currentRemainingBalance,
                        DueDate = model.ReservationDate.AddMonths(i),
                        Term = i,
                        PaymentInfo = new PaymentInfo
                        {
                            Amount = principalPaymentForThisPeriod,
                            Interest = interestForThisPeriod,
                            Insurance = insuranceForThisPeriod,
                            Total = totalPaymentForThisPeriod
                        }
                    });
                }

                ViewBag.EquitySchedule = schedule;

                return View("Index", model);
            }

            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
