namespace SATSolver
{
    internal class DIMACS_parser
    {
        public static Formula DIMACS(string path)
        {
            Formula formula = new Formula();
            using (StreamReader file = new StreamReader(path))
            {
                string? clauseLine;
                char variables;
                char clauses;

                while ((clauseLine = file.ReadLine()) != null)
                {
                    if (clauseLine[0] == 'c')
                    {
                        continue;
                    }

                    if (clauseLine[0] == 'p')
                    {
                        variables = clauseLine[6];
                        clauses = clauseLine[8];
                        continue;
                    }

                    Clause clause = new Clause();
                    foreach (var c in clauseLine.Split(" "))
                    {
                        if (Convert.ToInt32(c) == 0)
                        {
                            continue;
                        }

                        clause.Add(Convert.ToInt32(c));
                    }

                    formula.Add(clause);
                    clause = new Clause();
                }
            }

            return formula;
        }
    }
}
