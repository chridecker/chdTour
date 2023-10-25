// See https://aka.ms/new-console-template for more information
using chdTour.Persistence.EF;
using Microsoft.EntityFrameworkCore;

var cts = new CancellationTokenSource();

Console.WriteLine("Hello, World!");


using var ctx = new chdTourContext();
await ctx.Database.EnsureCreatedAsync(cts.Token);

if ((await ctx.Database.GetPendingMigrationsAsync(cts.Token)).Any())
{
    await ctx.Database.EnsureDeletedAsync(cts.Token);
    await ctx.Database.MigrateAsync(cts.Token);
}
ctx.Persons.Add(new chdTour.DataAccess.Contracts.Domain.Person
{
    FirstName = "Christoph",
    LastName = "Decker",
    Created = DateTime.Now,
});
await ctx.SaveChangesAsync(cts.Token);

