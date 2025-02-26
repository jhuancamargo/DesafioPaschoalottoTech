using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using PaschoalottoBackend.Modelos;
using PaschoalottoBackend.Servicos;
using Microsoft.OpenApi.Models;





var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient<ServicoUsuario>();

builder.Services.AddDbContext<ContextoPaschoalotto>(options =>
    options.UseNpgsql(builder.Configuration["ConnectionStrings:ConexaoPadrao"]));

// Configura o CORS -- lugar onde se permite a conexão com o front-end 
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configura o Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Paschoalotto API", Version = "v1" });
});

// Adicionado o ServicoUsuario como um serviço
builder.Services.AddScoped<ServicoUsuario>();

var app = builder.Build();

// Configuração o pipeline de requisições
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Paschoalotto API V1");
    });
}

// Usa o CORS
app.UseCors("PermitirTudo");


app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();