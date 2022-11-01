using JFA.Telegram;
using Xarajat.Bot.Services;
using Xarajat.Data.Context;

namespace Xarajat.Bot.Common;

public abstract class CommandHandler : CommandHandlerBase<MessageContext>
{
    protected CommandHandler(XarajatDbContext context, TelegramBotService telegramBotService)
    {
        Context = context;
        TelegramBotService = telegramBotService;
    }

    protected XarajatDbContext Context { get; set; }
    protected TelegramBotService TelegramBotService { get; set; }
}