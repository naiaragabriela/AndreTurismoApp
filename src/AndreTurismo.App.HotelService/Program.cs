﻿using AndreTurismo.App.HotelService.Data;
using Microsoft.EntityFrameworkCore;
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AndreTurismoAppHotelServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AndreTurismoAppHotelServiceContext") ?? throw new InvalidOperationException("Connection string 'AndreTurismoAppHotelServiceContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
