﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;

namespace Akali
{
    class Akali
    {
        public static Menu Menu;
        public static Spell Q, E, W, R;
        public static Items.Item Zhonyas;
        public static SpellSlot Ignite;

        private static void Main(string[] args)
        {
            if (args != null)
            {
                CustomEvents.Game.OnGameLoad += CargarScript;
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

        private static void CargarScript(EventArgs args)
        {
            if (ObjectManager.Player.BaseSkinName != "Akali")
            {
                Game.PrintChat("Estas cargando una assemblie de singed, cuando tu campeon es:" + ObjectManager.Player.BaseSkinName);
                return;
            }

            Zhonyas = new Items.Item(3157, 0f);
            Ignite = ObjectManager.Player.GetSpellSlot("SummonerDot");

            Q = new Spell(SpellSlot.Q, 600);
            W = new Spell(SpellSlot.W, 700);
            E = new Spell(SpellSlot.E, 325);
            R = new Spell(SpellSlot.R, 800);

            EjecutarMenu();
        }

        private static void EjecutarMenu()
        {
            Menu = new Menu("Assemblie Akali", "Akali - Menu", true);
            {
                var comboMenu = new Menu("Combo", "Combo");
                {
                    comboMenu.AddItem(new MenuItem("UsarQ", "Utilizar Q").SetValue(true));
                    comboMenu.AddItem(new MenuItem("UsarW", "Utilizar W").SetValue(true));
                    comboMenu.AddItem(new MenuItem("UsarE", "Utilizar E").SetValue(false));
                    comboMenu.AddItem(new MenuItem("UsarR", "Utilizar R").SetValue(false));
                    Menu.AddSubMenu(comboMenu);
                }

                var LimpiarLinea = new Menu("LimpiarLinea", "Limpiar la Linea");
                {
                    LimpiarLinea.AddItem(new MenuItem("LimpiarE", "Limpiar usando la E?").SetValue(true));
                    LimpiarLinea.AddItem(new MenuItem("MinimoEMinions", "Numero de Minions para usar la E").SetValue(new Slider(2, 1, 5)));
                    LimpiarLinea.AddItem(new MenuItem("LimpiarLineaActivo", "Activo:").SetValue(new KeyBind('V', KeyBindType.Press)));
                    Menu.AddSubMenu(LimpiarLinea);
                }

                var Señalizaciones = new Menu("Dibujos", "Alcanze Hechizos");
                {
                    Señalizaciones.AddItem(new MenuItem("DibujoQ", "Limpiar usando la E?").SetValue(true));
                    Señalizaciones.AddItem(new MenuItem("DibujoW", "Limpiar usando la E?").SetValue(false));
                    Señalizaciones.AddItem(new MenuItem("DibujoE", "Limpiar usando la E?").SetValue(false));
                    Señalizaciones.AddItem(new MenuItem("DibujoR", "Limpiar usando la E?").SetValue(true));
                    Menu.AddSubMenu(Señalizaciones);
                }
                Menu.AddToMainMenu();
            }
            Game.PrintChat("<font color=\"#DF0101\">LeagueSharp - Assemblie Akali cargada</font>");
            Drawing.OnDraw += AlcanzeHechizos;
        }

        public static void AlcanzeHechizos(EventArgs args)
        {
            if (!ObjectManager.Player.IsDead)
            {
                if (Menu.Item("DibujoQ").GetValue<bool>() && Q.Level > 0)
                {
                    Utility.DrawCircle(ObjectManager.Player.Position, Q.Range, System.Drawing.Color.Red);
                }
                if (Menu.Item("DibujoW").GetValue<bool>() && W.Level > 0)
                {
                    Utility.DrawCircle(ObjectManager.Player.Position, W.Range, System.Drawing.Color.Black);
                }
                if (Menu.Item("DibujoE").GetValue<bool>() && E.Level > 0)
                {
                    Utility.DrawCircle(ObjectManager.Player.Position, E.Range, System.Drawing.Color.Cyan);
                }
                if (Menu.Item("DibujoR").GetValue<bool>() && R.Level > 0)
                {
                    Utility.DrawCircle(ObjectManager.Player.Position, R.Range, System.Drawing.Color.Green);
                }
            }
            else
            {
                return;
            }
        }
    }
}
