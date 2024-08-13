using Shorturler;
using Shorturler.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ShortRequestHandler>();
builder.Services.AddScoped<IUrlShortener, UrlShortener>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();