namespace SATSolver
{
    public class SAT
    {
        private string path;

        public SAT(string path)
        {
            this.path = path;
            this.Solution = new Clause();
        }

        public Clause Solution { get; set; }

        public bool Solved { get; set; }

        public void Solve()
        {
            Clauses formula = DIMACS_parser.DIMACS(this.path);
            if (Solver.DPLL(formula, this.Solution))
            {
                this.Solved = true;
                Console.WriteLine("s SATISFIABLE");
                Console.Write("v ");
                foreach (var r in this.Solution.Data)
                {
                    Console.Write(r.ToString() + " ");
                }

                Console.Write("0");
            }
            else
            {
                this.Solved=false;
                Console.WriteLine("UNSAT");
            }
        }
    }
}
