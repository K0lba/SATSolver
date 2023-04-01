using SATSolver;

namespace SatTests
{
    public class Tests
    {
        private string path = @"Problems//";

        [Test]
        public void UNSAT1()
        {
            SAT sat = new SAT(path + "unsat1.cnf");
            sat.Solve();
            Assert.That(sat.Solved, Is.EqualTo(false));
        }

        [Test]
        public void UNSAT2()
        {
            SAT sat = new SAT(path + "unsat2.cnf");
            sat.Solve();
            Assert.That(sat.Solved, Is.EqualTo(false));
        }

        [Test]
        public void SAT1()
        {
            SAT sat = new SAT(path + "sat1.cnf");
            sat.Solve();
            List<int> expect = new List<int>() { 1, -2, 3 };
            Assert.That(sat.Solution.Data, Is.EqualTo(expect));
        }

        [Test]
        public void SAT2()
        {
            SAT sat = new SAT(path + "sat2.cnf");
            sat.Solve();
            List<int> expect = new List<int>() { 2, 1 };
            Assert.That(sat.Solution.Data, Is.EqualTo(expect));
        }

        [Test]
        public void SAT3()
        {
            SAT sat = new SAT(path + "sat3.cnf");
            sat.Solve();
            List<int> expect = new List<int>() { 8, 6, 4, -5, -2 };
            Assert.That(sat.Solution.Data, Is.EqualTo(expect));
        }

        [Test]
        public void SAT4()
        {
            SAT sat = new SAT(path + "sat4.cnf");
            sat.Solve();
            List<int> expect = new List<int>() { -3, 1, -2 };
            Assert.That(sat.Solution.Data, Is.EqualTo(expect));
        }
    }
}