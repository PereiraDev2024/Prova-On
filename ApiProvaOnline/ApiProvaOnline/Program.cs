using ApiProvaOnline.Application.Interfaces;
using ApiProvaOnline.Application.Services;
using ApiProvaOnline.Infrastructure.Data;
using ApiProvaOnline.Infrastructure.Repositories;
using ApiProvaOnline.Service;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 1. Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirLocalhost",
        policy =>
        {
            policy.WithOrigins("http://127.0.0.1:5500")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

// 2. Configurar Entity Framework Core com MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ProvaOnDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// 3. Registrar Repositories
builder.Services.AddScoped<IProvaRepository, ProvaRepository>();
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
builder.Services.AddScoped<IAdministrativoRepository, AdministrativoRepository>();
builder.Services.AddScoped<IPerguntaObjetivaRepository, PerguntaObjetivaRepository>();
builder.Services.AddScoped<IPerguntaRepository, PerguntaRepository>();
builder.Services.AddScoped<IAlunoRespostaObjetivaRepository, AlunoRespostaObjetivaRepository>();
builder.Services.AddScoped<IAlunoRespostaRepository, AlunoRespostaRepository>();

// 4. Registrar Services
builder.Services.AddScoped<ProvaService>();
builder.Services.AddScoped<AlunoService>();
builder.Services.AddScoped<ProfessorService>();
builder.Services.AddScoped<AdministrativoService>();
builder.Services.AddScoped<PerguntaObjetivaService>();
builder.Services.AddScoped<PerguntaService>();
builder.Services.AddScoped<AlunoRespostaObjetivaService>();
builder.Services.AddScoped<AlunoRespostaService>();
builder.Services.AddScoped<ICorrecaoService, CorrecaoService>();

// 5. Registrar FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 6. Ativar CORS antes da autorização
app.UseCors("PermitirLocalhost");

app.UseAuthorization();

app.MapControllers();

app.Run();
