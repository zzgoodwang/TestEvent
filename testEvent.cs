/*
要创建一个事件驱动的程序需要下面的步骤：
	(1)、定义delegate对象类型，它有两个参数，第一个参数是事件发送者对象，第二个参数是事件参数类对象。
　　(2)、定义事件参数类，此类应当从System.EventArgs类派生。如果事件不带参数，这一步可以省略。
　　(3)、定义"事件处理方法，它应当与delegate对象具有相同的参数和返回值类型"。
　　(4)、用event关键字定义事件对象，它同时也是一个delegate对象。
　　(5)、用+=操作符添加事件到事件队列中（-=操作符能够将事件从队列中删除）。
　　(6)、在需要触发事件的地方用调用delegate的方式写事件触发方法。一般来说，此方法应为protected访问限制，即
		不能以public方式调用，但可以被子类继承。名字是OnEventName。
　　(7)、在适当的地方调用事件触发方法触发事件。
现在我们来编写一个自定义事件的程序。主人养了一条忠实的看门狗，晚上主人睡觉的时候，狗负责看守房子。
一旦有小偷进来，狗就发出一个Alarm事件，主人接到Alarm事件后就会采取相应的行动,警察也会接到Alarm事件，去逮捕小偷。
*/
//其实，事件就是一个委托，用于在特定情况发生时，一个类实例去主动调用另一个类实例中的方法。

using System;
using System.Linq;
using System.Threading;

public delegate void AlarmHandler(object sender, ThiefAlarm e);

public class ThiefAlarm:EventArgs
{
	private int _thiefCount;
	public int ThiefCount
	{
		get{
			return _thiefCount;
			}
	}
	public ThiefAlarm(int thiefCount)
	{
		_thiefCount=thiefCount;
	}
}
class Dog
{
	public event AlarmHandler  AlarmEvent;
	protected void OnAlarm(int thiefcount)
	{
		if (AlarmEvent != null)
		{
			AlarmEvent(this, new ThiefAlarm(thiefcount));
		}
	}
	public void SeeThief(int thiefcount)
	{
		Console.WriteLine("Dog: There is {0} thieves.",thiefcount);
		OnAlarm(thiefcount);

	}
}

class Host
{
	public Host(Dog dog)
	{
		dog.AlarmEvent += ThiefIn;
	}
	void ThiefIn(object sender, ThiefAlarm e)
	{
		Console.WriteLine("Host: Yes, I've got it and see {0} thieves.",e.ThiefCount);
	}
}

class Policeman
{
	public Policeman(Dog dog)
	{
		dog.AlarmEvent += ThiefFound;
	}
	void ThiefFound(object sender, ThiefAlarm e)
	{
		Console.WriteLine("Policemen: OK, We will go to arrest the {0} thieves.",e.ThiefCount);
	}
}


class Program
{
	static void Main(string [] args)
	{

		Dog dog = new Dog();
		Host host = new Host(dog);
		Policeman policeman = new Policeman(dog);

		Thread aThread = new Thread(()=>
		{
			DateTime now = new DateTime(2013,12,31,23,59,50);
			DateTime midnight = new DateTime(2014,1,1,0,0,0);

			while( now < midnight)
			{
				Console.WriteLine("The current time is {0}",now);
				Thread.Sleep(1000);
				now = now.AddSeconds(1);
			}

			Console.WriteLine("The theives are coming....");
			dog.SeeThief(2);
		});

		aThread.IsBackground = true;
		aThread.Start();
		Console.ReadKey();
	}

}