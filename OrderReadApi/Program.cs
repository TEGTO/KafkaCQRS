using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using OrderApi.Common;
using OrderReadApi;
using OrderReadApi.Commands.GetOrderById;
using OrderReadApi.Commands.GetOrders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AssemblyReference.Assembly);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(AssemblyReference));

ValidatorOptions.Global.LanguageManager.Enabled = false;

builder.Services.AddMassTransit(c =>
{
    c.SetKebabCaseEndpointNameFormatter();

    c.AddConsumers(typeof(Program).Assembly);

    c.AddRequestClient<GetOrderByIdQuery>();
    c.AddRequestClient<GetOrdersQuery>();

    c.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddHealthChecks();

#region [Kafka]

builder.Services.AddScoped<IKafkaConsumer, KafkaConsumer>();

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

await app.RunAsync();
