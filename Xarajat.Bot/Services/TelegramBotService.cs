using JFA.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Xarajat.Bot.Services;

[Scoped]
public class TelegramBotService
{
    private readonly TelegramBotClient _bot;

    public TelegramBotService()
    {
        _bot = new TelegramBotClient("5481889693:AAHWere6v27dnKpEBYXO80ffHTsUeADx3sc");
    }

    public async Task SendMessage(long chatId, string message, IReplyMarkup? reply = null)
    {
        await _bot.SendTextMessageAsync(chatId, message, replyMarkup: reply);
    }

    public async Task SendMessage(long chatId, string message, Stream image, IReplyMarkup? reply = null)
    {
        await _bot.SendPhotoAsync(chatId, new InputOnlineFile(image), message, replyMarkup: reply);
    }

    public async Task EditMessageButtons(long chatId, int messageId, InlineKeyboardMarkup reply)
    {
        await _bot.EditMessageReplyMarkupAsync(chatId, messageId, replyMarkup: reply);
    }

    public ReplyKeyboardMarkup GetKeyboard(List<string> buttonsText)
    {
        return new ReplyKeyboardMarkup(buttonsText.Select(text => 
            new KeyboardButton[] { new(text) })) { ResizeKeyboard = true };
    }

    public InlineKeyboardMarkup GetInlineKeyboard(List<string> buttonsText)
    {
        return new InlineKeyboardMarkup(buttonsText.Select(text=> new[]
        {
            InlineKeyboardButton.WithCallbackData(text: text, callbackData: text)
        }));
    }

}