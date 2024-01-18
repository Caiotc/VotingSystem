// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using VotingSystem.EFCore;

Console.WriteLine("Hello, World!");

var ctx = new AppDbContext();

var fruit = ctx.Fruits.FirstOrDefault();

Console.ReadLine();