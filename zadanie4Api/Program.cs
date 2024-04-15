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

app.MapPut("animals", (IMockDb mockDb, Animal animal) =>
{
    return Results.Ok(mockDb.AddAnimal(animal));
});

app.MapGet("animals/{id}", (IMockDb mockDb, int id) =>
{
    return Results.Ok(mockDb.GetAnimalDetails(id));
});

app.MapDelete("animals/{id}", (IMockDb mockDb, int id) =>
{
    return Results.Ok(mockDb.RemoveAnimal(id));
});

app.Run();

