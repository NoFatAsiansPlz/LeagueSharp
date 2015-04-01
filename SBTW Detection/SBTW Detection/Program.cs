using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace SBTWDetection
{
    class Program
    {
        private static readonly List<PathUpdate> Players = new List<PathUpdate>();
        private static Menu _config;

        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += eventArgs =>
            {
                foreach (var hero in ObjectManager.Get<Obj_AI_Hero>().Where(h => !h.IsMe))
                {
                    Players.Add(new PathUpdate(hero));
                }

                _config = new Menu("SBTW-Detection", "SBTW-Detection", true);
                _config.AddItem(new MenuItem("rate", "Detection Rate").SetValue(new Slider(130, 50, 300)));
                _config.AddToMainMenu();

                Game.PrintChat("<font color='#15C3AC'>SBTW-Detection</font> <font color='#FFFFFF'>by h3h3 loaded.</font>");
            };
        }

        private class PathUpdate
        {
            private Obj_AI_Hero Hero { get; set; }
            private Vector3 LastPath { get; set; }
            private int LastTick { get; set; }
            private bool Active { get; set; }
            private Render.Text Text { get; set; }

            public PathUpdate(Obj_AI_Hero hero)
            {
                Hero = hero;
                Text = new Render.Text("SBTW", Hero, new Vector2(145, 5), 20, new ColorBGRA(255, 255, 255, 255)) { OutLined = true };
                Text.VisibleCondition += sender => Active;
                Text.Add();

                Game.OnGameUpdate += Update;
            }

            private void Update(EventArgs args)
            {
                if (Hero.Path.Length <= 0 || Hero.Path.Last() == LastPath)
                    return;

                Active = Environment.TickCount - LastTick < _config.Item("rate").GetValue<Slider>().Value && Render.OnScreen(Drawing.WorldToScreen(Hero.Position));
                LastTick = Environment.TickCount;
                LastPath = Hero.Path.Last();
            }
        }
    }
}
