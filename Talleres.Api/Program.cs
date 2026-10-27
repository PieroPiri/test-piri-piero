using Talleres.Logica;
using Talleres.Logica.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(new TallerRepository("talleres.json"));
builder.Services.AddSingleton(new InscripcionRepository("inscripciones.json"));
builder.Services.AddSingleton<InscripcionService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("PermitirFrontend");
app.UseAuthorization();
app.MapControllers();

app.Run();
