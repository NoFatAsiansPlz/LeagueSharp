using System;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using System.Drawing;
using System.Linq;

namespace SubirHechizos
{
    public class SubirHechizos
    {
        public static Menu _Menu;
        public static int sL = 0, qL = 0, wL = 0, eL = 0, rL = 0, qOff = 0, wOff = 0, eOff = 0, rOff = 0;
        public static int[] seq = new int[18];
        public static int[] abilitySequence;
        private static SpellSlot Smite;
        public static Obj_AI_Base Player = ObjectManager.Player;
        public static string tipo = "";
        public static string champion = "";
        public static string firstlevel = "";
        public static Boolean first = true;

        public static void AddToMenu(Menu menu)
        {
            _Menu = menu;

            livellini();
            champion = Player.BaseSkinName + tipo;

            menu = new Menu("LevelUp by Emin3m", "Emin3m`s AutoLevel", true);
            menu.AddItem(new MenuItem(champion + "Enabled", "Enabled").SetValue(true));
            menu.Item(champion + "Enabled").ValueChanged += Enabled_ValueChanged;
            menu.AddItem(new MenuItem(champion + "Level1", "Level 1").SetValue(new Slider(abilitySequence[0], 1, 4)));
            menu.Item(champion + "Level1").ValueChanged += Level1_ValueChanged;
            menu.AddItem(new MenuItem(champion + "Level2", "Level 2").SetValue(new Slider(abilitySequence[1], 1, 4)));
            menu.Item(champion + "Level2").ValueChanged += Level2_ValueChanged;
            menu.AddItem(new MenuItem(champion + "Level3", "Level 3").SetValue(new Slider(abilitySequence[2], 1, 4)));
            menu.Item(champion + "Level3").ValueChanged += Level3_ValueChanged;
            menu.AddItem(new MenuItem(champion + "Level4", "Level 4").SetValue(new Slider(abilitySequence[3], 1, 4)));
            menu.Item(champion + "Level4").ValueChanged += Level4_ValueChanged;
            menu.AddItem(new MenuItem(champion + "Level5", "Level 5").SetValue(new Slider(abilitySequence[4], 1, 4)));
            menu.Item(champion + "Level5").ValueChanged += Level5_ValueChanged;
            menu.AddItem(new MenuItem(champion + "Level6", "Level 6").SetValue(new Slider(abilitySequence[5], 1, 4)));
            menu.Item(champion + "Level6").ValueChanged += Level6_ValueChanged;
            menu.AddItem(new MenuItem(champion + "Level7", "Level 7").SetValue(new Slider(abilitySequence[6], 1, 4)));
            menu.Item(champion + "Level7").ValueChanged += Level7_ValueChanged;
            menu.AddItem(new MenuItem(champion + "Level8", "Level 8").SetValue(new Slider(abilitySequence[7], 1, 4)));
            menu.Item(champion + "Level8").ValueChanged += Level8_ValueChanged;
            menu.AddItem(new MenuItem(champion + "Level9", "Level 9").SetValue(new Slider(abilitySequence[8], 1, 4)));
            menu.Item(champion + "Level9").ValueChanged += Level9_ValueChanged;
            menu.AddItem(new MenuItem(champion + "Level10", "Level 10").SetValue(new Slider(abilitySequence[9], 1, 4)));
            menu.Item(champion + "Level10").ValueChanged += Level10_ValueChanged;
            menu.AddItem(new MenuItem(champion + "Level11", "Level 11").SetValue(new Slider(abilitySequence[10], 1, 4)));
            menu.Item(champion + "Level11").ValueChanged += Level11_ValueChanged;
            menu.AddItem(new MenuItem(champion + "Level12", "Level 12").SetValue(new Slider(abilitySequence[11], 1, 4)));
            menu.Item(champion + "Level12").ValueChanged += Level12_ValueChanged;
            menu.AddItem(new MenuItem(champion + "Level13", "Level 13").SetValue(new Slider(abilitySequence[12], 1, 4)));
            menu.Item(champion + "Level13").ValueChanged += Level13_ValueChanged;
            menu.AddItem(new MenuItem(champion + "Level14", "Level 14").SetValue(new Slider(abilitySequence[13], 1, 4)));
            menu.Item(champion + "Level14").ValueChanged += Level14_ValueChanged;
            menu.AddItem(new MenuItem(champion + "Level15", "Level 15").SetValue(new Slider(abilitySequence[14], 1, 4)));
            menu.Item(champion + "Level15").ValueChanged += Level15_ValueChanged;
            menu.AddItem(new MenuItem(champion + "Level16", "Level 16").SetValue(new Slider(abilitySequence[15], 1, 4)));
            menu.Item(champion + "Level16").ValueChanged += Level16_ValueChanged;
            menu.AddItem(new MenuItem(champion + "Level17", "Level 17").SetValue(new Slider(abilitySequence[16], 1, 4)));
            menu.Item(champion + "Level17").ValueChanged += Level17_ValueChanged;
            menu.AddItem(new MenuItem(champion + "Level18", "Level 18").SetValue(new Slider(abilitySequence[17], 1, 4)));
            menu.Item(champion + "Level18").ValueChanged += Level18_ValueChanged;

            menu.AddToMainMenu();
            seq = new[] { menu.Item(champion + "Level1").GetValue<Slider>().Value, menu.Item(champion + "Level2").GetValue<Slider>().Value, menu.Item(champion + "Level3").GetValue<Slider>().Value, menu.Item(champion + "Level4").GetValue<Slider>().Value, menu.Item(champion + "Level5").GetValue<Slider>().Value, menu.Item(champion + "Level6").GetValue<Slider>().Value, menu.Item(champion + "Level7").GetValue<Slider>().Value, menu.Item(champion + "Level8").GetValue<Slider>().Value, menu.Item(champion + "Level9").GetValue<Slider>().Value, menu.Item(champion + "Level10").GetValue<Slider>().Value, menu.Item(champion + "Level11").GetValue<Slider>().Value, menu.Item(champion + "Level12").GetValue<Slider>().Value, menu.Item(champion + "Level13").GetValue<Slider>().Value, menu.Item(champion + "Level14").GetValue<Slider>().Value, menu.Item(champion + "Level15").GetValue<Slider>().Value, menu.Item(champion + "Level16").GetValue<Slider>().Value, menu.Item(champion + "Level17").GetValue<Slider>().Value, menu.Item(champion + "Level18").GetValue<Slider>().Value };
            Game.PrintChat("[00:00] <font color='#C80046'>AutoLevelSpells by Emin3m loaded...</font>");
            Game.OnGameProcessPacket += Game_OnGameProcessPacket;

        }

