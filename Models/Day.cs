using System;
using System.Collections.Generic;
using System.Text;
using CrowCount.Enums;

namespace CrowCount.Models
{
    public class Day : IDay
    {
        #region Constants
        private const int minDays = 1;
        private const int maxCrowsCount = 40;
        private const int minCrowsCount = 0;
        private const int maxTemperature = 35;
        private const int minTemperature = -20;
        private const int morning = 7;
        private const int noon = 14;
        private const int afternoon = 17;
        private const int evening = 21;
        private const int night = 24;
        #endregion

        #region Readonly fields
        // private readonly int maxMinutex = (int) TimeSpan.FromDays(1).TotalMinutes;
        // private readonly Random random = new Random(DateTimeOffset.Now.TimeOfDay.Milliseconds);
        #endregion

        public Day()
        {

        }

        public Day(
            int crowsCount,
            int temperature,
            PartOfDay partOfDay,
            Weather weather,
            TimeSpan timeStamp)
        {
            CrowsCount = crowsCount;
            Temperature = temperature;
            PartOfDay = partOfDay;
            Weather = weather;
            TimeStamp = timeStamp;
        }

        #region Properties
        // unused
        public IList<ICrow> Crows { get; set; }

        public int CrowsCount { get; set; }
        public int Temperature { get; set; }
        public PartOfDay PartOfDay { get; set; }
        public Weather Weather { get; set; }
        public TimeSpan TimeStamp { get; set; }
        public string Crop { get; set; }
        #endregion

        private static TimeSpan GetRandomTimeSpan(PartOfDay partOfDay, Random random) => partOfDay switch
        {
            PartOfDay.Morning => TimeSpan.FromHours(0).Add(TimeSpan.FromMinutes(random.Next((int) TimeSpan.FromHours(morning).TotalMinutes))),

            PartOfDay.Noon => TimeSpan.FromHours(morning).Add(TimeSpan.FromMinutes(random.Next((int) (TimeSpan.FromHours(noon) - TimeSpan.FromHours(morning)).TotalMinutes))),

            PartOfDay.Afternoon => TimeSpan.FromHours(noon).Add(TimeSpan.FromMinutes(random.Next((int) (TimeSpan.FromHours(afternoon) - TimeSpan.FromHours(noon)).TotalMinutes))),

            PartOfDay.Evening => TimeSpan.FromHours(afternoon).Add(TimeSpan.FromMinutes(random.Next((int) (TimeSpan.FromHours(evening) - TimeSpan.FromHours(afternoon)).TotalMinutes))),

            PartOfDay.Night => TimeSpan.FromHours(evening).Add(TimeSpan.FromMinutes(random.Next((int) (TimeSpan.FromHours(night) - TimeSpan.FromHours(evening)).TotalMinutes))),

            _ => throw new ArgumentException(nameof(partOfDay))
        };

        private static IDay CreateRandomDay()
        {
            Random random = new Random();

            int crowsCount = random.Next(minCrowsCount, maxCrowsCount);
            int temperature = random.Next(minTemperature, maxTemperature);

            PartOfDay partOfDay = (PartOfDay) random.Next(Enum.GetValues<PartOfDay>().Length);
            Weather weather = (Weather) random.Next(Enum.GetValues<Weather>().Length);

            TimeSpan timeStamp = GetRandomTimeSpan(partOfDay, random);

            if(temperature < 0 && crowsCount + temperature >= 0)
            {
                crowsCount += temperature;
            }

            if(temperature > 0 && weather == Weather.Snowy)
            {
                temperature = random.Next(minTemperature, 0);
            }

            return new Day(crowsCount, temperature, partOfDay, weather, timeStamp);
        }

        public static IList<IDay> CreateRandomDays(int count)
        {
            if(count < minDays)
            {
                throw new ArgumentException($"Minimum {minDays} day", nameof(count));
            }

            IList<IDay> days = new List<IDay>();

            for (int i = 0; i < count; i++)
            {
                days.Add(CreateRandomDay());
            }

            return days;
        }

        public override string ToString()
        {
            StringBuilder str = new();

            str.AppendLine($"Today was a {Weather} day.");
            str.AppendLine($"You decided to visit field at {PartOfDay}, at {TimeStamp}.");
            str.AppendLine($"You noticed {CrowsCount} crows today.");
            str.AppendLine($"The temperature was {Temperature}");

            return str.ToString();
        }
    }
}