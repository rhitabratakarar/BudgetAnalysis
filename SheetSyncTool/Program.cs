using Microsoft.Extensions.Configuration;
using System.Reflection;

IConfiguration configuration = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly()).Build();

string? API_KEY = configuration["SHEETS_ONLY_API_KEY"];

if (API_KEY != null)
    Console.WriteLine(API_KEY);
