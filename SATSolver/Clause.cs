namespace SATSolver
{
    using System.Collections;

    public class Clause : IEnumerable
    {
        public Clause()
        {
            this.Data = new List<int>();
        }

        public Clause(int clause)
        {
            this.Data = new List<int>() { clause };
        }

        public Clause(List<int> clause)
        {
            this.Data = new List<int>(clause);
        }

        public int Count => this.Data.Count;

        public List<int> Data { get; set; }

        public int this[int key]
        {
            get => this.Data[key];
        }

        public void Add(int clause)
        {
            this.Data.Add(clause);
        }

        public void Add(List<int> clause)
        {
            if (this.Data != null)
            {
                this.Data.AddRange(clause);
            }
            else
            {
                this.Data = clause;
            }
        }

        public bool Contains(int n)
        {
            return this.Data.Contains(n);
        }

        public IEnumerator GetEnumerator()
        {
            return new ClauseEnumerator(this.Data.ToArray());
        }

        public void Remove(int literal)
        {
            this.Data.Remove(literal);
        }

        private class ClauseEnumerator : IEnumerator
        {
            private int[] literalslist;
            private int position = -1;

            public ClauseEnumerator(int[] list)
            {
                this.literalslist = list;
            }

            public object Current
            {
                get
                {
                    try
                    {
                        return this.literalslist[this.position];
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
                return this.position < this.literalslist.Length;
            }

            public void Reset()
            {
                this.position = -1;
            }
        }
    }
}
