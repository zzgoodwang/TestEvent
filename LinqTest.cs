using System;
using System.Linq;
using System.Threading;


namespace MyProgram1
{
	class testProClass
	{
		private string _name="Wang Jun";

		public string Name{
			get{
				return _name;
				} 
			set{
				_name = value;
				}
			}
		public int Age {get;set;}
	}

    class program
    {
		/// <summary>
        /// 委托  逛超市
        /// </summary>
        /// <param name="a">花费</param>
        /// <param name="b">付钱</param>
        /// <returns>找零</returns>
        delegate int GuangChaoshi(int a,int b);
		delegate int del(int i,int j);  
		delegate int xx(int x);

		delegate int add(int a, int b);

        static void Main(string[] args)
        {
			//简单的Lambda
			int j=0;
			del myDelegate = (x,y) => x * y;   
			j = myDelegate(5,6); //j = 25 
            Console.WriteLine("Hello {0}",j);

			//复杂Lambda
			GuangChaoshi gwl=(p,z)=>
			{
               int zuidixiaofei = 10;
                if (p < zuidixiaofei)
                {
                    return 100;
                }
                else
                {
                    return z - p - 10;
                }
            };



            Console.WriteLine(gwl(10,100) + "");   //打印80，z对应参数b，p对应参数a			

			int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int oddNumbers = numbers.Count(n => n % 2 == 1);
            Console.WriteLine("odd {0}",oddNumbers);

			xx m = (x)=> x*x;
            Console.WriteLine("x*x {0}",m(2));

			//匿名方法
			add aAdd= delegate(int a, int b)
			{
				return a + b;
			};
            Console.WriteLine("a+b {0}",aAdd(3,2));

			testProClass pro = new testProClass();

			//pro.Name="John";
			pro.Age = 47;

            Console.WriteLine("Pro {0},{1}",pro.Name, pro.Age);






            Console.ReadKey();
        }
    }
}