using Meli.Processor.Interfaces;
using Meli.Processor;
using Meli.Proxies.Interfaces;
using Meli.Proxies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddScoped<IHttpClientFactory>();
builder.Services.AddScoped<IHttpClientProxy, HttpClientProxy>();
builder.Services.AddScoped<ICouponClient,CouponClient>();
builder.Services.AddScoped<ICouponProxy,CouponProxy>();
builder.Services.AddScoped<ICouponProcessor,CouponProcessor>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(route => route.LowercaseUrls = true);
builder.Services.AddHttpClient();
builder.Services.AddSwaggerGen();
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
