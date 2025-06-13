using System;
using System.ComponentModel.DataAnnotations;

namespace EquityScheduleCalculator.Models
{
    public class EquityScheduleEntry
    {
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public double Balance { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }

        public int Term { get; set; }

        public PaymentInfo PaymentInfo { get; set; } = new PaymentInfo();
    }
}