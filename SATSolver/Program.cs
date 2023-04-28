// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using SATSolver;

SAT sat = new SAT(args[0]);
var timer = new Stopwatch();
timer.Start();
sat.Solve();
var elapsed = timer.Elapsed;
Console.WriteLine($"{elapsed.TotalSeconds:00}.{elapsed.Milliseconds:00}");
