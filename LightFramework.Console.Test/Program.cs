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
                .Append(Restrictions.Between("Birth", DateTime.Now, DateTime.Now.AddDays(1)));

            System.Console.WriteLine(operand.Compile());
            System.Console.WriteLine("Finished");
            System.Console.Read();
        }
    }
}
