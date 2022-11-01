using JFA.Telegram;
using Microsoft.EntityFrameworkCore;
using Xarajat.Bot.Common;
using Xarajat.Bot.Enums;
using Xarajat.Bot.Services;
using Xarajat.Data.Context;

namespace Xarajat.Bot.Commands
{
    [Command((int)EStep.EnterJoinRoomKey)]
    public class JoinRoomCommand : CommandHandler
    {
        public JoinRoomCommand(XarajatDbContext context, TelegramBotService telegramBotService) : base(context, telegramBotService)
        {
        }

        [Method]
        public async Task JoinRoom(MessageContext context)
        {
            if (string.IsNullOrEmpty(context.Message) || context.Message.Length < 32)
            {
                await TelegramBotService.SendMessage(context.ChatId, "Enter valid room key.");
                return;
            }

            var room = await Context.Rooms.FirstOrDefaultAsync(r => r.Key == context.Message);
            if (room is null)
            {
                await TelegramBotService.SendMessage(context.ChatId, "Enter valid room key.");
                return;
            }

            context.User!.RoomId = room.Id;
            context.User!.Step = (int)EStep.InRoom;
            await Context.SaveChangesAsync();

            await TelegramBotService.SendMessage(context.ChatId, "Menu", TelegramBotService.GetKeyboard(new List<string>
            {
                "Add outlay",
                "Calculate",
                "My room"
            }));
        }
    }
}
