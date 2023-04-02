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
            var resCopy = new Clause();
            Chosoe_Literal(resClause, clauses, clauseCopy, clauseNegativeCopy, resCopy);
            return DPLL(clauseCopy, resCopy) || DPLL(clauseNegativeCopy, resCopy);
        }

        private static void Chosoe_Literal(Clause resClause, Clauses clauses, Clauses clauseCopy, Clauses clauseNegativeCopy, Clause resCopy)
        {
            var new_lit = clauses.Data[0].Data[0];
            foreach (var clause in clauses.Data)
            {
                var copy = new int[clause.Data.Count];
                clause.Data.CopyTo(copy);

                clauseCopy.Add(new Clause(copy.ToList()));
                clauseNegativeCopy.Add(new Clause(copy.ToList()));
            }

            var copyRes = new int[resClause.Data.Count];
            resClause.Data.CopyTo(copyRes);
            copyRes = copyRes.ToHashSet().ToArray();
            resCopy.Add(copyRes.ToList());

            clauseCopy.Add(new Clause(new_lit));
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

            //var temp = new Clauses();
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
