using EstatePredict.Api.Data;
using Microsoft.EntityFrameworkCore;
using EstatePredict.Api.Data;
using EstatePredict.Api.Interfaces;
using EstatePredict.Api.Services;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<EstatePredictContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//custom servisi

builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IPropertyTypeService, PropertyTypeService>();
builder.Services.AddHttpClient<IPredictionService, PredictionService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:8000/");
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => policy.WithOrigins("http://localhost:5173") // Promijeni po potrebi
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

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
app.UseCors("AllowReact");



app.MapControllers();


app.Run();

