using Confluent.Kafka;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using OrderApi.Common;
using OrderWriteApi;
using OrderWriteApi.Commands.CreateOrder;
using OrderWriteApi.Commands.UpdateOrder;
using OrderWriteApi.Services;

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

    c.AddRequestClient<CreateOrderCommand>();
    c.AddRequestClient<UpdateOrderCommand>();

    c.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddHealthChecks();

#region [Kafka]

var producerConfig = new ProducerConfig
{
    BootstrapServers = builder.Configuration[ConfigurationKeys.KafkaBootstrapServers],
    Acks = Acks.All,
    AllowAutoCreateTopics = true
};

builder.Services.AddSingleton(producerConfig);
builder.Services.AddSingleton<IKafkaProducerFactory, KafkaProducerFactory>();
builder.Services.AddSingleton<IProducer, KafkaProducer>();

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