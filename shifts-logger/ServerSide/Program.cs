using shifts_logger;
using shifts_logger.Data.DbContexts;
using shifts_logger.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient<ShiftClientService>();
builder.Services.AddHttpsRedirection((options) =>
{
    options.HttpsPort = 443;
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
});

builder.Services.AddDbContext<ShiftContext>();
var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

