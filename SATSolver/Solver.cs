namespace SATSolver
{
    internal class Solver
    {
        public static bool DPLL(Clauses clauses, Clause resClause)
        {
            Unit_Propagate(resClause, clauses);
            Pure_Literal_Assign(resClause, clauses);

            if (clauses.Data.Count == 0)
            {
                return true;
            }

            foreach (var clause in clauses.Data)
            {
                if (clause.Data.Count == 0)
                {
                    return false;
                }
            }

            var clauseCopy = new Clauses();
            var clauseNegativeCopy = new Clauses();
            Chosoe_Literal(resClause, clauses, clauseCopy, clauseNegativeCopy);
            return DPLL(clauseCopy, resClause) || DPLL(clauseNegativeCopy, resClause);
        }

        private static void Chosoe_Literal(Clause resClause, Clauses clauses, Clauses clauseCopy, Clauses clauseNegativeCopy)
        {
            var new_lit = clauses.Data[0].Data[0];
            foreach (var clause in clauses.Data)
            {
                clauseCopy.Add(clause);
            }

            clauseCopy.Add(new Clause(new_lit));
            foreach (var clause in clauses.Data)
            {
                clauseNegativeCopy.Add(clause);
            }

            clauseNegativeCopy.Add(new Clause(-new_lit));
        }

        private static void Unit_Propagate(Clause l, Clauses clauses)
        {
            foreach (var clause in clauses.Data)
            {
                if (clause.Data.Count == 1)
                {
                    l.Add(clause.Data[0]);
                }
            }

            foreach (var lit in l.Data)
            {
                foreach (var c in clauses.Data.ToList())
                {
                    c.Data.Remove(-lit);
                    if (c.Data.Contains(lit))
                    {
                        clauses.Remove(c);
                    }
                }
            }
        }

        private static void Pure_Literal_Assign(Clause l, Clauses clauses)
        {
            HashSet<int> variables = new HashSet<int>();
            foreach (var clause in clauses.Data)
            {
                foreach (var variable in clause.Data)
                {
                    variables.Add(variable);
                }
            }

            var pure_l = new Clause();

            foreach (var let in variables)
            {
                if (variables.Contains(-let))
                {
                    continue;
                }

                l.Add(let);
                pure_l.Add(let);
            }

            var temp = new Clauses();
            foreach (var clause in clauses.Data.ToList())
            {
                foreach (var variable in clause.Data)
                {
                    if (pure_l.Contains(variable))
                    {
                        clauses.Remove(clause);
                    }
                }
            }
        }
    }
}
