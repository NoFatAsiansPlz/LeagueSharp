using System;
using System.Collections.Generic;

namespace LeagueSharp.Common
{
    public class AutoLevel
    {
        private static List<SpellSlot> order = new List<SpellSlot>();
        private static float LastLeveled;
        private static float NextDelay;
        private static readonly Obj_AI_Hero Player = ObjectManager.Player;
        private static Random RandomNumber;

        public AutoLevel(IEnumerable<int> levels)
        {
            foreach (var level in levels)
            {
                order.Add((SpellSlot) (level - 1));
            }
            RandomNumber = new Random(Environment.TickCount);
            Game.OnGameUpdate += Game_OnGameUpdate;
        }

        public AutoLevel(List<SpellSlot> levels)
        {
            order = levels;
            RandomNumber = new Random(Environment.TickCount);
            Game.OnGameUpdate += Game_OnGameUpdate;
        }

        private static void Game_OnGameUpdate(EventArgs args)
        {
            if (Player.SpellTrainingPoints < 1 || Environment.TickCount - LastLeveled < NextDelay)
            {
                return;
            }

            NextDelay = RandomNumber.Next(750);
            LastLeveled = Environment.TickCount;
            var spell = order[GetTotalPoints()];
            Player.Spellbook.LevelSpell(spell);
        }

        private static int GetTotalPoints()
        {
            var spell = Player.Spellbook;
            var q = spell.GetSpell(SpellSlot.Q).Level;
            var w = spell.GetSpell(SpellSlot.W).Level;
            var e = spell.GetSpell(SpellSlot.E).Level;
            var r = spell.GetSpell(SpellSlot.R).Level;

            return q + w + e + r;
        }

        public static void Enabled(bool enabled)
        {
            if (enabled)
            {
                Game.OnGameUpdate += Game_OnGameUpdate;
            }
            else
            {
                Game.OnGameUpdate -= Game_OnGameUpdate;
            }
        }
    }
}