using ApachePulsar.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IPulsarService, PulsarService>();
builder.Services.AddScoped<IPulsarConsumerService, PulsarConsumerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//  app.UseHttpsRedirection();

app.MapControllers();
app.Run();
