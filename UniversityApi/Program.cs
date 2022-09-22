//1. Usings to work with Entity framework
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UniversityApi;
using UniversityApi.DataAccess;
using UniversityApi.Services;

var builder = WebApplication.CreateBuilder(args);


//2. Connection with SQL Server
const string CONNECTIONNAME = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

//3. Add context
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));

//7. Add services of JWT Authorization
builder.Services.AddJwtTokenServices(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();

//4. Add custom services (folder services)
builder.Services.AddScoped<IStudentsService, StudentsService>();
//TODO: Add all the services we create

//8. Add AUthorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//9. CONFIG SWAGGER TO TAKE CARE OF JWT AUTHORIZATION
builder.Services.AddSwaggerGen(c =>
{
    // api title, custumize for each API; c.SwaggerDoc("v1", new OpenApiInfo { Title = "Login & Registration API", Version = "v1", Description = "API de login y registro con JWT/ Login and registration API with JWT" });

    //DEFINING SECURITY
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insertar token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


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
