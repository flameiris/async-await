using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace async_await
{
    partial class Program03
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"主线程执行1  线程id={Thread.CurrentThread.ManagedThreadId}");
            Test();

            //Thread.Sleep(3000);
            Console.WriteLine($"主线程执行5  线程id={Thread.CurrentThread.ManagedThreadId}");
            Console.ReadLine();
        }

        static async Task Test()
        {
            Console.WriteLine($"主线程执行2  线程id={Thread.CurrentThread.ManagedThreadId}");

            //这里没有用 await,所以下面的代码可以继续执行
            //但是如果上面是 await GetName()，下面的代码就不会立即执行，输出结果就不一样了。
            //GetName() 方法返回 Task<string> 类型
            Console.WriteLine("调用GetName 开始");
            var name = GetName();

            Console.WriteLine("调用GetName 结束");

            //这里调用 await name
            //主线程大概是做标记， 先返回到调用方法处继续执行代码
            //主线程执行完毕后, 新线程如果未执行完毕，则主线程执行挂起等待做标记的代码（等待新线程执行完毕）；如新线程执行完毕，主线程直接执行代码
            Console.WriteLine($"线程id={Thread.CurrentThread.ManagedThreadId} 获取到 GetName() 的返回值为 {await name}");
            //此后是新线程执行代码
            Console.WriteLine($"新线程执行3  线程id={Thread.CurrentThread.ManagedThreadId}");
        }

        static async Task<string> GetName()
        {
            // 这里还是主线程
            Console.WriteLine($"主线程执行3  线程id={Thread.CurrentThread.ManagedThreadId}");
            //Task.Run() 创建新线程，并且执行线程（此时 主线程 和 新线程 是同时在运行）
            return await Task.Run(() =>
            {
                Console.WriteLine($"新线程执行1  线程id={Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(1000);
                Console.WriteLine($"新线程执行2  线程id={Thread.CurrentThread.ManagedThreadId}");
                return "Flameiris";
            });
        }
    }
}