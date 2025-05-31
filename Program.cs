var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
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

List<string> names = new() { "Bhomika", "Karshma", "Kashish", "Ukesh", "Daksh" };

var name = "Bhomika";
app.MapGet("/myName", () =>
{

    return name;
});

app.MapGet("/names", () =>
{

    return Results.Ok(names);
});

app.MapPost("/giveName", (NameRequest request) =>
{
    if (string.IsNullOrEmpty(request.Name))
    {
        return Results.BadRequest("Name Can't be empty");
    }
    names.Add(request.Name);
    return Results.Ok($"The name {request.Name} has been added");


});
app.MapPost("/removeName", (NameRequest request) =>
{
    var found = names.FirstOrDefault(n =>
    string.Equals(n, request.Name, StringComparison.OrdinalIgnoreCase));

    if (found == null)
    {
        return Results.NotFound($"Name '{request.Name}' not found");
    }

    names.Remove(found);
    return Results.Ok($"The name {request.Name} has been removed");


});
app.MapGet("/nameOnIndex/{index:int}", (int index) =>
    {
        if (index < 0 || index >= names.Count)
        {
            return Results.NotFound($"Index {index} is out of range");
        }
        return Results.Ok(names[index]);
    });

app.Run();

public class NameRequest
{
    public string Name { get; set; } = "bhumika";
}