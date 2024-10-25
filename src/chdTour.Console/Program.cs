// See https://aka.ms/new-console-template for more information
using chdTour.Persistence.EF;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");


var ctx = new chdTourContext(new DbContextOptionsBuilder<chdTourContext>().Options);