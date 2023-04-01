namespace SATSolver
{
    internal class Clauses
    {
        public Clauses()
        {
            this.Data = new List<Clause>();
        }

        public Clauses(Clause clause)
        {
            this.Data = new List<Clause>() { clause };
        }

        public List<Clause> Data { get; set; }

        public void Add(Clause clause)
        {
            this.Data.Add(clause);
        }

        public void Remove(Clause clause)
        {
            this.Data.Remove(clause);
        }
    }
}
