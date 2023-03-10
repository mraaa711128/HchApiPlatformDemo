using HchApiPlatform.Biz;
using HchApiPlatform.Extensions;
using HchApiPlatform.ModelBinders;
using Oracle.ManagedDataAccess.Client;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt => {
    opt.ModelBinderProviders.Insert(0, new EmptyQueryStringModelBinderProvider());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConfig(builder.Configuration);
builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddScoped<AdmitPatientBiz>();
builder.Services.AddScoped<AdmitBedBiz>();
builder.Services.AddScoped<AdmitBedStatBiz>();

builder.Services.AddQuartzJobs(builder.Configuration);

builder.Services.AddNLog(builder.Configuration);

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
