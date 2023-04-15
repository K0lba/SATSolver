// See https://aka.ms/new-console-template for more information

using SATSolver;
using System.Diagnostics;

Console.WriteLine("Hello, World!");

SAT sat = new SAT(@"C:\Users\aruds\source\repos\SATSolver\SatTests\Problems\sat300.cnf");
var timer = new Stopwatch();
timer.Start();
sat.Solve();
var elapsed = timer.Elapsed;
Console.WriteLine($"{elapsed.TotalSeconds:00}ss::{elapsed.Milliseconds:00}ms");
