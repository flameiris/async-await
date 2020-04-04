using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace async_await
{
    partial class Program04
    {

        //await并不是针对于async的方法，而是针对async方法所返回给我们的Task，这也是为什么所有的async方法都必须返回给我们Task。
        //所以我们同样可以在Task前面也加上await关键字，这样做实际上是告诉编译器我需要等这个Task的返回值或者等这个Task执行完毕之后才能继续往下走。
        //加上await关键字之后，后面的代码会被挂起等待，直到task执行完毕有返回值的时候才会继续向下执行，这一段时间主线程会处于挂起状态。
        static void Main()
        {
            Test();

            Console.WriteLine("aaaaaaa");
            Console.ReadLine();
        }

        static async void Test()
        {
            Task<string> task = Task.Run(() =>
            {
                Thread.Sleep(5000);
                return "Hello World";
            });
            string str = await task;  //5 秒之后才会执行这里

            Console.WriteLine("hahahah");

        }







        //不用await关键字，如何确认Task执行完毕了？
        //GetAwaiter方法会返回一个awaitable的对象（继承了INotifyCompletion.OnCompleted方法）我们只是传递了一个委托进去，
        //等task完成了就会执行这个委托，但是并不会影响主线程，下面的代码会立即执行。这也是为什么我们结果里面第一句话会是 “主线程执行完毕”！
        //static void Main()
        //{
        //    var task = Task.Run(() =>
        //    {
        //        return GetName();
        //    });

        //    task.GetAwaiter().OnCompleted(() =>
        //    {
        //        // 2 秒之后才会执行这里
        //        var name = task.Result;
        //        Console.WriteLine("My name is: " + name);
        //    });

        //    Console.WriteLine("主线程执行完毕1");
        //    Console.WriteLine("另外一个线程返回的数据：" + task.Result);
        //    Console.WriteLine("主线程执行完毕2");
        //    Console.ReadLine();
        //}

        //static string GetName()
        //{
        //    Console.WriteLine("另外一个线程在获取名称");
        //    Thread.Sleep(2000);
        //    return "Jesse";
        //}
    }
}