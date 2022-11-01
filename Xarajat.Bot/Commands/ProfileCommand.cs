using JFA.Telegram;
using Xarajat.Bot.Common;
using Xarajat.Bot.Services;
using Xarajat.Data.Context;

namespace Xarajat.Bot.Commands;

[Command(0)]
public class ProfileCommand : CommandHandler
{
    public ProfileCommand(XarajatDbContext context, TelegramBotService telegramBotService) : base(context, telegramBotService)
    {
    }

    [Method("/profile")]
    public async Task SendProfile(MessageContext context)
    {
        await TelegramBotService.SendMessage(context.User!.ChatId, context.User!.Name!);
    }

    [Method("/start")]
    public async Task SendMenu(MessageContext context)
    {
        await TelegramBotService.SendMessage(context.User!.ChatId, "Menu");
    }
}