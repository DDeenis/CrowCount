using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CrowCount.Models;

namespace CrowCount
{
    public class CrowCounter
    {
        public CrowCounter(int days = 31, int timeoutSeconds = 1)
        {
            DaysCount = days;
            TimeoutSeconds = timeoutSeconds;
            Crops = new List<string>
            {
                "cucumbers", "wheat", "corn", "cotton", "rice", "soybean", "peanuts", "sunflowers", "lentil", "tobacco", "shugar beet"
            };
            Days = new List<IDay>();
        }

        public int DaysCount { get; set; }
        public int TimeoutSeconds { get; }
        public IList<IDay> Days { get; private set; }
        public IList<string> Crops { get; set; }

        private readonly Random random = new Random();
        private int nextDay = 0;

        // main method
        public void StartCount()
        {
            foreach (var day in Day.CreateRandomDays(DaysCount))
            {
                StringBuilder str = new StringBuilder(day.ToString());

                day.Crop = Crops[random.Next(Crops.Count)];

                str.Insert(0, $"Today you visited a fields, where {day.Crop} was growing.\n");
                str.Insert(0, $"Day {++nextDay}.\n");

                Console.WriteLine(str);

                Days.Add(day);

                Thread.Sleep(TimeoutSeconds * 1000);
            }

            int minCrows = Days[0].CrowsCount;
            int minElementIndex = 0;

            int maxCrows = Days[0].CrowsCount;
            int maxElementIndex = 0;

            for (int i = 0; i < DaysCount; i++)
            {
                if(Days[i].CrowsCount > maxCrows)
                {
                    maxCrows = Days[i].CrowsCount;
                    maxElementIndex = i;
                }

                if(Days[i].CrowsCount < minCrows)
                {
                    minCrows = Days[i].CrowsCount;
                    minElementIndex = i;
                }
            }

            StringBuilder result = new StringBuilder();

            var min = Days[minElementIndex];
            var max = Days[maxElementIndex];

            result.AppendLine($"Looks like most crows ({max.CrowsCount}) like {max.Crop} fields at {max.Weather} days, when it's {max.PartOfDay} and temperature is {max.Temperature} in time of {max.TimeStamp}");
            result.AppendLine($"And the least crows ({min.CrowsCount}) like {min.Crop} fields at {min.Weather} days, when it's {min.PartOfDay} and temperature is {min.Temperature} in time of {min.TimeStamp}");

            Console.WriteLine(result);
        }
    }
}