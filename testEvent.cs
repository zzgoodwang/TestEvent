/*
Ҫ����һ���¼������ĳ�����Ҫ����Ĳ��裺
	(1)������delegate�������ͣ�����������������һ���������¼������߶��󣬵ڶ����������¼����������
����(2)�������¼������࣬����Ӧ����System.EventArgs������������¼�������������һ������ʡ�ԡ�
����(3)������"�¼�����������Ӧ����delegate���������ͬ�Ĳ����ͷ���ֵ����"��
����(4)����event�ؼ��ֶ����¼�������ͬʱҲ��һ��delegate����
����(5)����+=����������¼����¼������У�-=�������ܹ����¼��Ӷ�����ɾ������
����(6)������Ҫ�����¼��ĵط��õ���delegate�ķ�ʽд�¼�����������һ����˵���˷���ӦΪprotected�������ƣ���
		������public��ʽ���ã������Ա�����̳С�������OnEventName��
����(7)�����ʵ��ĵط������¼��������������¼���
������������дһ���Զ����¼��ĳ�����������һ����ʵ�Ŀ��Ź�����������˯����ʱ�򣬹������ط��ӡ�
һ����С͵���������ͷ���һ��Alarm�¼������˽ӵ�Alarm�¼���ͻ��ȡ��Ӧ���ж�,����Ҳ��ӵ�Alarm�¼���ȥ����С͵��
*/
//��ʵ���¼�����һ��ί�У��������ض��������ʱ��һ����ʵ��ȥ����������һ����ʵ���еķ�����

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