// telegram
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Net;

namespace Jamae
{
    class TelegramApi
    {
        private static string ApiKey = "5029203473:AAE5WOf--vIefHqs-iL69FnPAOdjnqzPTcc";
        private static string ChatID = "2114969217";

        TelegramBotClient Bot = new Telegram.Bot.TelegramBotClient(ApiKey);

        public string trxClosePrice { get; set; }

        public TelegramApi()
        {
            setTelegramEvent();
        }

        private void setTelegramEvent()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Bot.OnMessage += Bot_OnMessage;

            Bot.StartReceiving();
        }

        public async void Bot_SendMessage(string msg)
        {
            await Bot.SendTextMessageAsync(ChatID, msg);
        }


        private async void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e.Message;

            if (message == null || message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
                return;

            if (message.Text == "/who")
            {
                await Bot.SendTextMessageAsync(message.Chat.Id, "james.kim");
            }
            else if (message.Text == "/price")
            {
                await Bot.SendTextMessageAsync(message.Chat.Id, trxClosePrice);

            }
            else
            {
                await Bot.SendTextMessageAsync(message.Chat.Id, message.Text);
            }
        }
    }
}
