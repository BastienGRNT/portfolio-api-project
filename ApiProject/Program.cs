namespace ApiProject;

public class Program
{
    public static void Main(string[] args)
    {
        DotNetEnv.Env.Load("./config/bdd.env");
        
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()   
                    .AllowAnyHeader()  
                    .AllowAnyMethod(); 
            });
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSwagger(c =>
        {
            c.RouteTemplate = "swagger/{documentName}/swagger.json";
        });
        app.UseSwaggerUI();

        app.UseCors("AllowAll");

        app.MapControllers();

        app.Run();
    }
}