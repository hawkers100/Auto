using System;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using Auto.Data.Models;

namespace Auto.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            if (!context.CarRents.Any())
            {
                context.CarRents.AddOrUpdate(new CarRent { CarNumber = "к001мн", RentLocation = "пр. Ленина, 1", RentTime = DateTime.Now.AddHours(-3).AddMinutes(20) });
                context.CarRents.AddOrUpdate(new CarRent { CarNumber = "с200кл", RentLocation = "пр. Трудящихся, 10", RentTime = DateTime.Now.AddHours(-2).AddMinutes(11) });
                context.CarRents.AddOrUpdate(new CarRent { CarNumber = "п300кс", RentLocation = "ул. Чернышевского, 52", RentTime = new DateTime(2018, 5, 20, 5, 0, 0, 0),
                                                           ReturnLocation = "ул. Севастопольская, 23", ReturnTime = new DateTime(2018, 5, 20, 7, 0, 0, 0), Price = 200 });
                context.SaveChanges();
            }
        }
    }
}
