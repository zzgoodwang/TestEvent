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
        /// ί��  �䳬��
        /// </summary>
        /// <param name="a">����</param>
        /// <param name="b">��Ǯ</param>
        /// <returns>����</returns>
        delegate int GuangChaoshi(int a,int b);
		delegate int del(int i,int j);  
		delegate int xx(int x);

		delegate int add(int a, int b);

        static void Main(string[] args)
        {
			//�򵥵�Lambda
			int j=0;
			del myDelegate = (x,y) => x * y;   
			j = myDelegate(5,6); //j = 25 
            Console.WriteLine("Hello {0}",j);

			//����Lambda
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



            Console.WriteLine(gwl(10,100) + "");   //��ӡ80��z��Ӧ����b��p��Ӧ����a			

			int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int oddNumbers = numbers.Count(n => n % 2 == 1);
            Console.WriteLine("odd {0}",oddNumbers);

			xx m = (x)=> x*x;
            Console.WriteLine("x*x {0}",m(2));

			//��������
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