        private static void Game_OnGameProcessPacket(EventArgs args)
        {
            qL = Player.Spellbook.GetSpell(SpellSlot.Q).Level;
            wL = Player.Spellbook.GetSpell(SpellSlot.W).Level;
            eL = Player.Spellbook.GetSpell(SpellSlot.E).Level;
            rL = Player.Spellbook.GetSpell(SpellSlot.R).Level;
            sL = qL + wL + eL + rL;
            if (_Menu.Item(champion + "Level1").GetValue<Slider>().Value == 1) firstlevel = "your Q at ";
            if (_Menu.Item(champion + "Level1").GetValue<Slider>().Value == 2) firstlevel = "your W at ";
            if (_Menu.Item(champion + "Level1").GetValue<Slider>().Value == 3) firstlevel = "your E at ";
            if (_Menu.Item(champion + "Level1").GetValue<Slider>().Value == 4) firstlevel = "your R at ";
            Drawing.OnDraw += Drawing_OnDraw;
            if (sL > 0 && first)
            {
                Drawing.OnDraw -= Drawing_OnDraw;

                changeSeq(0);
                first = false;
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (sL == 0) Drawing.DrawText(250, 10, System.Drawing.Color.White, "Please skill " + firstlevel + "Level 1 for your own." + sL);
        }

        private static void Enabled_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            AutoLevel.Enabled(e.GetNewValue<bool>());
        }

        private static void Level1_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[0] = e.GetNewValue<Slider>().Value;
            if (sL > 0) changeSeq(1);
        }

