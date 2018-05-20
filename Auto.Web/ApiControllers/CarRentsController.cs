using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Auto.Data;
using Auto.Tools;

namespace Auto.Web.ApiControllers
{
    // Errors logged via filters
    public class CarRentsController : ApiController
    {
        public IHttpActionResult Get()
        {
            var db = new DataContext();
            var courses = db.CarRents.Select(Models.CarRent.FromDataModel).ToList();
            return Ok(courses);
        }

        public IHttpActionResult Put(Models.CarRent carRent)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (!ValidateCarRent(carRent))
                return BadRequest();
            var db = new DataContext();
            var carRentEntity = db.CarRents.Find(carRent.Id);
            if (carRentEntity == null)
                return NotFound();
            carRent.Price = RentPriceTool.CalculatePrice(carRent.RentTime, carRent.ReturnTime);
            carRent.FillDataModel(carRentEntity);
            db.Entry(carRentEntity).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(Models.CarRent.FromDataModel(carRentEntity));
        }

        public IHttpActionResult Post(Models.CarRent carRent)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (!ValidateCarRent(carRent))
                return BadRequest();
            carRent.Price = RentPriceTool.CalculatePrice(carRent.RentTime, carRent.ReturnTime);
            var carRentEntity = carRent.ToDataModel();
            var db = new DataContext();
            db.CarRents.Add(carRentEntity);
            db.SaveChanges();
            return Ok(Models.CarRent.FromDataModel(carRentEntity));
        }

        private bool ValidateCarRent(Models.CarRent carRent)
        {
            if (carRent.RentTime >= carRent.ReturnTime)
                return false;
            if ((carRent.ReturnTime != null && string.IsNullOrWhiteSpace(carRent.ReturnLocation))
                || (carRent.ReturnTime == null && !string.IsNullOrWhiteSpace(carRent.ReturnLocation)))
                return false;
            return true;
        }
    }
}