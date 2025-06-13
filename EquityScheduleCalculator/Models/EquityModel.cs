using System;
using System.ComponentModel.DataAnnotations;

namespace EquityScheduleCalculator.Models
{
    public class EquityModel
    {
        [Required(ErrorMessage = "Selling Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Selling Price must be a positive number.")]
        [Display(Name = "Selling Price")]
        public double SellingPrice { get; set; }

        [Required(ErrorMessage = "Reservation Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Reservation Date")]
        public DateTime ReservationDate { get; set; }

        [Required(ErrorMessage = "Term is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Term must be at least 1.")]
        [Display(Name = "Term")]
        public int Term { get; set; }
    }
}