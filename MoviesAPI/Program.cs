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
