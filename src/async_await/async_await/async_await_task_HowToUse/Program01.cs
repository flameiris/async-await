using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace async_await
{
    partial class Program01
    {
        static void Main(string[] args)
        {
            //执行1 主线程
            Console.WriteLine("111 balabala. My Thread ID is :" + Thread.CurrentThread.ManagedThreadId);
            //执行2 主线程
            AsyncMethod();

            //var resultTask = AsyncMethod();
            //Console.WriteLine(resultTask.Result);
            //执行6 新线程执行耗时操作，切换回主线程执行
            Console.WriteLine("222 balabala. My Thread ID is :" + Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine("OK");
            Console.Read();
        }

        private static async Task AsyncMethod()
        {
            //主线程
            //var ResultFromTimeConsumingMethod = TimeConsumingMethod();
            //执行3 主线程创建新线程，并在新线程中执行task，获取新线程中task的结果
            string Result = await TimeConsumingMethod() + " + AsyncMethod. My Thread ID is :" + Thread.CurrentThread.ManagedThreadId;
            //执行7 新线程耗时操作完毕结果返回 切换回主线程
            Console.WriteLine(Result);
            //返回值是Task的函数可以不用return
            //return Result;
        }

        //这个函数就是一个耗时函数，可能是IO操作，也可能是cpu密集型工作。
        private static Task<string> TimeConsumingMethod()
        {
            //执行4 主线程创建task
            var task = Task.Run(() =>
            {
                Thread.Sleep(5000);
                //执行6 新线程
                Console.WriteLine("Helo I am TimeConsumingMethod. My Thread ID is :" + Thread.CurrentThread.ManagedThreadId);
                //新线程阻塞5000毫秒，新线程阻塞期间，切换回主线程执行
                //执行8， 5000毫秒后，主线程切换回新线程执行
                Console.WriteLine("Helo I am TimeConsumingMethod after Sleep(5000). My Thread ID is :" + Thread.CurrentThread.ManagedThreadId);
                //新线程返回task结果
                return "Hello I am TimeConsumingMethod";

            });
            //执行5 返回task
            return task;
        }


    }
}