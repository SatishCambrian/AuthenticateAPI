
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Authenticated.Data;
using AuthenticateWebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


builder.Services.AddDbContext<SecurityDbContext>(options => options.UseInMemoryDatabase("ApplicationDB"));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("SecurityDB"));


builder.Services.AddAuthorization();


builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<SecurityDbContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

app.MapIdentityApi<IdentityUser>();
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


