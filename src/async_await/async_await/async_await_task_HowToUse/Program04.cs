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
            Console.WriteLine($"主线程执行1  线程id={Thread.CurrentThread.ManagedThreadId}");
            Test();
            Console.WriteLine($"主线程执行4  线程id={Thread.CurrentThread.ManagedThreadId}");
            Console.ReadLine();
        }

        static async void Test()
        {
            Console.WriteLine($"主线程执行2  线程id={Thread.CurrentThread.ManagedThreadId}");
            Task<string> task = Task.Run(() =>
            {
                Console.WriteLine($"新线程执行1  线程id={Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(5000);
                Console.WriteLine($"新线程执行2 返回 Hello World  线程id={Thread.CurrentThread.ManagedThreadId}");
                return "Hello World";
            });
            Console.WriteLine($"主线程执行3  线程id={Thread.CurrentThread.ManagedThreadId}");
            //5秒之后才会执行这里，这句是 主线程执行的
            string str = await task;
            //证明是 主线程执行的
            //Console.WriteLine($"输出内容= {await task}  线程id= {Thread.CurrentThread.ManagedThreadId}");


            Console.WriteLine($"新线程执行3 返回 Hello World  线程id={Thread.CurrentThread.ManagedThreadId}");

        }
    }
}