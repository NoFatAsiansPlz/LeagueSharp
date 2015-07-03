using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace HaydariGeceler_cici_wipi_ENG
{
    internal class Program
    {
        private static LeagueSharp.Common.Menu haydarigeceler;
        public static bool duramk = false;
        public static float gameTime1 = 0;
        private static string IsMe = "glhf";
        private static string riot = "[Riot Games™] We added a new feature in league! Type fiddlix without the chat box!";
        private static string message;

        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }
        private static void Game_OnGameLoad(EventArgs args)
        {
        Game.PrintChat(
                "<font color = \"#ff052b\">HaydariGeceler cici wipi</font>  <font color = \"#fcdfff\">Loaded  </font> ");

            haydarigeceler = new LeagueSharp.Common.Menu("HaydariGeceler cici wipi", "", true);
            var press = haydarigeceler.AddItem(new MenuItem("GGyaz", "Write GG").SetValue(new KeyBind(37, KeyBindType.Press)));
            var press2 = haydarigeceler.AddItem(new MenuItem("WPyaz", "Write WP").SetValue(new KeyBind(39, KeyBindType.Press)));
            var press3 = haydarigeceler.AddItem(new MenuItem("XDyaz", "Write XD").SetValue(new KeyBind(40, KeyBindType.Press)));
            var press4 = haydarigeceler.AddItem(new MenuItem("PNSciz", "Draw Penis").SetValue(new KeyBind(96, KeyBindType.Press)));
            haydarigeceler.AddItem(new MenuItem("İnfo", "Writed By HaydariGeceler, thanks to everyone for the support:)"));
            haydarigeceler.AddToMainMenu();


            press.ValueChanged += delegate(object sender, OnValueChangeEventArgs EventArgs)
            {
                if (haydarigeceler.Item("GGyaz").GetValue<KeyBind>().Active)
                    if (duramk == false)
                    {
                        
                        Game.Say("/all                           ");
                        Game.Say("/all   ######        ######   ");
                        Game.Say("/all  ##                  ##       ");
                        Game.Say("/all  ##                  ##       ");
                        Game.Say("/all  ##   ####     ##   ####");
                        Game.Say("/all  ##        ##     ##       ##");
                        Game.Say("/all  ##        ##     ##       ##");
                        Game.Say("/all   ######        ######  ");
                        duramk = true;
                        gameTime1 = Game.Time + 1;
                        
                    }
                if (Game.Time > gameTime1)
                {
                    duramk = false;
                }
            };
            press2.ValueChanged += delegate(object sender, OnValueChangeEventArgs EventArgs)
            {
                if (haydarigeceler.Item("WPyaz").GetValue<KeyBind>().Active)
                    if (duramk == false)
                    {

                        Game.Say("/all                           ");
                        Game.Say("/all  ##         ##   ####### ");
                        Game.Say("/all  ##  ##  ##   ##      ##");
                        Game.Say("/all  ##  ##  ##   ##      ##");
                        Game.Say("/all  ##  ##  ##   ####### ");
                        Game.Say("/all  ##  ##  ##   ##       ");
                        Game.Say("/all  ##  ##  ##   ##       ");
                        Game.Say("/all   ###  ###    ##      ");

                        duramk = true;
                        gameTime1 = Game.Time + 1;

                    }
                if (Game.Time > gameTime1)
                {
                    duramk = false;
                }
            };
            press3.ValueChanged += delegate(object sender, OnValueChangeEventArgs EventArgs)
            {
                if (haydarigeceler.Item("XDyaz").GetValue<KeyBind>().Active)
                    if (duramk == false)
                    {
                        var message = string.Format("/all {0}{1}  {2}", IsMe, new string(' ', 65 + sender.Length), riot);
                        Game.Say(message);
                        args.Process = false;
                        duramk = true;
                        gameTime1 = Game.Time + 1;

                    }
                if (Game.Time > gameTime1)
                {
                    duramk = false;
                }
            };
            press4.ValueChanged += delegate(object sender, OnValueChangeEventArgs EventArgs)
            {
                if (haydarigeceler.Item("PNSciz").GetValue<KeyBind>().Active)
                    if (duramk == false)
                    {

                        Game.Say("/all          ____");
                        Game.Say("/all        / /     7");
                        Game.Say("/all       (__,__/\\ ");
                        Game.Say("/all        \\         \\ ");
                        Game.Say("/all         \\         \\ ");
                        Game.Say("/all       __\\         \\__");
                        Game.Say("/all      (     \\            )");
                        Game.Say("/all       \\___\\_____/  ");

                        duramk = true;
                        gameTime1 = Game.Time + 1;

                    }
                if (Game.Time > gameTime1)
                {
                    duramk = false;
                }
            };
        }
    }
}
          

            
        

        
        
            
        
            


       
