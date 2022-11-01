using User = Xarajat.Data.Entities.User;

namespace Xarajat.Bot.Common;

public class MessageContext : JFA.Telegram.MessageContextBase<User>
{
    public string? Username { get; set; }
    public string? Name { get; set; }
    public int MessageId { get; set; }
}