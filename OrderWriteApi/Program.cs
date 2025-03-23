using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using OrderWriteApi;
using OrderWriteApi.Commands.CreateOrder;
using OrderWriteApi.Commands.UpdateOrder;

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

app.Run();