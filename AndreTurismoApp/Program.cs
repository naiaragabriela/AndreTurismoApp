using AndreTurismoApp.ExternalService;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ExternalAddressService>();
builder.Services.AddSingleton<ExternalCityService>();
builder.Services.AddSingleton<ExternalClientService>();
builder.Services.AddSingleton<ExternalHotelService>();
builder.Services.AddSingleton<ExternalTicketService>();
builder.Services.AddSingleton<ExternalPackageService>();
builder.Services.AddSingleton<ExternalTicketService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
