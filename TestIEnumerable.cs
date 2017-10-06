/*IEnumerable的用法
IEnumerable和IEnumerable<T>接口在.NET中是非常重要的接口,它允许开发人员定义foreach语句功能的实现并支持非泛型方法的简单的迭代，IEnumerable和IEnumerable<T>接口是.NET Framework中最基本的集合访问器。它定义了一组扩展方法，用来对数据集合中的元素进行遍历、过滤、排序、搜索等操作。
IEnumerable接口是非常的简单，只包含一个抽象的方法GetEnumerator()，它返回一个可用于循环访问集合的IEnumerator对象。
IEnumerator对象有什么呢？它是一个真正的集合访问器，没有它，就不能使用foreach语句遍历集合或数组，因为只有IEnumerator对象才能访问集合中的项，假如连集合中的项都访问不了，那么进行集合的循环遍历是不可能的事情了。
 
一、IEnumerable、IEnumerator、ICollection、IList、List
 
IEnumerator：提供在普通集合中遍历的接口，有Current，MoveNext()，Reset()，其中Current返回的是object类型。
IEnumerable： 暴露一个IEnumerator，支持在普通集合中的遍历。

IEnumerator<T>：继承自IEnumerator，有Current属性，返回的是T类型。
IEnumerable<T>：继承自IEnumerable，暴露一个IEnumerator<T>，支持在泛型集合中遍历。
 
1、IEnumerable接口
    // 摘要:
    //     公开枚举器，该枚举器支持在指定类型的集合上进行简单迭代。
    //
    // 类型参数:
    //   T:
    //     要枚举的对象的类型。
    [TypeDependency("System.SZArrayHelper")]
    public interface IEnumerable<out T> : IEnumerable
    {
        // 摘要:
        //     返回一个循环访问集合的枚举器。
        //
        // 返回结果:
        //     可用于循环访问集合的 System.Collections.Generic.IEnumerator<T>。
        IEnumerator<T> GetEnumerator();
    }

 
可以看到，GetEnumerator方法返回对另一个接口System.Collections.IEnumerator的引用。这个接口提供了基础设施，调用方可以用来移动IEnumerable兼容容器包含的内部对象。
 
2、IEnumerator接口
public interface IEnumerator
{
  bool MoveNext(); //将游标的内部位置向前移动
  object Current{get;} //获取当前的项（只读属性）
  void Reset(); //将游标重置到第一个成员前面
}

 3、ICollection接口：ICollection<T> 同时继承IEnumerable<T>和IEnumerable两个接口
    [TypeDependency("System.SZArrayHelper")]
    public interface ICollection<T> : IEnumerable<T>, IEnumerable
4、IList接口
public interface IList<T> : ICollection<T>, IEnumerable<T>, IEnumerable
 
5、List的定义
public class List<T> : IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace testIEnumerable
{
    class Person
    {
        private string _name;
        private int _age;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }
        public Person(string strName, int nAge)
        {
            _name = strName;
            _age = nAge;
        }
    }
    class PersonList : IEnumerable
    {
        private Person[] _persons;

        public PersonList(Person[] arrpersons)
        {
            _persons = new Person[arrpersons.Length];
            for (int i = 0; i < arrpersons.Length; i++)
            {
                _persons[i] = arrpersons[i];
            }
        }
        public IEnumerator GetEnumerator()
        {
            return new PersonListEnum(_persons);
        }
    }
    class PersonListEnum : IEnumerator
    {
        private Person[] _pers;
        private int position = -1;
        public PersonListEnum(Person[] arrpers)
        {
            _pers=arrpers;
        }
        public bool MoveNext()
        {
            position++;
            return (position < _pers.Length);

        }
        public Object Current
        {
            get
            {
                try
                {
                    return _pers[position];
                }
                catch (IndexOutOfRangeException)        
                {
                    throw new InvalidOperationException();//抛出异常信息
                }
            }
            
        }
        public void Reset()
        {
            position = -1;
        }

    }
    class TestIEnumerable
    {
        static void Main(string[] args)
        {
            Person[] persons = new Person[]{
                new Person("张三",20), new Person("李四",21),
                new Person("马五",19), new Person("赵六",25)
            };

            PersonList personlist = new PersonList(persons);

            foreach ( Person per in personlist )
            {
                Console.WriteLine("{0}  {1}",per.Name, per.Age);
            }
            Console.ReadKey();
        }
    }
}
