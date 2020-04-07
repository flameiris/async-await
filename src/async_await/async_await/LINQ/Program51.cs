using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace async_await
{
    partial class Program51
    {

        static void Main()
        {
            string[] names = { "flame1", "flame2", "flame3" };
            var name1 = names.FirstOrDefault(x => x == "flame1");
            Console.WriteLine(name1);

            var nameList = names.Where(x => x == "flame2").OrderByDescending(x => x).Skip(0).Take(1).FirstOrDefault();

            string name2 = (from n in names
                            where n.Equals("flame1")
                            select n).First();
            Console.WriteLine(name2);




            int[] seq1 = { 1, 2, 2, 3 };
            int[] seq2 = { 3, 4, 5 };
            // { 1, 2, 2, 3, 3, 4, 5 }
            IEnumerable<int> concat = seq1.Concat(seq2);
            // { 1, 2, 3, 4, 5 }
            IEnumerable<int> union = seq1.Union(seq2);





















            // string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

            var query =
                from n in names
                where n.Contains("1")
                orderby n.Length
                select n.ToUpper();


            // 获取所有长度最短的名字（注意：可能有多个）
            IEnumerable<string> outQuery = names
                .Where(n => n.Length ==
                    (names.OrderBy(n2 => n2.Length).Select(n2 => n2.Length).First()));


            var query2 = names
                .Select(n => n.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", ""))
                .Where(n => n.Length > 2)
                .OrderBy(n => n);





            Console.ReadKey();
        }

    }
}