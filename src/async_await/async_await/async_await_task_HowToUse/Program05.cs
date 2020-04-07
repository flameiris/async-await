using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace async_await
{
    partial class Program05
    {
        //不用await关键字，如何确认Task执行完毕了？
        //使用  task.GetAwwiter().OnCompleted()

        //疑问： 以下两个步骤的执行顺序？ 是否是看当前CPU是否空闲来自动分配执行顺序？
        //1.主线程执行 task.Result 获取结果 
        //2. OnCompleted()中传入的委托 
        static void Main()
        {

            Console.WriteLine($"主线程执行1  线程id={Thread.CurrentThread.ManagedThreadId}");
            var task = Task.Run(() =>
            {
                Console.WriteLine($"新线程执行1  线程id={Thread.CurrentThread.ManagedThreadId}");
                return GetName();
            });

            Console.WriteLine($"主线程执行2  线程id={Thread.CurrentThread.ManagedThreadId}");
            //注意，这里主线程没有挂起
            //GetAwaiter() 返回 TaskAwaiter 类型的对象， （TaskAwaiter 类型继承了 INotifyCompletion.OnCompleted 方法）
            //OnCompleted 方法中传入匿名委托，当新线程中的Task执行返回或执行完毕后，再执行委托
            //所以，主线程在执行该语句后，没有挂起并等待，而是继续执行后续代码。
            task.GetAwaiter().OnCompleted(() =>
            {
                //5秒之后才会执行这里
                //新线程中的Task执行返回或执行完毕后，再执行委托
                Console.WriteLine($"新线程执行3  新线程获取到名称  线程id={Thread.CurrentThread.ManagedThreadId}");
                var name = task.Result;
                Console.WriteLine($"新线程执行4  名称= {name}  线程id={Thread.CurrentThread.ManagedThreadId}");
            });

            Console.WriteLine($"主线程执行3  线程id={Thread.CurrentThread.ManagedThreadId}");
            //主线程执行到该句代码时，获取 task.Result ，主线程挂起并等待task执行返回
            Console.WriteLine($"主线程执行4  另一个线程返回的数据= { task.Result}  线程id={Thread.CurrentThread.ManagedThreadId}");
            Console.ReadLine();
        }

        static string GetName()
        {
            Console.WriteLine($"新线程执行2  新线程在获取名称  线程id={Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(5000);
            return "FlameIris";
        }
    }
}