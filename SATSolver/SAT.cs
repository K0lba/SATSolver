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
            /*var set = Solution.Data.ToHashSet<int>();
            foreach (int literal in set)
            {
                if (set.Contains(-literal))
                {
                    set.Remove(-literal);
                }
            }
            Solution.Data = set.ToList();*/
            //Solution.Data = Solution.Data.ToHashSet<int>().ToList();
            if (this.Solved && CheckSolution.Check(this.Solution, this.FormCop))
            {
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
                Console.WriteLine("UNSAT");
            }
        }
    }
}
