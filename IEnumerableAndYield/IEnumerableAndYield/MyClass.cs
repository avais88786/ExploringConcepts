using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExploringConcepts.IEnumerableAndYield
{
    public class MyClass :IEnumerable
    {
        public object[] m_items = null;
        int freeIndex = 0;

        public MyClass()
        {
            m_items = new object[10];
        }

        public void add(object item)
        {
            m_items[freeIndex] = item;
            freeIndex++;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (object o in m_items)
            {
                yield return o;
            }
        }
    }
}
