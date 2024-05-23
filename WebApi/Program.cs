using MongoDB.Bson;
using MongoDB.Driver;


var builder = WebApplication.CreateBuilder(args);

var movieDatabaseConfigSection =
builder.Configuration.GetSection("DatabaseSettings");
builder.Services.Configure<DatabaseSettings>(movieDatabaseConfigSection);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/check", (Microsoft.Extensions.Options.IOptions<DatabaseSettings> options) => {
    try{
        var mongoDbConnectionString = options.Value.ConnectionString;
        MongoClient dbClient = new MongoClient(mongoDbConnectionString);
        var dbList = dbClient.ListDatabaseNames().ToList();
        return "Zugriff auf MongoDB ok! " + dbList.ToJson();
    } catch {
        throw new Exception("Could not connect to MongoDb");    
    }
});

app.Run();
