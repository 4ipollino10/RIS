using System.Text.Json.Serialization;
using CrackHash.Manager.Application.Jobs;
using CrackHash.Manager.Application.Services;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddQuartz(options =>
{
    var crackTaskDivideJobKey = JobKey.Create(nameof(CrackTaskDivideJob));
    options
        .AddJob<CrackTaskDivideJob>(crackTaskDivideJobKey)
        .AddTrigger(
            trigger => trigger
                .ForJob(crackTaskDivideJobKey)
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