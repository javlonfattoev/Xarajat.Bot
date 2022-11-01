using JFA.Telegram;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddTelegramCommands();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
