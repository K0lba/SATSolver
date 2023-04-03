// See https://aka.ms/new-console-template for more information

using SATSolver;

Console.WriteLine("Hello, World!");

SAT sat = new SAT(@"C:\Users\aruds\source\repos\SATSolver\SatTests\Problems\sat7909.cnf");
sat.Solve();