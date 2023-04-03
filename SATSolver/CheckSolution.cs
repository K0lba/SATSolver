namespace SATSolver
{
    internal class CheckSolution
    {
        public static bool Check(Clause solution, Formula formula)
        {
            var literals = solution.Data.ToHashSet();
            if (literals.Any(literal => literals.Contains(-literal)))
            {
                return false;
            }

            foreach (Clause clause in formula)
            {
                foreach (int literal in clause)
                {
                    if (!literals.Contains(literal) && !literals.Contains(-literal))
                    {
                        literals.Add(literal);
                        solution.Add(literal);
                    }
                }
            }

            return formula.Clauses.All(clause => clause.Data.Any(literal => literals.Contains(literal)));
        }
    }
}
