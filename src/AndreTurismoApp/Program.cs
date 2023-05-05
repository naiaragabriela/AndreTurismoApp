using AndreTurismoApp.ExternalService;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ExternalAddressService>();
builder.Services.AddSingleton<ExternalCityService>();
builder.Services.AddSingleton<ExternalCustomerService>();
builder.Services.AddSingleton<ExternalHotelService>();
builder.Services.AddSingleton<ExternalTicketService>();
builder.Services.AddSingleton<ExternalPackageService>();
builder.Services.AddSingleton<ExternalTicketService>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
