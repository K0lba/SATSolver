namespace SATSolver
{
    internal class Solver
    {
        public static(bool, Clause) DPLL(Formula formula, Clause solution)
        {
            Unit_Propagate(solution, formula);
            Pure_Literal_Assign(solution, formula);

            if (formula.Count == 0)
            {
                return (true, solution);
            }

            foreach (Clause clause in formula)
            {
                if (clause.Count == 0)
                {
                    return (false, new Clause());
                }
            }

            // literal choice
            var new_lit = formula.Clauses[0].Data[0];

            // Remove repetitive
            solution.Data = solution.Data.ToHashSet<int>().ToList();

            var formulaPositive = new Formula(formula)
            {
                new Clause(new_lit)
            };
            var solutionPositive = new Clause(solution);
            var(solve, result) = DPLL(formulaPositive, solutionPositive);
            if (solve)
            {
                return (solve, result);
            }

            formula.Add(new Clause(-new_lit));
            return DPLL(formula, solution);
        }

        private static void Unit_Propagate(Clause solution, Formula formula)
        {
            var temp = new HashSet<int>();
            foreach (Clause clause in formula)
            {
                if (clause.Count == 1)
                {
                    temp.Add(clause[0]);
                    solution.Add(clause[0]);
                }
            }

            foreach (int lit in temp)
            {
                foreach (Clause clause in formula)
                {
                    clause.Remove(-lit);
                    if (clause.Contains(lit))
                    {
                        formula.Remove(clause);
                    }
                }
            }

            solution.Data = solution.Data.ToHashSet().ToList();
        }

        private static void Pure_Literal_Assign(Clause solution, Formula formula)
        {
            HashSet<int> variables = new HashSet<int>();
            foreach (Clause clause in formula)
            {
                clause.Data.ForEach(x => variables.Add(x));
            }

            var pure_l = new Clause();

            foreach (int literal in variables)
            {
                if (variables.Contains(-literal))
                {
                    continue;
                }

                solution.Add(literal);
                pure_l.Add(literal);
            }

            foreach (Clause clause in formula)
            {
                foreach (int literal in clause)
                {
                    if (pure_l.Contains(literal))
                    {
                        formula.Remove(clause);
                    }
                }
            }
        }
    }
}
