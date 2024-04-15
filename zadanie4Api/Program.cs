using RestApiApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMockDb, MockDb>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("animals", (IMockDb mockDb) =>
{
    return Results.Ok(mockDb.GetAllAnimals());
});


app.MapGet("animals/{id}", (IMockDb mockDb, int id) =>
{
    var animal = mockDb.GetAnimalDetails(id);
    if (animal is null) return Results.NotFound();

    return Results.Ok(animal);
});


app.MapPost("animals", (IMockDb mockDb, Animal animal) =>
{
    mockDb.AddAnimal(animal);
    return Results.Created("", animal);
});


app.MapDelete("animals/{id}", (IMockDb mockDb, int id) =>
{
    var animal = mockDb.RemoveAnimal(id);
    if (animal is null) return Results.NotFound();
    
    return Results.Ok(animal);
});

app.MapPut("animals", (IMockDb mockDb, int id, Animal animal) =>
{
    var ani = mockDb.ReplaceAnimal(id, animal);

    return Results.Created("", ani);
});

app.Run();