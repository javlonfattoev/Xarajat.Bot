using JFA.Telegram;
using Xarajat.Bot.Common;
using Xarajat.Bot.Enums;
using Xarajat.Bot.Services;
using Xarajat.Data.Context;
using Xarajat.Data.Entities;

namespace Xarajat.Bot.Commands;

[Command((int)EStep.EnterNewRoomName)]
public class CreateRoomCommand : CommandHandler
{
    public CreateRoomCommand(XarajatDbContext context, TelegramBotService telegramBotService) : base(context, telegramBotService)
    {
    }

    [Method]
    public async Task CreateRoom(MessageContext context)
    {
        if (string.IsNullOrEmpty(context.Message) || context.Message.Length < 4)
        {
            await TelegramBotService.SendMessage(context.ChatId, "Enter valid room name.");
            return;
        }

        var room = new Room
        {
            Key = Guid.NewGuid().ToString("N"),
            Name = context.Message,
            Status = RoomStatus.Created
        };

        await Context.Rooms.AddAsync(room);
        await Context.SaveChangesAsync();
        context.User!.RoomId = room.Id;
        context.User!.Step = (int)EStep.InRoom;
        await Context.SaveChangesAsync();

        await TelegramBotService.SendMessage(context.ChatId, "Menu", TelegramBotService.GetKeyboard(new List<string>()
        {
            "Add outlay",
            "Calculate",
            "My room"
        }));
    }
}