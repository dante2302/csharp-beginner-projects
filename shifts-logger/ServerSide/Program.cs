using shifts_logger.Data.DbContexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
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

