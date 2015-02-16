using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Action_Delegate
{
    class Program
    {
        static void Main(string[] args)
        {

            var parameter = Expression.Parameter(typeof(ProgramZ));
            var x2 = Expression.Property(parameter, "x");

            var y = Expression.MakeIndex(x2, typeof(List<ProgramY>).GetProperty("Item"), new[] { Expression.Constant(0) });

            var expr =

                

        Expression.Lambda<Func<ProgramZ,ProgramY>>(
            y,
        parameter);


         //   var instance = new ProgramZ { x = new List<string> { "a", "b" } };

            //Console.WriteLine(expr.Compile().Invoke(instance));


            string s = "SubsidaryCompanies[0].TestGroup[0]";
            string s2 = "[0]SubsidaryCompanies.TestGroup";
            var x = s.LastIndexOf('[');
            var indexX = s[x];
            var indexX2 = s[x+1];

            var x3 = s2.LastIndexOf('[');
            var indexsX = s[x3];
            var indexsX2 = s[x3 + 1];


            string template = "#RepeatGroupContainerStandardQuestions.SubsidiaryCompanies[0]TestRepeatGroups";
            var templatex = template.Replace('.', '_');
            templatex = templatex.Replace('[', '_');
            templatex = templatex.Replace(']', '_');


            Regex pattern = new Regex(@"[.\[\]]");
            var gggg = pattern.Replace(template, "_");

            Console.ReadLine();

        }


        public class ProgramZ
        {
            public List<ProgramY> x { get; set; }
        }


        public class ProgramY
        {
            public string x2 { get; set; }
        }
    }
}
