using System.Text.Json.Serialization;
using Microsoft.AspNetCore.SignalR;
using Punto;
using Punto.objects;

class Program
{
    
    /**
     * EnterPoint du programme
     * Gère toute la partie du programme serveur qui envoye et écoute les requettes
     * <param name="args">Argument du programme</param>
     */
    static void Main(string[] args)
    {
        Console.WriteLine("Punto Client Start");

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", builder =>
            {
                builder.WithOrigins("http://punto.test")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });
        builder.Services
            .AddSignalR(o => { o.EnableDetailedErrors = true; })
            .AddJsonProtocol(options => { options.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; });
        
        // Permet d'ajouter l'instance Game tous au long du lifetime de l'app
        builder.Services.AddSingleton<Game>(serviceProvider =>
        {
            var hubContext = serviceProvider.GetRequiredService<IHubContext<ChatHub>>();
            return new Game(hubContext, 2);
        });
        var app = builder.Build();
        app.UseCors("AllowSpecificOrigin");
        app.MapHub<ChatHub>("/chathub");
        app.Run();
    }
}