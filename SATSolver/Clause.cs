namespace SATSolver
{
    public class Clause
    {
        public Clause()
        {
            this.Data = new List<int>();
        }

        public Clause(int clause)
        {
            this.Data = new List<int>() { clause };
        }

        public List<int> Data { get; set; }

        public int this[int key]
        {
            get => this.Data[key];
        }

        public void Add(int clause)
        {
            this.Data.Add(clause);
        }

        public bool Contains(int n)
        {
            return this.Data.Contains(n);
        }
    }
}
