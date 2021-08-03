using Hashers.Interfaces;
using Hashers.Services;
using Isopoh.Cryptography.Argon2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrypt;
using System;
using System.IO;
using System.Text;

namespace Hashers.Examples
{
    public static class Program
    {
        private static void ConfigureService(this IServiceCollection services)
        {
            var configuration = GetConfigurationEnvironment();

            services.AddScoped<IArgon2idHasher>((_) => new Argon2idHasher(GetArgon2Config()));
            services.AddScoped<IBCryptHasher>((_) => new BCryptHasher(configuration["BCrypt:Salt"]));
            services.AddScoped<ISCryptHasher>((_) => new SCryptHasher(new ScryptEncoder()));
            services.AddScoped<ISha1Hasher, Sha1Hasher>();
        }

        private static ServiceProvider GetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureService();

            return serviceCollection.BuildServiceProvider();
        }

        private static IConfiguration GetConfigurationEnvironment()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false);

            
            return builder.Build();
        }

        private static Argon2Config GetArgon2Config()
        {
            var configuration = GetConfigurationEnvironment();

            return new Argon2Config
            {
                Type = Argon2Type.DataIndependentAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = int.Parse(configuration["Argon2:TimeCost"]), // time de generate hash
                MemoryCost = int.Parse(configuration["Argon2:MemoryCost"]), // memory size
                Lanes = int.Parse(configuration["Argon2:Lanes"]), // iterations
                Threads = Environment.ProcessorCount,
                Salt = Encoding.UTF8.GetBytes(configuration["Argon2:Salt"]),
                HashLength = 20
            };
        }

        static void Main(string[] args)
        {
            var serviceProvider = GetServiceProvider();
            
            string myPassword = "Password to hash!";

            #region Argon2

            var argon2idHasher = serviceProvider.GetService<IArgon2idHasher>();
            Func<string> argon2HashDelegate = () => argon2idHasher.Hash(myPassword);
            ShowHashProcessData("Argon2", myPassword, argon2HashDelegate);

            #endregion

            #region BCrypt

            var bcryptHasher = serviceProvider.GetService<IBCryptHasher>();
            Func<string> bcryptDelegate = () => bcryptHasher.Hash(myPassword);
            ShowHashProcessData("BCrypt", myPassword, bcryptDelegate);

            #endregion


            #region SCrypt

            var scryptHasher = serviceProvider.GetService<ISCryptHasher>();
            Func<string> scryptDelegate = () => scryptHasher.Hash(myPassword);
            ShowHashProcessData("SCrypt", myPassword, scryptDelegate);

            #endregion

            #region Sha1

            var sha1Hasher = serviceProvider.GetService<ISha1Hasher>();
            Func<string> sha1Delegate = () => sha1Hasher.Hash(myPassword);
            ShowHashProcessData("Sha1", myPassword, sha1Delegate);

            #endregion

            Console.ReadKey();
        }

        private static void ShowHashProcessData(string algorithmName, string password, Func<string> hashDelegate)
        {
            var startTime = DateTime.Now;
            var hashedPassword = hashDelegate.Invoke();
            var endTime = DateTime.Now;

            double timeToExecuteInSeconds = endTime.Subtract(startTime).TotalSeconds;

            Console.WriteLine(" -> {0} Algorithm\n", algorithmName);
            Console.WriteLine(" -> Time to execute: {0:0.00} seconds\n", timeToExecuteInSeconds);
            Console.WriteLine(" -> Time to execute: {0:0.00} seconds\n", timeToExecuteInSeconds);
            Console.WriteLine(" -> Password to hash: {0}\n", password);
            Console.WriteLine(" -> Hashed password: {0}\n", hashedPassword);
            Console.WriteLine("--------------------------------------------------\n\n\n");
        }
    }
}
