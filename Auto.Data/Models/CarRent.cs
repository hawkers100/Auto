using System;
using System.ComponentModel.DataAnnotations;
using Auto.Common;

namespace Auto.Data.Models
{
    public class CarRent
    {
        public int Id { get; set; }

        [Required]
        [StringLength(Constants.CarNumberLength)]
        public string CarNumber { get; set; }

        [Required]
        [StringLength(Constants.RentLocationLength)]
        public string RentLocation { get; set; }

        [Required]
        public DateTime RentTime { get; set; }

        [StringLength(Constants.RentLocationLength)]
        public string ReturnLocation { get; set; }

        public DateTime? ReturnTime { get; set; }

        public decimal? Price { get; set; }
    }
}
