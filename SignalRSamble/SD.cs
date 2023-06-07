﻿namespace SignalRSamble
{
    public static class SD
    {
        static SD()
        {
            DeathlyHallowRace = new Dictionary<string, int>();
            DeathlyHallowRace.Add(Wand, 0);
            DeathlyHallowRace.Add(Cloak, 0);
            DeathlyHallowRace.Add(Stone, 0);
        }
        public const string Wand = "wand";
        public const string Cloak = "cloak";
        public const string Stone = "stone";

        public static Dictionary<string, int> DeathlyHallowRace;
    }
}
