namespace SATSolver
{
    using System.Collections;

    public class Formula : IEnumerable
    {
        public Formula()
        {
            this.Clauses = new List<Clause>();
        }

        public Formula(Clause clause)
        {
            this.Clauses = new List<Clause>() { clause };
        }

        public Formula(Formula formula)
        {
            this.Clauses = new List<Clause>();
            foreach (Clause clause in formula)
            {
                this.Add(new Clause(clause.Data));
            }
        }

        public int Count => this.Clauses.Count;

        public List<Clause> Clauses { get; set; }

        public Clause this[int key]
        {
            get => this.Clauses[key];
        }

        public void Add(Clause clause)
        {
            this.Clauses.Add(clause);
        }

        public void Remove(Clause clause)
        {
            this.Clauses.Remove(clause);
        }

        public IEnumerator GetEnumerator()
        {
            return new FormulaEnumerator(this.Clauses.ToArray());
        }

        private class FormulaEnumerator : IEnumerator
        {
            private Clause[] clauselist;
            private int position = -1;

            public FormulaEnumerator(Clause[] list)
            {
                this.clauselist = list;
            }

            public object Current
            {
                get
                {
                    try
                    {
                        return this.clauselist[this.position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            public bool MoveNext()
            {
                this.position++;
                return this.position < this.clauselist.Length;
            }

            public void Reset()
            {
                this.position = -1;
            }
        }
    }
}
