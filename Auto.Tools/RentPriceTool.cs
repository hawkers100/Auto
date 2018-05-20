using System;

namespace Auto.Tools
{
    public static class RentPriceTool
    {
        public const decimal Tariff1PerSecond = 100M / 3600M;
        public const decimal Tariff2PerSecond = 200M / 3600M;

        public static int? CalculatePrice(DateTime rentTime, DateTime? returnTime)
        {
            if (returnTime == null)
                return null;

            var secondsOverHour = (decimal)Math.Round((returnTime.Value - rentTime).TotalSeconds) % 3600M;
            if (secondsOverHour != 0)
            {
                returnTime = returnTime.Value.AddSeconds(3600 - (double) secondsOverHour);
            }

            var tariffs = new[]
            {
                new { PeriodEndMark = TimeSpan.FromHours(7), Tariff = Tariff1PerSecond }, new { PeriodEndMark = TimeSpan.FromHours(11), Tariff = Tariff2PerSecond },
                new { PeriodEndMark = TimeSpan.FromHours(17), Tariff = Tariff1PerSecond }, new { PeriodEndMark = TimeSpan.FromHours(20), Tariff = Tariff2PerSecond }
            };
            var price = 0M;
            var date = rentTime;
            var dayStart = date.Date;
            var nextMark = dayStart;

            while (true)
            {
                var breakFromCycle = false;

                foreach (var tariff in tariffs)
                {
                    var periodStart = rentTime > nextMark ? rentTime : nextMark;
                    nextMark = dayStart.Add(tariff.PeriodEndMark);

                    if (rentTime >= nextMark)
                        continue;

                    var periodEnd = returnTime > nextMark ? nextMark : returnTime;
                    var period = periodEnd.Value - periodStart;
                    price += (decimal) period.TotalSeconds * tariff.Tariff;

                    if (returnTime == periodEnd)
                    {
                        breakFromCycle = true;
                        break;
                    }
                }

                if (breakFromCycle)
                    break;

                dayStart = dayStart.AddDays(1);
            }

            return (int)Math.Round(price);
        }
    }
}
