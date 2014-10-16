using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace GetType_TypeOf_Is
{
    class Program
    {
        static void Main(string[] args)
        {

            string x = "Hello";
            Console.WriteLine("Gettype of x is: {0}", x.GetType());
            Console.WriteLine("Base Gettype of x is: {0}", x.GetType().BaseType);

            object o = x;
            Console.WriteLine("Gettype of o is: {0}", o.GetType());
            Console.WriteLine("typeof of o is: {0}", typeof(object));

            o = new MyBaseClass();
            Console.WriteLine("GetType of o changed type is: {0}", o.GetType());




            MyBaseClass baseClass = new MyBaseClass();
            MyDerivedClass derivedClass = new MyDerivedClass();


            Console.WriteLine("Gettype of baseClass is: {0}", baseClass.GetType().Name);
            Console.WriteLine("Gettype of derivedClass is: {0}", derivedClass.GetType().Name);
            Object o2 = derivedClass;

            Console.WriteLine("Gettype of o2 is: {0}", o2.GetType().Name);
            Console.WriteLine("Base Gettype of o2 is: {0}", o2.GetType().BaseType.Name);

            MyDerivedClass derivedClass2 = new MyDerivedClass();
            derivedClass2.DerivedClassId = 2;
            derivedClass2.BaseClassId = 1;
            String derivedClass2JSON = new JavaScriptSerializer().Serialize(derivedClass2);
            
            
            MyBaseClass baseClass2 = new MyBaseClass();
            baseClass2.BaseClassId = 1;
            String baseClass2JSON = new JavaScriptSerializer().Serialize(baseClass2);


            Console.WriteLine("derivedClass2JSON is: {0}", derivedClass2JSON);
            var x2 = new JavaScriptSerializer().Deserialize(derivedClass2JSON, typeof(MyDerivedClass));
            Console.WriteLine("derivedClass2JSON Deserialized type is: {0}", x2.GetType());

            var x3 = new JavaScriptSerializer().Deserialize(derivedClass2JSON, baseClass.GetType());
            Console.WriteLine("derivedClass2JSON Deserialized type is: {0}", x3.GetType());

            var x4 = new JavaScriptSerializer().Deserialize(baseClass2JSON, typeof(NotRealtedAnyhow));
            Console.WriteLine("baseClass2JSON Deserialized type is: {0}", x4.GetType());


            Console.ReadLine();
        }


        public class MyBaseClass
        {
            public int BaseClassId { get; set; }
        }

        public class MyDerivedClass : MyBaseClass
        {
            public int DerivedClassId { get; set; }
        }

        public class NotRealtedAnyhow
        {
            public int NotRealtedAnyhowId { get; set; }
        }
    }
}
