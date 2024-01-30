using ReddisSample1.Models;
using ReddisSample1.Services;
using ReddisSample1.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ConnectionHelper>();
builder.Services.AddSingleton<ICacheService, CacheService>();
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<ReddisDbSettings>(builder.Configuration.GetSection("ReddisConnection"));
builder.Services.AddSingleton<MongoDBService>();
builder.Services.AddSingleton<UpdateRedisCache>();
builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = "localhost:6379";

    options.InstanceName = "ReddisDemo";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
