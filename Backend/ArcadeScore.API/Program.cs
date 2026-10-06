using ArcadeScore.Infra.Ioc;

// builder é onde configuramos a aplicação
var builder = WebApplication.CreateBuilder(args);

// Habilita o uso de controllers
builder.Services.AddControllers();

// Gera a descrição da API
builder.Services.AddOpenApi();

// Liga as interfaces às classes (método que criamos no projeto Ioc)
builder.Services.AddInfrastructure();

// Libera o Angular (que roda em outra porta) para chamar a API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Porta padrão do Angular
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// app é a aplicação pronta
var app = builder.Build();

// Faz o bloco rodar só na minha máquina
if (app.Environment.IsDevelopment())
{
    // Publica o JSON da API
    app.MapOpenApi(); 

    // Mostra a página visual de testes, apontando para esse JSON
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "ArcadeScore API v1");
    });
}

app.UseHttpsRedirection();

// Ativa a regra Cors, tem q vir antes de UseAuthorization()  e MapControllers()
app.UseCors("AllowAngular");

app.UseAuthorization();

// Conecta as URLs aos controllers que vamos criar
app.MapControllers();

app.Run();