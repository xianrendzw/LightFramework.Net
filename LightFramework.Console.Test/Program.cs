using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Console.Test
{
    using Data;

    class Program
    {
        static void Main(string[] args)
        {
            Operand operand = Restrictions.Clause(SqlClause.Where)
                .Append(Restrictions.Equal("Name", "TomDeng"))
                .Append(Restrictions.And)
                .Append(Restrictions.Equal("Age", 30))
                .Append(Restrictions.And)
                .Append(Restrictions.Bracket(Bracket.Left))
                .Append(Restrictions.Between("Birth", DateTime.Now, DateTime.Now.AddDays(1)))
                .Append(Restrictions.And)
                .Append(Restrictions.LessThan("Weight", 200))
                .Append(Restrictions.Bracket(Bracket.Rgiht));

            System.Console.WriteLine(operand);
            System.Console.WriteLine("Finished");
            System.Console.Read();
        }
    }
}
