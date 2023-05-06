using Parcs.HostAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddValidation();
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddApplicationOptions(builder.Configuration);
builder.Services.AddAsynchronousJobProcessing();
builder.Services.AddHttpClient();
builder.Services.AddDatabase(builder.Configuration);

var app = builder.Build();

app.MigrateDatabase();

app.UseSwagger();
app.UseSwaggerUI();

app.UseGlobalExceptionHandler();
app.UseAuthorization();
app.MapControllers();

app.Run();