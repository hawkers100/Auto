using System;
using System.ComponentModel.DataAnnotations;
using Auto.Common;

namespace Auto.Web.Models
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

        public static CarRent FromDataModel(Data.Models.CarRent carRent)
        {
            return new CarRent
            {
                Id = carRent.Id,
                CarNumber = carRent.CarNumber,
                RentLocation = carRent.RentLocation,
                RentTime = carRent.RentTime,
                ReturnLocation = carRent.ReturnLocation,
                ReturnTime = carRent.ReturnTime,
                Price = carRent.Price
            };
        }

        public Data.Models.CarRent ToDataModel()
        {
            var dataModel = new Data.Models.CarRent { Id = Id };
            FillDataModel(dataModel);
            return dataModel;
        }

        public void FillDataModel(Data.Models.CarRent carRentDataModel)
        {
            carRentDataModel.CarNumber = CarNumber;
            carRentDataModel.RentLocation = RentLocation;
            carRentDataModel.RentTime = RentTime;
            carRentDataModel.ReturnLocation = ReturnLocation;
            carRentDataModel.ReturnTime = ReturnTime;
            carRentDataModel.Price = Price;
        }
    }
}
