using LMCEvents.Core.Interfaces;
using LMCEvents.Core.Service;
using LMCEvents.Filters;
using LMCEvents.Infra.Data.Repositories;
using LMCEvents.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var key = Encoding.ASCII.GetBytes(builder.Configuration["secretKey"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) 
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true, 
            ValidateAudience = true, 
            ValidIssuer = "APIClientes.com",
            ValidAudience = "APIEvents.com"
        };
    });

builder.Services.AddSwaggerGen(setup =>
{

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put ONLY your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options =>
{
    options.Filters.Add<ExceptionsFilters>();
}
);

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICityEventService, CityEventService>();
builder.Services.AddScoped<ICityEventRepository, CityEventRepository>();
builder.Services.AddScoped<IEventDTOMapper, EventDTOMapper>();
builder.Services.AddScoped<IBookingDTOMapper, BookingDTOMapper>();
builder.Services.AddScoped<IEventReservationService, EventReservationService>();
builder.Services.AddScoped<IEventReservationRepository, EventReservationRepository>();

builder.Services.AddScoped<ValidateBookingIdActionFilter>();
builder.Services.AddScoped<ValidateEventIdActionFilter>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
