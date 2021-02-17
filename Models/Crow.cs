using System;

namespace CrowCount.Models
{
    public class Crow : ICrow
    {
        int deathChance = 5 + new Random().Next(15);
        const int maxCrowAge = 10;
        const int minCrowAge = 1;

        public Crow()
        {
            var random = new Random();

            Color = random.Next(20) == 0 ? "white" : "black";
            Age = random.Next(minCrowAge);
            IsHungry = random.Next(9) < 2 ? true : false;
        }

        public int Age { get; set; }
        public string Color { get; private set; }
        public bool IsHungry { get; private set; }
        public bool IsDead { get; private set; }
        public int DeathChance => IsHungry ? deathChance * 2 : deathChance;

        public bool LiveDay()
        {
            var random = new Random();

            IsDead = random.Next(DeathChance * 5) - DeathChance < DeathChance ? true : false;

            return IsDead;
        }
    }
}