using Meli.DataAccess.Interfaces;
using Meli.DataAccess;
using Meli.Repository;
using Meli.Processor.Interfaces;
using Meli.Processor;
using Meli.Proxies;
using Meli.Proxies.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient(typeof(ISqlDataAccess<>),typeof(SqlDataAccess<>));
builder.Services.AddScoped<IDataContext, SqlDataContext>();
builder.Services.AddScoped<ISqlDataContext, SqlDataContext>();
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IHttpClientProxy, HttpClientProxy>();
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICouponClient, CouponClient>();
builder.Services.AddScoped<ICouponProxy, CouponProxy>();
builder.Services.AddScoped<ICouponProcessor, CouponProcessor>();
builder.Services.AddScoped<IProductProcessor, ProductProcessor>();
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
