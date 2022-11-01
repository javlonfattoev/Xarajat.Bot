using JFA.Telegram;
using Xarajat.Bot.Common;
using Xarajat.Bot.Enums;
using Xarajat.Bot.Services;
using Xarajat.Data.Context;

namespace Xarajat.Bot.Commands;

[Command((int)EStep.Default)]
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

    [Method("/newroom")]
    public async Task CreateRoom(MessageContext context)
    {
        if (context.User!.RoomId is not null)
            await TelegramBotService.SendMessage(context.ChatId, "Already in room! Leave room first");
        
        await TelegramBotService.SendMessage(context.ChatId, "Enter new room name.");
        context.User.Step = (int)EStep.EnterNewRoomName;
        await Context.SaveChangesAsync();
    }

    [Method("/joinroom")]
    public async Task JoinRoom(MessageContext context)
    {
        if (context.User!.RoomId is not null)
            await TelegramBotService.SendMessage(context.ChatId, "Already in room! Leave room first");

        await TelegramBotService.SendMessage(context.ChatId, "Enter room key.");
        context.User.Step = (int)EStep.EnterJoinRoomKey;
        await Context.SaveChangesAsync();
    }
}