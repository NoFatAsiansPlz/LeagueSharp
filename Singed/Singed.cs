﻿using System;
using LeagueSharp;
using LeagueSharp.Common;
using Kiteo;

namespace Singed
{
    class Singed
    {
        public static Menu Menu;
        public static Spell Q, W, E;
        public static Kiteo.Kiteo.Orbwalker MenuKiteo;
        public static bool RisaMalvada = false;
        public static Obj_AI_Hero Jugador = ObjectManager.Player;
        
        private static void Main(string[] args)
        {
            if (args != null)
            {
                CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
                AppDomain.CurrentDomain.UnhandledException +=
                delegate(object sender, UnhandledExceptionEventArgs eventArgs)
                {
                    var exception = eventArgs.ExceptionObject as Exception;
                    if (exception != null)
                    {
                        Console.WriteLine(exception.Message);
                    }
                };
            }
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            if (ObjectManager.Player.BaseSkinName != "Singed")
            {
                Game.PrintChat("Estas cargando una assemblie de singed, cuando tu campeon es:" + ObjectManager.Player.BaseSkinName);
                return;
            }

            Q = new Spell(SpellSlot.Q);
            W = new Spell(SpellSlot.W, 1000);
            E = new Spell(SpellSlot.E, 125);
            W.SetSkillshot(0.5f, 350f, 700f, false, SkillshotType.SkillshotCircle);

            EjecutarMenu();
            Game.PrintChat("<font color=\"#DF0101\">LeagueSharp - Assemblie singed cargada</font>");
        }

        private static void EjecutarMenu()
        {
            Menu = new Menu("Singed", "Menu", true);
            var orbwalkerMenu = new Menu("Kiteo", "Menu Kiteo");
            MenuKiteo = new Kiteo.Kiteo.Orbwalker(orbwalkerMenu);
            Menu.AddSubMenu(orbwalkerMenu);

            var comboMenu = new Menu("Combo", "combo");
            comboMenu.AddItem(new MenuItem("UsarW", "Utilizar W").SetValue(true));
            comboMenu.AddItem(new MenuItem("UsarE", "Utilizar E").SetValue(true));
            comboMenu.AddItem(new MenuItem("UsarR", "Utilizar R").SetValue(true));
            Menu.AddSubMenu(comboMenu);

            Menu.AddItem(new MenuItem("Exploit", "Invisible Q").SetValue(new KeyBind("T".ToCharArray()[0], KeyBindType.Toggle)));
            Menu.AddItem(new MenuItem("Risa", "Risa malvada").SetValue(true));
            Menu.AddToMainMenu();

            Game.OnGameUpdate += ActualizarEstado;
        }

        public static void ActualizarEstado(EventArgs args)
        {
            switch (MenuKiteo.ActiveMode)
            {
                case Kiteo.Kiteo.OrbwalkingMode.Combo:
                    Combo();
                break;
            }
            QExploit();
        }

        private static void Combo()
        {
            var UsarW = Menu.Item("UsarW").GetValue<bool>();
            var UsarE = Menu.Item("UsarE").GetValue<bool>();
            var objetivo = TargetSelector.GetTarget(W.Range, TargetSelector.DamageType.Magical);
            if (objetivo != null)
            {
                if (W.IsReady() && Jugador.Distance(objetivo) < W.Range + objetivo.BoundingRadius && UsarW)
                    W.Cast(objetivo);
                if (E.IsReady() && Jugador.Distance(objetivo) < E.Range && UsarE)
                    E.Cast(objetivo, true);
            }
        }

        public static void QExploit()
        {
            if (Menu.Item("Exploit").GetValue<KeyBind>().Active)
            {
                if (Q.IsReady())
                {
                    Q.Cast(ObjectManager.Player, true);
                }
                if (Menu.Item("Risa").GetValue<bool>())
                {
                    Packet.C2S.Emote.Encoded(new Packet.C2S.Emote.Struct(2)).Send();
                    Packet.C2S.Move.Encoded(new Packet.C2S.Move.Struct(Game.CursorPos.X, Game.CursorPos.Y)).Send();
                }
                else
                {
                    Game.PrintChat("<font color=\"#DF0101\">LeagueSharp - Activa la Risa para el exploit</font>");
                }
            }
            else
            {
                return;
            }
        }

    }
}
