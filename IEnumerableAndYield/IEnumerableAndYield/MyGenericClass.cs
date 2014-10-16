using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExploringConcepts.IEnumerableAndYield
{
    public class MyGenericClass<T> : IEnumerable<T>//,IComparable<T>
    {
        public IList<T> m_items = null;
        public IList<int> m_items2 = null;

        public MyGenericClass()
        {
            m_items = new List<T>();

        }

        public void add(T item)
        {
            m_items.Add(item);
        }




        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in m_items)
            {
                yield return item;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int CompareTo(T other)
        {
            int x = Convert.ToInt32(other);
            foreach (var item in m_items)
            {
                int y = Convert.ToInt32(item);

                if (y > x)
                    return -1;
                else if (y < x)
                    return 1;
                else
                    return 0;
            }
            return 0;
        }
    }
}
