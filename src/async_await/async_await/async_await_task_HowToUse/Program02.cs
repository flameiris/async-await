using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace async_await
{
    partial class Program02
    {

        //一个小例子
        static void Main(string[] args)
        {
            // 这个方法其实是多余的, 本来可以直接写下面的方法
            // await GetName() 
            // 但是由于控制台的入口方法不支持async,所有我们在入口方法里面不能 用 await
            //这里是调用方法
            Console.WriteLine($"主线程执行1  线程id={Thread.CurrentThread.ManagedThreadId}");
            Test();

            //主线程执行4
            Console.WriteLine($"主线程执行4  线程id={Thread.CurrentThread.ManagedThreadId}");
            Console.ReadKey();
            //主线程执行完毕，线程状态？？？
        }

        //这里是异步方法
        static async Task Test()
        {
            // 方法打上async关键字，就可以用await调用同样打上async的方法
            Console.WriteLine($"主线程执行2  线程id={Thread.CurrentThread.ManagedThreadId}");
            await GetName();
        }

        //这里是异步方法
        static async Task GetName()
        {
            Console.WriteLine($"主线程执行3  线程id={Thread.CurrentThread.ManagedThreadId}");
            // Delay 方法来自于.net 4.5
            //耗时操作，Task开启新的线程【新线程执行1，主线程执行返回到调用方法处继续执行】
            await Task.Delay(1000);
            Console.WriteLine($"新线程执行1  线程id={Thread.CurrentThread.ManagedThreadId}");
        }

    }
}