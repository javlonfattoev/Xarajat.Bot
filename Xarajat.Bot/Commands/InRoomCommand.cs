using System.Text;
using JFA.Telegram;
using Microsoft.EntityFrameworkCore;
using Xarajat.Bot.Common;
using Xarajat.Bot.Enums;
using Xarajat.Bot.Services;
using Xarajat.Data.Context;

namespace Xarajat.Bot.Commands
{
    [Command((int)EStep.InRoom)]
    public class InRoomCommand : CommandHandler
    {
        public InRoomCommand(XarajatDbContext context, TelegramBotService telegramBotService) : base(context, telegramBotService)
        {
        }

        [Method("Add outlay")]
        public async Task AddOutlay(MessageContext context)
        {
            context.User!.Step = (int)EStep.EnterOutlayDetails;
            await Context.SaveChangesAsync();
            await TelegramBotService.SendMessage(context.ChatId, "Enter outlay details. \n Cost - Description");
        }

        [Method("Calculate")]
        public async Task CalculateRoom(MessageContext context)
        {
            var room = await Context.Rooms
                .Include(r => r.Users)
                .Include(r => r.Outlays)
                .FirstOrDefaultAsync(r => r.Id == context.User!.RoomId);

            if (room is null)
            {
                await TelegramBotService.SendMessage(context.ChatId, "You are not in room. Create or join first"); return;
            }

            var total = room!.Outlays!.Sum(o => o.Cost);
            var usersCount = room.Users?.Count;

            var message = new StringBuilder();
            message.Append($"Room: {room.Name}\n");
            message.Append($"Users: {usersCount}\n");
            message.Append($"Total: {total}\n");
            message.Append($"Per user: {total/usersCount}\n");

            await TelegramBotService.SendMessage(context.ChatId, message.ToString());
        }

        [Method("My room")]
        public async Task MyRoom(MessageContext context)
        {
            if (context.User?.Room is null)
            {
                await TelegramBotService.SendMessage(context.ChatId, "You are not in room. Create or join first"); return;
            }

            var message = new StringBuilder();
            message.Append($"Room: {context.User.Room.Name}\n");
            message.Append($"Key: {context.User.Room.Key}\n");
            message.Append($"Status: {context.User.Room.Status}\n");

            await TelegramBotService.SendMessage(context.ChatId, message.ToString());
        }
    }
}
