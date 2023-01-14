using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;


namespace BirthdayReminder.Telegram
{
    public class TelegramService : ITelegramService
    {
        private static string token { get; set; } = "5709592372:AAEo8ZYJgISbeXSXejscODAu0OIuu2ZN7UE";
        private static TelegramBotClient client;
        public void CallTelegramBot()
        {
            Console.WriteLine("Start TelegramBot...\n");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You can use the Bot!");
            TelegramService telegramService = new TelegramService();
            client = new TelegramBotClient(token);
            client.StartReceiving();
            client.OnMessage += telegramService.OnMessageHandler;
            Console.ReadLine();
            client.StopReceiving();


        }
        private async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            if (msg.Text != null)
            {
                Console.WriteLine($"Message mit dem Text: {msg.Text}");
                //await client.SendTextMessageAsync(msg.Chat.Id, msg.Text, replyMarkup: GetButtons());

                switch (msg.Text)
                {
                    case "Stic":
                        var stic = await client.SendStickerAsync(
                            chatId: msg.Chat.Id,
                            sticker: "https://cdn.tlgrm.app/stickers/ccd/a8d/ccda8d5d-d492-4393-8bb7-e33f77c24907/192/1.webp",
                            replyToMessageId: msg.MessageId,
                            replyMarkup: GetButtons());
                        break;
                    case "Happy":
                        var stic2 = await client.SendStickerAsync(
                            chatId: msg.Chat.Id,
                            sticker: "https://tlgrm.ru/_/stickers/ccd/a8d/ccda8d5d-d492-4393-8bb7-e33f77c24907/9.webp",
                            replyToMessageId: msg.MessageId,
                            replyMarkup: GetButtons());
                        break;
                    case "Cry":
                        var stic3 = await client.SendStickerAsync(
                            chatId: msg.Chat.Id,
                            sticker: "https://tlgrm.ru/_/stickers/ccd/a8d/ccda8d5d-d492-4393-8bb7-e33f77c24907/4.webp",
                            replyToMessageId: msg.MessageId,
                            replyMarkup: GetButtons());
                        break;
                    case "Wer ist heute geboren?":
                        var heuteGeboren = await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text: $"Heute war ___ geboren",
                            replyMarkup: GetButtons());
                        break;
                    case "Wer ist morgen geboren?":
                        var morgenGeboren = await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text: $"Morgen war ___ geboren",
                            replyMarkup: GetButtons());
                        break;
                    case "Ein Grüß sagen":
                        var gruseSagen = await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text: $"Ein Grüß war ___ geschickt",
                            replyMarkup: GetButtons());
                        break;
                    default:
                        await client.SendTextMessageAsync(msg.Chat.Id, "Chouse comand: ", replyMarkup: GetButtons());
                        break;
                }
            }
        }

        private IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{new KeyboardButton { Text = "Wer ist heute geboren?" },new KeyboardButton { Text = "Wer ist morgen geboren?"} },
                    new List<KeyboardButton>{new KeyboardButton { Text = "Cry"},new KeyboardButton { Text = "Happy"} },
                    new List<KeyboardButton>{new KeyboardButton { Text = "Ein Grüß sagen"} }
                }
            };
        }
    }
}
