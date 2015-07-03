namespace Faker
{
    using LeagueSharp;
    using LeagueSharp.Common;

    internal class Program
    {
        #region Static Fields

        private static string Message = "gj";

        private static string Sender = "Server Message";

        #endregion

        #region Methods

        private static void Main(string[] args)
        {
            Game.OnChat += OnChatFake;
            Game.OnChat += OnChatMessage;
            Game.OnChat += OnChatSender;
            CustomEvents.Game.OnGameLoad += eventArgs => Notifications.AddNotification("Faker by h3h3 Loaded.", 5);
        }

        private static void OnChatFake(GameChatEventArgs args)
        {
            if (!args.Sender.IsMe)
            {
                return;
            }

            if (!args.Message.StartsWith("."))
            {
                return;
            }

            if (!args.Message.Contains("fake"))
            {
                return;
            }

            args.Process = false;

            var msg = args.Message.Substring(args.Message.IndexOf(" ") + 1);
            var message = string.Format("/all {0}{1}[{2}] {3}", Message, new string(' ', 70), Sender, msg);

            Game.Say(message);
        }

        private static void OnChatSender(GameChatEventArgs args)
        {
            if (!args.Sender.IsMe)
            {
                return;
            }

            if (!args.Message.StartsWith("."))
            {
                return;
            }

            if (!args.Message.Contains("sender"))
            {
                return;
            }

            args.Process = false;

            var msg = args.Message.Substring(args.Message.IndexOf(" ") + 1);
            Notifications.AddNotification("Set fake Sender to '" + msg + "'", 5);
        }

        private static void OnChatMessage(GameChatEventArgs args)
        {
            if (!args.Sender.IsMe)
            {
                return;
            }

            if (!args.Message.StartsWith("."))
            {
                return;
            }

            if (!args.Message.Contains("msg"))
            {
                return;
            }

            args.Process = false;

            var msg = args.Message.Substring(args.Message.IndexOf(" ") + 1);
            Notifications.AddNotification("Set fake Message to '" + msg + "'", 5);
        }

        #endregion
    }
}