        private static void Level2_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[1] = e.GetNewValue<Slider>().Value;
            if (sL > 0) changeSeq(2);
        }

        private static void Level3_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[2] = e.GetNewValue<Slider>().Value;
            if (sL > 0) changeSeq(3);
        }

        private static void Level4_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[3] = e.GetNewValue<Slider>().Value;
            if (sL > 0) changeSeq(4);
        }

        private static void Level5_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[4] = e.GetNewValue<Slider>().Value;
            if (sL > 0) changeSeq(5);
        }

        private static void Level6_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[5] = e.GetNewValue<Slider>().Value;
            if (sL > 0) changeSeq(6);
        }

        private static void Level7_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[6] = e.GetNewValue<Slider>().Value;
            if (sL > 0) changeSeq(7);
        }

        private static void Level8_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[7] = e.GetNewValue<Slider>().Value;
            if (sL > 0) changeSeq(8);
        }

        private static void Level9_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[8] = e.GetNewValue<Slider>().Value;
            if (sL > 0) changeSeq(9);
        }

        private static void Level10_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[9] = e.GetNewValue<Slider>().Value;
            if (sL > 0) changeSeq(10);
        }

        private static void Level11_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[10] = e.GetNewValue<Slider>().Value;
            if (sL > 0) changeSeq(11);
        }

        private static void Level12_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[11] = e.GetNewValue<Slider>().Value;
            if (sL > 0) changeSeq(12);
        }

        private static void Level13_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[12] = e.GetNewValue<Slider>().Value;
            if (sL > 0) changeSeq(13);
        }

        private static void Level14_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[13] = e.GetNewValue<Slider>().Value;
            if (sL > 0) changeSeq(14);
        }

        private static void Level15_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[14] = e.GetNewValue<Slider>().Value;
            if (sL > 0) changeSeq(15);
        }

        private static void Level16_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[15] = e.GetNewValue<Slider>().Value;
            if (sL > 0) changeSeq(16);
        }

        private static void Level17_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[16] = e.GetNewValue<Slider>().Value;
            if (sL > 0) changeSeq(17);
        }

        private static void Level18_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            seq[17] = e.GetNewValue<Slider>().Value;
            if (sL > 0) changeSeq(18);
        }

        private static void changeSeq(int num)
        {
            AutoLevel.Enabled(false);
            var level = new AutoLevel(seq);
            AutoLevel.Enabled(_Menu.Item(champion + "Enabled").GetValue<bool>());
        }

        public static void livellini()
        {
            Smite = ObjectManager.Player.GetSpellSlot("SummonerSmite");
            if (Player.BaseSkinName == "Aatrox") abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Ahri") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Akali") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Alistar") abilitySequence = new int[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Amumu") abilitySequence = new int[] { 3, 1, 2, 3, 2, 4, 3, 3, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Anivia") abilitySequence = new int[] { 1, 3, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Annie")
            {
                if (ObjectManager.Player.Masteries.Where(mastery => mastery.Page == MasteryPage.Utility)
                .Any(mastery => mastery.Id == 100 && mastery.Points == 1))
                {
                    abilitySequence = new int[] { 2, 1, 1, 3, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Support";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.BaseSkinName == "Ashe") abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Azir") abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Blitzcrank")
            {
                if (ObjectManager.Player.Masteries.Where(mastery => mastery.Page == MasteryPage.Utility).Any(mastery => mastery.Id == 100 && mastery.Points == 1))
                {
                    abilitySequence = new int[] { 1, 3, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Support";
                }
                else
                {
                    if (Player.FlatMagicDamageMod > Player.FlatPhysicalDamageMod)
                    {
                        abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                        tipo = " AP";
                    }
                    else
                    {
                        abilitySequence = new int[] { 3, 1, 2, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
                        tipo = " AD";
                    }
                }
            }
            else if (Player.BaseSkinName == "Brand") abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Braum") abilitySequence = new int[] { 1, 3, 2, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Caitlyn") abilitySequence = new int[] { 2, 1, 1, 3, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Cassiopeia") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Chogath") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Corki")
            {
                if (Player.FlatMagicDamageMod > Player.FlatPhysicalDamageMod)
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " AP";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " AD";
                }
            }
            else if (Player.BaseSkinName == "Darius") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Diana") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "DrMundo") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Draven") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Elise")
            {
                rOff = -1;
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 2, 1, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.BaseSkinName == "Evelynn") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Ezreal") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "FiddleSticks") abilitySequence = new int[] { 2, 3, 1, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Fiora") abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Fizz")
            {
                if (Player.FlatMagicDamageMod > Player.FlatPhysicalDamageMod)
                {
                    abilitySequence = new int[] { 1, 3, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                    tipo = " AP";
                }
                else
                {
                    abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                    tipo = " AD";
                }
            }
            else if (Player.BaseSkinName == "Galio") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Gangplank") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Garen") abilitySequence = new int[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Gnar") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Gragas") abilitySequence = new int[] { 1, 3, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Graves") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Hecarim") abilitySequence = new int[] { 2, 1, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Heimerdinger") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Irelia") abilitySequence = new int[] { 3, 1, 2, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Janna") abilitySequence = new int[] { 3, 1, 2, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "JarvanIV")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 3, 1, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 3, 1, 2, 1, 4, 1, 3, 1, 3, 4, 3, 2, 3, 2, 4, 2, 2 };
                    tipo = " Lane";
                }
            }
            else if (Player.BaseSkinName == "Jax") abilitySequence = new int[] { 3, 1, 2, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Jayce") { abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 }; rOff = -1; }
            else if (Player.BaseSkinName == "Jinx") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Kalista") abilitySequence = new int[] { 1, 3, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Karma") { abilitySequence = new int[] { 1, 3, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 }; rOff = -1; }
            else if (Player.BaseSkinName == "Karthus") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Kassadin") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Katarina") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Kayle")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 3, 1, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 2, 3, 2, 4, 2, 2 };
                    tipo = " Lane";
                }
            }
            else if (Player.BaseSkinName == "Kennen") abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Khazix")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 1, 3, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " Lane";
                }
            }
            else if (Player.BaseSkinName == "KogMaw") abilitySequence = new int[] { 2, 3, 1, 2, 3, 4, 2, 3, 2, 3, 4, 2, 3, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Leblanc") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "LeeSin")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 3, 1, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " Lane";
                }
            }
            else if (Player.BaseSkinName == "Leona") abilitySequence = new int[] { 1, 3, 2, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Lissandra") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Lucian") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Lulu") abilitySequence = new int[] { 1, 3, 2, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Lux") abilitySequence = new int[] { 3, 2, 1, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Malphite") abilitySequence = new int[] { 3, 1, 2, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Malzahar") abilitySequence = new int[] { 1, 3, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Maokai")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 3, 2, 1, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 3, 1, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.BaseSkinName == "MasterYi") abilitySequence = new int[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "MissFortune") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Mordekaiser") abilitySequence = new int[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Morgana")
            {
                if (ObjectManager.Player.Masteries.Where(mastery => mastery.Page == MasteryPage.Utility)
                .Any(mastery => mastery.Id == 100 && mastery.Points == 1))
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " Support";
                }
                else
                {
                    abilitySequence = new int[] { 2, 3, 1, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                    tipo = " Lane";
                }
            }
            else if (Player.BaseSkinName == "Nami") abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Nasus")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 3, 1, 2, 3, 3, 4, 3, 2, 3, 2, 4, 2, 1, 2, 1, 4, 1, 1 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.BaseSkinName == "Nautilus") abilitySequence = new int[] { 2, 3, 1, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Nidalee") { abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 }; rOff = -1; }
            else if (Player.BaseSkinName == "Nocturne")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 2, 3, 2, 4, 2, 2 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " Lane";
                }
            }
            else if (Player.BaseSkinName == "Nunu")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 1, 3, 2, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                    tipo = " Lane";
                }
            }
            else if (Player.BaseSkinName == "Olaf") abilitySequence = new int[] { 3, 2, 1, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Orianna") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Pantheon") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Poppy") abilitySequence = new int[] { 2, 1, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Quinn") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Rammus") abilitySequence = new int[] { 2, 1, 3, 2, 3, 4, 2, 3, 3, 3, 4, 2, 2, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Rammus") abilitySequence = new int[] { 2, 1, 3, 2, 3, 4, 2, 3, 3, 3, 4, 2, 2, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "RekSai") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Renekton") abilitySequence = new int[] { 2, 1, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Rengar") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Riven")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 1, 2, 1, 3, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.BaseSkinName == "Rumble") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Ryze") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Sejuani") abilitySequence = new int[] { 2, 3, 1, 2, 2, 4, 2, 1, 2, 3, 4, 3, 3, 3, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Shaco")
            {
                if (Player.FlatMagicDamageMod > Player.FlatPhysicalDamageMod)
                {
                    abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                    tipo = " AP";
                }
                else
                {
                    abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " AD";
                }
            }
            else if (Player.BaseSkinName == "Shen") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Shyvana") abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Singed") abilitySequence = new int[] { 1, 3, 1, 3, 1, 4, 1, 2, 1, 2, 4, 3, 2, 3, 2, 4, 2, 3 };
            else if (Player.BaseSkinName == "Sion")
            {
                if (Player.FlatMagicDamageMod > Player.FlatPhysicalDamageMod)
                {
                    abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                    tipo = " AP";
                }
                else
                {
                    abilitySequence = new int[] { 1, 3, 2, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 1, 1, 4, 1, 1 };
                    tipo = " AD";
                }
            }
            else if (Player.BaseSkinName == "Sivir") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Skarner")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 3, 4, 3, 2, 2, 3, 4, 3, 2 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 3, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.BaseSkinName == "Sona") abilitySequence = new int[] { 1, 2, 1, 2, 1, 4, 3, 1, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Soraka") abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 3, 3, 3, 3, 4, 2, 2, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Swain") abilitySequence = new int[] { 2, 3, 1, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Syndra") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Talon") abilitySequence = new int[] { 2, 3, 1, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Taric") abilitySequence = new int[] { 3, 2, 1, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Teemo") abilitySequence = new int[] { 1, 3, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Thresh") abilitySequence = new int[] { 3, 1, 3, 2, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Tristana") abilitySequence = new int[] { 3, 2, 1, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Trundle")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 3, 4, 2, 2, 2, 3, 4, 3, 3 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 1, 3, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.BaseSkinName == "Tryndamere") abilitySequence = new int[] { 3, 2, 1, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "TwistedFate") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Twitch") abilitySequence = new int[] { 3, 2, 1, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Udyr")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 4, 1, 3, 4, 4, 3, 4, 3, 4, 3, 3, 1, 1, 1, 1, 2, 2, 2 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 3, 1, 2, 1, 2, 3, 2, 3, 3, 2, 4, 4, 4 };
                    tipo = " Lane";
                }
            }
            else if (Player.BaseSkinName == "Urgot") abilitySequence = new int[] { 3, 1, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Varus") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Vayne") abilitySequence = new int[] { 1, 3, 2, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "Veigar") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Velkoz")
            {
                if (ObjectManager.Player.Masteries.Where(mastery => mastery.Page == MasteryPage.Utility)
                .Any(mastery => mastery.Id == 100 && mastery.Points == 1))
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Support";
                }
                else
                {
                    abilitySequence = new int[] { 2, 3, 1, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.BaseSkinName == "Vi")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 3, 1, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 3, 1, 1, 2, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.BaseSkinName == "Viktor") abilitySequence = new int[] { 3, 2, 1, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Vladimir") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Volibear") abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
            else if (Player.BaseSkinName == "Warwick") abilitySequence = new int[] { 2, 1, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "MonkeyKing") abilitySequence = new int[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Xerath") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
            else if (Player.BaseSkinName == "XinZhao")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 2, 4, 2, 3, 2, 3, 4, 2, 3 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    tipo = " Lane";
                }
            }
            else if (Player.BaseSkinName == "Yasuo") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Yorick") abilitySequence = new int[] { 2, 3, 1, 3, 3, 4, 3, 2, 3, 1, 4, 2, 1, 2, 1, 4, 2, 1 };
            else if (Player.BaseSkinName == "Zac")
            {
                if (Smite != SpellSlot.Unknown)
                {
                    abilitySequence = new int[] { 2, 1, 3, 3, 1, 4, 3, 1, 3, 1, 4, 3, 1, 2, 2, 4, 2, 2 };
                    tipo = " Jungler";
                }
                else
                {
                    abilitySequence = new int[] { 2, 3, 1, 2, 2, 4, 2, 3, 2, 3, 4, 3, 3, 1, 1, 4, 1, 1 };
                    tipo = " Lane";
                }
            }
            else if (Player.BaseSkinName == "Zed") abilitySequence = new int[] { 3, 2, 1, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Ziggs") abilitySequence = new int[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Zilean") abilitySequence = new int[] { 1, 2, 3, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
            else if (Player.BaseSkinName == "Zyra")
            {
                if (ObjectManager.Player.Masteries.Where(mastery => mastery.Page == MasteryPage.Utility)
                .Any(mastery => mastery.Id == 100 && mastery.Points == 1))
                {
                    abilitySequence = new int[] { 1, 2, 3, 3, 3, 4, 3, 1, 1, 3, 4, 1, 1, 2, 2, 4, 2, 2 };
                    tipo = " Support";
                }
                else
                {
                    abilitySequence = new int[] { 1, 2, 1, 3, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    tipo = " Lane";
                }
            }

        }
    }
}