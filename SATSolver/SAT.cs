namespace SATSolver
{
    public class SAT
    {
        private string path;

        public SAT(string path)
        {
            this.path = path;
            this.Solution = new Clause();
            this.Formula = DIMACS_parser.DIMACS(this.path);
            this.FormCop = DIMACS_parser.DIMACS(this.path);
        }

        public Clause Solution { get; set; }

        public bool Solved { get; set; }

        public Formula Formula { get; set; }

        private Formula FormCop { get; set; }

        public void Solve()
        {
            (this.Solved, this.Solution) = Solver.DPLL(this.Formula, this.Solution);
            if (this.Solved && CheckSolution.Check(this.Solution, this.FormCop))
            {
                Console.WriteLine("s SATISFIABLE");
                Console.Write("v ");
                foreach (var r in this.Solution.Data)
                {
                    Console.Write(r.ToString() + " ");
                }

                Console.Write("0");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("UNSAT");
            }
        }
    }
}
