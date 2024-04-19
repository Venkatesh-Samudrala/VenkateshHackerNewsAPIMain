using VenkateshHackerNewsAPI.Interfaces;
using VenkateshHackerNewsAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
string CORSOpenPolicy = "OpenCORDPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORSOpenPolicy,
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IHackerNews, HackerNewsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(CORSOpenPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
