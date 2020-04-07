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
    partial class Program41
    {

        static void Main()
        {
            //ParameterExpression exp1 = Expression.Parameter(typeof(int), "a");
            //ParameterExpression exp2 = Expression.Parameter(typeof(int), "b");

            //BinaryExpression exp = Expression.Add(exp1, exp2);
            //var lamExp = Expression.Lambda<Func<int, int, int>>(exp, new ParameterExpression[] { exp1, exp2 });
            //var lamExpCompile = lamExp.Compile();
            //Console.WriteLine(lamExp);
            //Console.WriteLine(lamExp.Compile());
            //Console.WriteLine(lamExpCompile(1, 2));


            //四则运算表达式树、
            Expression<Func<double, double, double, double, double>> myExp = (a, b, m, n) => -Math.Floor(m) * a * a + n * b * b;

            //初始化
            var calc = new BinaryExpressionCalculator(myExp);
            Console.WriteLine(calc.Calculate(1, 1, 2.9, 1));
            //Console.WriteLine(calc.CalculatePrefix(1, 2, 3, 4));


            //LambdaExpression.Compile() 将Lambda表达式的表达式树编译成一个.NET方法
            //ParameterExpression pi = Expression.Parameter(typeof(int), "i");
            //LambdaExpression fexp =
            //    Expression.Lambda(
            //        Expression.Add(pi, Expression.Constant(1))
            //        , pi);

            ////Compile() 方法生成了一个 Delegate
            //Delegate f = fexp.Compile();
            ////Delegate的类型是 Func<int, int>  输出结果是：System.Func`2[System.Int32,System.Int32]
            ////这个委托所引用的方法是一个DynamicMethod编译后的结果。
            ////DynamicMethod是Reflection.Emit的强大功能，可在运行时动态创建出.NET方法来
            //Console.WriteLine(f.GetType());
            //Console.WriteLine(f.DynamicInvoke(2));

            ////Expression.Lambda这个方法还有一个泛型版重载，它可以创建如同C#自己生成一样的强类型LambdaExpression。
            ////强类型LambdaExpression的Compile就直接生成强类型的委托
            //ParameterExpression pi2 = Expression.Parameter(typeof(int), "i");
            //var fexp2 = Expression.Lambda<Func<int, int>>(Expression.Add(pi2, Expression.Constant(1)), pi2);
            //var f2 = fexp2.Compile();
            //Console.WriteLine(f.GetType());
            //Console.WriteLine(f2(2));
            Console.ReadKey();
        }


        class BinaryExpressionCalculator
        {
            //键是 参数表达式，值是调用方法传入 lambda表达式的入参值
            Dictionary<ParameterExpression, double> argDict;
            //lambda表达式
            LambdaExpression exp;


            public BinaryExpressionCalculator(LambdaExpression exp)
            {
                this.exp = exp;
            }

            public double Calculate(params double[] args)
            {
                //从ExpressionExpression中提取参数，和传输的实参对应起来。
                //生成的字典可以方便我们在后面查询参数的值
                argDict = new Dictionary<ParameterExpression, double>();

                for (int i = 0; i < exp.Parameters.Count; i++)
                {
                    //就不检查数目和类型了，大家理解哈
                    argDict[exp.Parameters[i]] = args[i];
                }

                //提取树根
                Expression rootExp = exp.Body as Expression;

                //计算！
                return InternalCalc(rootExp);
            }

            double InternalCalc(Expression exp)
            {
                //判断是否为 常量 表达式
                ConstantExpression cexp = exp as ConstantExpression;
                if (cexp != null) return (double)cexp.Value;

                //判断是否为 参数表达式
                ParameterExpression pexp = exp as ParameterExpression;
                if (pexp != null) return argDict[pexp];

                //判断是否为 二元运算 表达式 或者 求负 表达式
                BinaryExpression bexp = exp as BinaryExpression;
                UnaryExpression uexp = exp as UnaryExpression;
                MethodCallExpression callexp = exp as MethodCallExpression;
                if (bexp == null && uexp == null && callexp == null) throw new ArgumentException("不支持表达式的类型", "exp");





                switch (exp.NodeType)
                {
                    case ExpressionType.Add:
                        return InternalCalc(bexp.Left) + InternalCalc(bexp.Right);
                    case ExpressionType.Divide:
                        return InternalCalc(bexp.Left) / InternalCalc(bexp.Right);
                    case ExpressionType.Multiply:
                        return InternalCalc(bexp.Left) * InternalCalc(bexp.Right);
                    case ExpressionType.Subtract:
                        return InternalCalc(bexp.Left) - InternalCalc(bexp.Right);
                    case ExpressionType.Negate:
                        return -InternalCalc(uexp.Operand);
                    case ExpressionType.Call:
                        var expParams = callexp.Arguments.Cast<ParameterExpression>();
                        List<object> args = new List<object>();
                        foreach (var item in expParams)
                        {
                            args.Add(argDict[item]);
                        }
                        var lamExp = Expression.Lambda<Func<double, double>>(callexp, expParams);
                        var lamExpCompile = lamExp.Compile();
                        return (double)lamExpCompile.DynamicInvoke(args);
                    default:
                        throw new ArgumentException("不支持表达式的类型", "exp");
                }
            }




            public string CalculatePrefix(params double[] args)
            {
                //从ExpressionExpression中提取参数，和传输的实参对应起来。
                //生成的字典可以方便我们在后面查询参数的值
                argDict = new Dictionary<ParameterExpression, double>();

                for (int i = 0; i < exp.Parameters.Count; i++)
                {
                    //就不检查数目和类型了，大家理解哈
                    argDict[exp.Parameters[i]] = args[i];
                }

                //提取树根
                Expression rootExp = exp.Body as Expression;

                //计算！
                return InternalPrefix(rootExp);
            }
            string InternalPrefix(Expression exp)
            {
                //判断是否为 常量 表达式
                ConstantExpression cexp = exp as ConstantExpression;
                if (cexp != null) return cexp.Value.ToString();
                //判断是否为 参数 表达式
                ParameterExpression pexp = exp as ParameterExpression;
                if (pexp != null) return pexp.Name;
                //判断是否为 二元运算 表达式
                BinaryExpression bexp = exp as BinaryExpression;
                if (bexp == null) throw new ArgumentException("不支持表达式的类型", "exp");

                switch (bexp.NodeType)
                {
                    case ExpressionType.Add:
                        return "+ " + InternalPrefix(bexp.Left) + " " + InternalPrefix(bexp.Right);
                    case ExpressionType.Divide:
                        return "- " + InternalPrefix(bexp.Left) + " " + InternalPrefix(bexp.Right);
                    case ExpressionType.Multiply:
                        return "* " + InternalPrefix(bexp.Left) + " " + InternalPrefix(bexp.Right);
                    case ExpressionType.Subtract:
                        return "/ " + InternalPrefix(bexp.Left) + " " + InternalPrefix(bexp.Right);
                    default:
                        throw new ArgumentException("不支持表达式的类型", "exp");
                }
            }
        }

    }
}