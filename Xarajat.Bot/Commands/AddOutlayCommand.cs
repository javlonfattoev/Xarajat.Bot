using JFA.Telegram;
using Xarajat.Bot.Common;
using Xarajat.Bot.Enums;
using Xarajat.Bot.Services;
using Xarajat.Data.Context;
using Xarajat.Data.Entities;

namespace Xarajat.Bot.Commands
{
    [Command((int)EStep.EnterOutlayDetails)]
    public class AddOutlayCommand : CommandHandler
    {
        public AddOutlayCommand(XarajatDbContext context, TelegramBotService telegramBotService) : base(context, telegramBotService)
        {
        }

        [Method]
        public async Task AddOutlay(MessageContext context)
        {
            if (context.User?.Room is null)
            {
                await TelegramBotService.SendMessage(context.ChatId, "You are not in room. Create or join first"); return;
            }

            if (string.IsNullOrEmpty(context.Message))
            {
                await TelegramBotService.SendMessage(context.ChatId, "Enter valid outlay details. \n Cost - Description"); return;
            }

            var outlayDetails = context.Message.Trim().Split('-');

            if (outlayDetails.Length == 0 || !int.TryParse(outlayDetails[0], out var cost))
            {
                await TelegramBotService.SendMessage(context.ChatId, "Enter valid outlay details. \n Cost - Description"); return;
            }

            var outlay = new Outlay
            {
                Cost = cost,
                Description = outlayDetails[1],
                UserId = context.User.Id,
                RoomId = context.User.RoomId
            };

            await Context.Outlays.AddAsync(outlay);
            context.User!.Step = (int)EStep.InRoom;
            await Context.SaveChangesAsync();

            await TelegramBotService.SendMessage(context.ChatId, "Outlay added", TelegramBotService.GetKeyboard(new List<string>()
            {
                "Add outlay",
                "Calculate",
                "My room"
            }));
        }
    }
}
