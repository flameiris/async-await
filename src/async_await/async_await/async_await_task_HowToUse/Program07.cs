using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace async_await
{
    partial class Program06
    {

        //使用  task.GetAwwiter().GetResult();

        static void Main()
        {
            Console.WriteLine($"主线程执行1  线程id={Thread.CurrentThread.ManagedThreadId}");
            var task = Task.Run(() =>
            {
                Console.WriteLine($"新线程执行1  线程id={Thread.CurrentThread.ManagedThreadId}");
                return GetName();
            });

            Console.WriteLine($"主线程执行2  线程id={Thread.CurrentThread.ManagedThreadId}");
            //注意，这里主线程挂起了
            //task.GetAwaiter() 返回 TaskAwaiter 类型的对象，调用task.GetAwaiter().GetResult()会挂起主线程
            //主线程挂起，等待新线程执行返回或执行完毕。 
            //如果执行到该句代码时新线程已经执行返回或执行完毕，则主线程不会挂起
            //Thread.Sleep(5000);
            var name = task.ConfigureAwait(true).GetAwaiter().GetResult();

            Console.WriteLine($"主线程执行3 新线程获取到名称= {name}  线程id={Thread.CurrentThread.ManagedThreadId}");
            Console.ReadLine();
        }

        static string GetName()
        {
            Console.WriteLine($"新线程执行2 新线程在获取名称  线程id={Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(3000);
            return "FlameIris";
        }
    }
}