using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System;
using System.Threading.Tasks;

namespace MultiSet
{
	public class MultiSet<T> : ICollection<T>
	{
        private Dictionary<T, int> mset = new Dictionary<T, int>();

        public MultiSet()
		{
		}

        public bool IsReadOnly => false;

        public int Count()
        {
            int counter = 0;
            foreach (var (item, multiplicity) in mset)
            {
                for (int i = 0; i < multiplicity; i++)
                    counter++;
            }
            return counter;
        }

        int ICollection<T>.Count => Count();

        //indexder
        //public static T operator this[int]

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var (item, multiplicity) in mset)
            {
                for (int i = 0; i < multiplicity; i++)
                    yield return item;  
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public MultiSet(IEnumerable<T> data)
        {
            foreach (var element in data)
            {
                this.Add(element);
            }
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            foreach (var (item, multiplicity) in mset)
            {
                output.Append($"{item}: {multiplicity}, ");
            }

            return output.ToString(0, output.Length-2);
        }

        public string ToStringExpanded()
        {
            StringBuilder output = new StringBuilder();
            foreach (var (item, multiplicity) in mset)
            {
                for (int i = 0; i < multiplicity; i++)
                {
                    output.Append($"{item}, ");
                }
            }

            return output.ToString(0, output.Length - 2);
        }

        public void Add(T item)
        {
            if (IsReadOnly)
                throw new NotSupportedException();

            if (!mset.ContainsKey(item))
                mset.Add(item, 1);
            else
                mset[item]++;
        }

        public bool Remove(T item)
        {
            if (IsReadOnly)
                throw new NotSupportedException();

            if (!mset.ContainsKey(item))
                return false;
            else
            {
                mset.Remove(item);
                return true;
            }
        }

        public void Clear() => mset.Clear();

        public bool Contains(T item) => mset.ContainsKey(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException();
            if (array is null)
                throw new ArgumentNullException();

            foreach (KeyValuePair<T, int> item in mset)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    array[arrayIndex + i] = item.Key;
                }
            }

        }

        private class MultiSetEnumerator : IEnumerator<T>
        {
            public T Current => throw new NotImplementedException();

            object IEnumerator.Current => Current;

            public void Dispose() {}

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }

        public MultiSet<T> Add(T item, int numberOfItems = 1)
        {
            if (IsReadOnly) throw new NotSupportedException();
            if (!mset.ContainsKey(item)) mset.Add(item, numberOfItems);
            else mset[item] =+ numberOfItems;

            return this;
        }

        public MultiSet<T> Remove(T item, int numberOfItems = 1)
        {
            if (IsReadOnly) throw new NotSupportedException();
            else if (!mset.ContainsKey(item)) return this;
            else for (int i = 0; i < numberOfItems; i++) mset[item] --;

            return this;     
        }

        public MultiSet<T> RemoveAll(T item)
        {
            if (IsReadOnly) throw new NotSupportedException();
            else if (!mset.ContainsKey(item)) return this;
            else mset[item] = 0;

            return this;
        }
    }
}

