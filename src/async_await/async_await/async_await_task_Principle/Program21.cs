using System;
using System.Threading;
using System.Threading.Tasks;

namespace async_await
{
    partial class Program21
    {

        static void Main()
        {
            Console.WriteLine($"主线程执行1  线程id={Thread.CurrentThread.ManagedThreadId}");
            //Task.Run() 编译后生成一个嵌套类
            var task = Task.Run(() =>
            {
                Console.WriteLine($"新线程执行1  线程id={Thread.CurrentThread.ManagedThreadId}");
            });
            Console.WriteLine($"主线程执行1  线程id={Thread.CurrentThread.ManagedThreadId}");
            Console.ReadKey();
        }



        //[CompilerGenerated]
        //[Serializable]
        //private sealed class <>c
        //{
        //	// Token: 0x06000003 RID: 3 RVA: 0x000020D0 File Offset: 0x000002D0
        //	// Note: this type is marked as 'beforefieldinit'.
        //	static <>c()
        //	{
        //	}
        //
        //	// Token: 0x06000004 RID: 4 RVA: 0x000020DC File Offset: 0x000002DC
        //	public <>c()
        //	{
        //	}
        //
        //	// Token: 0x06000005 RID: 5 RVA: 0x000020E5 File Offset: 0x000002E5
        //	internal void <Main>b__0_0()
        //	{
        //		Console.WriteLine(string.Format("新线程执行1  线程id={0}", Thread.CurrentThread.ManagedThreadId));
        //	}
        //
        //	// Token: 0x04000001 RID: 1
        //	public static readonly Program21.<>c <>9 = new Program21.<>c();
        //
        //	// Token: 0x04000002 RID: 2
        //	public static Action <>9__0_0;
        //}
    }
}