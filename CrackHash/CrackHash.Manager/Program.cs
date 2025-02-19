using System.Text.Json.Serialization;
using CrackHash.Manager.Application.Services;
using CrackHash.Manager.Jobs;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddQuartz(options =>
{
    var crackTaskDevideJobKey = JobKey.Create(nameof(CrackTaskDevideJob));
    options
        .AddJob<CrackTaskDevideJob>(crackTaskDevideJobKey)
        .AddTrigger(
            trigger => trigger
                .ForJob(crackTaskDevideJobKey)
                .WithSimpleSchedule(
                    schedule => schedule
                        .WithIntervalInSeconds(1)
                        .RepeatForever()
                )
        );
});

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});


builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddSingleton<TaskManagerService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();



app.Run();