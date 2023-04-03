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

            var clauseCopy = new Formula();
            var clauseNegativeCopy = new Formula();
            var resCopy = new Clause();
            var resCopy1 = new Clause();
            Chosoe_Literal(solution, formula, clauseCopy, clauseNegativeCopy, resCopy, resCopy1);
            var(solve, res) = DPLL(clauseCopy, resCopy);
            if (solve)
            {
                return (solve, res);
            }

            return DPLL(clauseNegativeCopy, resCopy1);
        }

        private static void Chosoe_Literal(Clause solution, Formula clauses, Formula clauseCopy, Formula clauseNegativeCopy, Clause resCopy, Clause resCopy1)
        {
            var new_lit = clauses.Clauses[0].Data[0];

            foreach (Clause clause in clauses)
            {
                clauseCopy.Add(new Clause(clause.Data));
                clauseNegativeCopy.Add(new Clause(clause.Data));
            }

            clauseCopy.Add(new Clause(new_lit));
            clauseNegativeCopy.Add(new Clause(-new_lit));
            solution.Data = solution.Data.ToHashSet<int>().ToList();
            foreach (int literal in solution)
            {
                resCopy.Add(literal);
                resCopy1.Add(literal);
            }
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
