var builder = WebApplication.CreateBuilder(args);

// 1. ADD SWAGGER SERVICES
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // <--- Make sure this is here!

var app = builder.Build();

// 2. ENABLE SWAGGER MIDDLEWARE
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   // Generates the JSON spec
    app.UseSwaggerUI(); // Serves the HTML webpage at /swagger
}

app.UseAuthorization();
app.MapControllers();

app.Run();
