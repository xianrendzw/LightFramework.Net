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
               .Append(Restrictions.Equal("Age", 29))
               .Append(Restrictions.And)
               .Append(Restrictions.Bracket(Bracket.Left))
               .Append(Restrictions.Between("Weight", 100, 180))
               .Append(Restrictions.And)
               .Append(Restrictions.LessThan("Salary", "20w"))
               .Append(Restrictions.Bracket(Bracket.Rgiht));

            System.Console.WriteLine(operand);
            System.Console.WriteLine("Finished");
            System.Console.Read();
        }
    }
}
