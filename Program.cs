using api_escola.Model;
using api_escola.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BancoDeDados>(options => options.UseInMemoryDatabase("BancoDeDados"));

// Add services to the container.
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

app.MapGet("/escolas", async (BancoDeDados Db) => {
    return await Db.Escolas.ToListAsync();
});

app.MapGet("/escolas{id}", async (BancoDeDados Db, Guid id) => {
    if (await Db.Escolas.FindAsync(id) is Escola escola)
    {
        return Results.Ok(escola);
    }

    return Results.NotFound();
   
});

app.MapPost("/escolas", async (BancoDeDados Db, Escola escola) =>
{
    try
    {
        Db.Escolas.Add(escola);
        await Db.SaveChangesAsync();
        return Results.Created($"/escola/{escola.Id}", escola);
    }
    catch (Exception ex) 
    {
        return Results.Problem(ex.Message);
    }
    
});

app.MapPut("/escolas", async (BancoDeDados Db, Guid id, Escola escolaInput) =>
{
    var escola = Db.Escolas.Find(id);

    if (escola is Escola)
    {
        escola.Id = escolaInput.Id;
        escola.Nome = escolaInput.Nome;
        escola.NumeroDeTurmas = escolaInput.NumeroDeTurmas;
        return Results.NoContent();
    }
   
    return Results.NotFound();
});

app.MapDelete("/escolas", async (BancoDeDados Db, Guid id) =>
{
    if (await Db.Escolas.FindAsync(id) is Escola escolaRemover)
    {
        Db.Escolas.Remove(escolaRemover);
        await Db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();

});

app.Run();

