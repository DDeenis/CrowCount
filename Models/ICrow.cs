using System.Collections.Generic;

namespace CrowCount.Models
{
    public interface ICrow
    {
        int Age { get; set; }
        string Color { get; }
        bool IsHungry { get; }
        bool IsDead { get; }
        int DeathChance { get; }

        bool LiveDay();
    }
}