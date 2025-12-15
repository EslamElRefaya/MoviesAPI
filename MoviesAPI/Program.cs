using MoviesAPI.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//add inject userManager and roleManager identity to application
builder.Services.AddIdentity<ApplicationUsers, IdentityRole>()
                            .AddEntityFrameworkStores<ApplicationDbContext>();

// this add Custom JWT Authentication
builder.Services.AddJWTAuthentication(builder.Configuration);


builder.Services.AddSwaggerGenAuthentication();
/*
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "New Name to My Api",
        Description = "My Frist Api",
        Contact = new OpenApiContact
        {
            Name = "Eslam El Refaye",
            Email = "eslammhmoudrefay14@gmail.com",
            Url = new Uri("https://www.google.com"),
        },
        License = new OpenApiLicense
        {
            Name = "My License",
            Url = new Uri("https://wwww.google.com"),
        }
    });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type= SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat="JWT",
        In=ParameterLocation.Header,
        Description="Enter your JWT key"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
                Name="Bearer",
                In=ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
*/

//add Connection String
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                       options.UseSqlServer(ConnectionString));

//add cors 1---> add in Services
builder.Services.AddCors();

//add services && Repository
builder.Services.AddScoped<GenreRepository>(); // because GenresService uses it directly
builder.Services.AddScoped<IMoviesAPIRepository<Movie>, MovieRepository>();
builder.Services.AddScoped<IGenresService, GenresService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();



// add Auto Mapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

//add cors 1---> add in Middle ware
app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
