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
            var name = task.GetAwaiter().GetResult();

            Console.WriteLine($"主线程执行3 新线程获取到名称= {name}  线程id={Thread.CurrentThread.ManagedThreadId}");
            Console.ReadLine();
        }

        static string GetName()
        {
            Console.WriteLine($"新线程执行2 新线程在获取名称  线程id={Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(3000);
            return "FlameIris";
        }



        //await 实质是在调用awaitable对象的GetResult方法???
        static async Task Test()
        {
            Task<string> task = Task.Run(() =>
            {
                Console.WriteLine("另一个线程在运行！");  // 这句话只会被执行一次
                Thread.Sleep(2000);
                return "Hello World";
            });

            // 这里主线程会挂起等待，直到task执行完毕我们拿到返回结果
            var result = task.GetAwaiter().GetResult();
            // 这里不会挂起等待，因为task已经执行完了，我们可以直接拿到结果
            var result2 = await task;
            Console.WriteLine(str);
        }
    }
}