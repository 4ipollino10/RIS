using CrackHash.Worker.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// HTTP клиент для Manager
builder.Services.AddHttpClient("ManagerClient", client => 
{
    client.BaseAddress = new Uri("http://manager/");
});

builder.Services.AddSingleton<WorkerTaskService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();