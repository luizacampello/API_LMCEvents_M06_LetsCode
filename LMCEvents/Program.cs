using LMCEvents.Core.Interfaces;
using LMCEvents.Core.Service;
using LMCEvents.Filters;
using LMCEvents.Infra.Data.Repositories;
using LMCEvents.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICityEventService, CityEventService>();
builder.Services.AddScoped<ICityEventRepository, CityEventRepository>();
builder.Services.AddScoped<IEventDTOMapper, EventDTOMapper>();
builder.Services.AddScoped<IBookingDTOMapper, BookingDTOMapper>();
builder.Services.AddScoped<IEventReservationService, EventReservationService>();
builder.Services.AddScoped<IEventReservationRepository, EventReservationRepository>();

builder.Services.AddScoped<ValidateBookingIdActionFilter>();
builder.Services.AddScoped<ValidateEventIdActionFilter>();

//builder.Services.AddMvc(options =>
//{
//    options.Filters.Add<ExceptionsFilters>();
//}
//);

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
