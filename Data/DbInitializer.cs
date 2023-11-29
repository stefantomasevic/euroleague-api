﻿using Euroleague.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;

namespace Euroleague.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            try { 


            context.Database.EnsureCreated();
            Console.WriteLine("Database creation status: " + context.Database.EnsureCreated());


            if (!context.Teams.Any())
            {
                List<Team> teams = new List<Team>
                {
                      new Team
                      {
                            Name = "Partizan",
                            City = "Belgrade",
                            Country = "Serbia",
                            Coach = "Zeljko Obradovic",
                            Arena = "Beogradska arena",
                            LogoPath = "/images/Logos/KKPartizan.png",
                      },
                       new Team
                       {
                            Name = "Real Madrid",
                            City = "Madrid",
                            Country = "Spain",
                            Coach = "Pablo Laso",
                            Arena = "WiZink Center",
                            LogoPath = "/images//Logos/real_madrid_logo.png"
                       },
                       new Team
                       {
                            Name = "No Team",
                            City = "N/A",
                            Country = "N/A",
                            Coach = "N/A",
                            Arena = "N/A",
                            LogoPath = "/images/Logos/free.png",
                       }
                    };

                context.Teams.AddRange(teams);
                context.SaveChanges();
                if (!context.Players.Any())
                {
                    List<Player> players = new List<Player>
                        {
                        new Player
                        {
                            FirstName = "Alen",
                            LastName = "Smailagic",
                            Age = 35,
                            Position = "Point Guard",
                            Nationality = "Serbian",
                            ImagePath = "/images/Players/alen.png",
                            TeamId = context.Teams.FirstOrDefault(t => t.Name == "Partizan")?.Id ?? 0
                        },
                        new Player
                        {
                            FirstName = "Danilo",
                            LastName = "Andjusic",
                            Age = 34,
                            Position = "Shooting Guard",
                            Nationality = "Serbian",
                            ImagePath = "/images/Players/Danilo-Andjusic.png",
                            TeamId = context.Teams.FirstOrDefault(t => t.Name == "Partizan")?.Id ?? 0
                        },
                        new Player
                        {
                            FirstName = "Scottie",
                            LastName = "Wilbekin",
                            Age = 22,
                            Position = "Point Forward",
                            Nationality = "Slovenia",
                            ImagePath = "/images/Players/Scottie-Wilbekin.png",
                            TeamId = context.Teams.FirstOrDefault(t => t.Name == "Real Madrid")?.Id ?? 0
                        },
                        new Player
                        {
                            FirstName = "Kampaco",
                            LastName = "Fakundo",
                            Age = 25,
                            Position = "Bench",
                            Nationality = "Unknown",
                            ImagePath = "/images/Players/Fakundo.png",
                            TeamId = context.Teams.FirstOrDefault(t => t.Name == "No Team")?.Id ?? 0
                        }
                        };

                    context.Players.AddRange(players);
                    context.SaveChanges();

                }
            }

            } catch ( Exception ex)
            {
                Console.WriteLine("Error during database initialization: " + ex.Message);
                var innerException = ex.InnerException;

                // Ako postoji unutrašnji izuzetak
                if (innerException != null)
                {
                    Console.WriteLine("Inner Exception Details:");
                    Console.WriteLine(innerException.Message);

                    // Ispisi sve detalje unutrašnjeg izuzetka
                    Console.WriteLine(innerException.ToString());
                }
            }


        }   
     }
}       

