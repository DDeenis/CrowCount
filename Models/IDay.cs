using System;
using System.Collections.Generic;
using CrowCount.Enums;

namespace CrowCount.Models
{
    public interface IDay
    {
        IList<ICrow> Crows { get; }
        int CrowsCount { get; }
        int Temperature { get; set; }
        string Crop { get; set; }
        PartOfDay PartOfDay { get; set; }
        Weather Weather { get; set; }
        TimeSpan TimeStamp { get; set; }
    }
}