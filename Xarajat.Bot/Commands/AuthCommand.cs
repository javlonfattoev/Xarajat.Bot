using JFA.DependencyInjection;
using JFA.Telegram;
using Microsoft.EntityFrameworkCore;
using Xarajat.Bot.Common;
using Xarajat.Bot.Services;
using Xarajat.Data.Context;
using Xarajat.Data.Entities;

namespace Xarajat.Bot.Commands;

[Scoped]
public class AuthCommand : CommandHandler
{
    public AuthCommand(XarajatDbContext context, TelegramBotService telegramBotService) : base(context, telegramBotService)
    {
    }

    [Method]
    public async Task Auth(MessageContext context)
    {
        var user = await Context.Users.FirstOrDefaultAsync(u => u.ChatId == context.ChatId);
        if (user is null)
        {
            user = new User
            {
                ChatId = context.ChatId,
                Name = context.Username,
                Step = 0,

            };

            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();
        }

        context.User = user;
    }
}