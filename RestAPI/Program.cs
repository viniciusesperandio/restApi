using Microsoft.EntityFrameworkCore;
using RestAPI.Models;
using RestAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<CandidateContext>(opt =>
    opt.UseInMemoryDatabase("CandidateList"));
builder.Services.AddDbContext<CompanyContext>(opt =>
    opt.UseInMemoryDatabase("CompanyList"));
builder.Services.AddDbContext<ResultContext>(opt =>
    opt.UseInMemoryDatabase("ResultList"));

builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<IResultService, ResultService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "RestAPI" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();