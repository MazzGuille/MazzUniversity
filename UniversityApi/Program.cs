//1. Usings to work with Entity framework
using Microsoft.EntityFrameworkCore;
using UniversityApi.DataAccess;
using UniversityApi.Services;

var builder = WebApplication.CreateBuilder(args);


//2. Connection with SQL Server
const string CONNECTIONNAME = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

//3. Add context
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));


// Add services to the container.

builder.Services.AddControllers();

//4. Add custom services (folder services)
builder.Services.AddScoped<IStudentsService, StudentsService>();
//TODO: Add all the services we create

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//5. CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

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


//6. Tell app to use CORS
app.UseCors("CorsPolicy");


app.Run();
