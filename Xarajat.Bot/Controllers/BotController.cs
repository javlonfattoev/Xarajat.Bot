using JFA.Telegram;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xarajat.Bot.Commands;
using Xarajat.Bot.Common;

namespace Xarajat.Bot.Controllers;

[ApiController]
[Route("bot")]
public class BotController : ControllerBase
{
    [HttpGet]
    public IActionResult GetMe() => Ok("Working...");

    [HttpPost]
    public async Task PostUpdate(Update update, [FromServices] ICommandFactory commandFactory)
    {
        if (!TryCreateContext(update, out var context))
            return;

        var auth = commandFactory.CreateCommand<MessageContext>(typeof(AuthCommand));
        if (auth is null) return;
        await auth.Excute(context);

        var command = commandFactory.CreateCommand(context);
        if (command is null) return;
        await command.Excute(context);
    }

    private static bool TryCreateContext(Update update, out MessageContext context)
    {
        context = new MessageContext { Step = 0 };

        switch (update.Type)
        {
            case UpdateType.Message:
                context.MessageId = update.Message!.MessageId;
                context.ChatId = update.Message!.From!.Id;
                context.Message = update.Message.Text;
                context.Username = update.Message.From.Username;
                context.Name = update.Message.From.FirstName + " " + update.Message.From.LastName;
                return true;
            case UpdateType.CallbackQuery:
                context.MessageId = update.CallbackQuery!.Message!.MessageId;
                context.ChatId = update.CallbackQuery!.From.Id;
                context.Message = update.CallbackQuery.Data;
                context.Username = update.CallbackQuery.From.Username;
                context.Name = update.CallbackQuery.From.FirstName + " " + update.CallbackQuery.From.LastName;
                return true;
            default:
                return false;
        }
    }
}
