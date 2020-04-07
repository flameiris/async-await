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
    class Program61
    {
        static void Main(string[] args)
        {
            Person p = new Person("hello world");
            p.SayHi();

            Console.ReadKey();
        }
    }

    public class Person
    {
        public string _word;

        public string Name { get; set; } = "Flame Iris";
        public int Age { get; set; } = 20;
        public DateTime Birthday { get; } = new DateTime(2000, 1, 1);


        public static string Nation { get; } = "中国";
        public static string Color { get; }
        static Person()
        {
            Color = "黄";
        }
        public Person()
        {
        }
        public Person(string word)
        {
            _word = word;
        }

        public void SayHi()
        {
            Console.WriteLine(_word);
        }

        public void SayHi(string word)
        {
            Console.WriteLine(word);
        }

        public void WhoAmI()
        {

            Console.WriteLine($"我是{Nation}人，{Color}皮肤，名字是{Name},年龄{Age}");
        }
    }

    class Person2 : Person
    {
        public void SayHi()
        {
            Console.WriteLine(_word);
        }
    }

    internal class Person3 : Person
    {

    }
}