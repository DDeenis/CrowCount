using System;
using System.Collections.Generic;
using System.Linq;
using CrowCount.Models;

namespace CrowCount
{
    public class CrowPool
    {
        private const int maxCrowsCount = 40;
        private const int minCrowsCount = 15;
        private readonly Random random = new Random();

        public CrowPool()
        {
            CrowsCount = new Random().Next(minCrowsCount, maxCrowsCount);

            Crows = new List<ICrow>();

            for (int i = 0; i < CrowsCount; i++)
            {
                Crows.Add(new Crow());
            }
        }

        public CrowPool(int crowsCount) : this()
        {
            CrowsCount = crowsCount < maxCrowsCount && crowsCount > minCrowsCount ?
                crowsCount :
                throw new ArgumentException($"Min {minCrowsCount} crows and max {maxCrowsCount} crows", nameof(crowsCount));
        }

        public IList<ICrow> Crows { get; private set; }
        public int CrowsCount { get; init; }

        public IList<ICrow> GetCrows(IList<ICrow> crows)
        {
            foreach (var crow in crows)
            {
                crow.LiveDay();
            }

            var crowList = crows.Where(c => c.IsDead == false).Take(random.Next(crows.Count) + 1).ToList();

            return crowList;
        }
    }
}