﻿using System.Collections;
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
        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => false;

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var (item, multiplicity) in mset)
            {
                for (int i = 0; i < multiplicity; i++)
                    yield return item;

                yield break;
            }
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
            if (!mset.ContainsKey(item))
                mset.Add(item, 1);
            else
                mset[item]++;
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear() => mset.Clear();

        public bool Contains(T item) => mset.ContainsKey(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
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
    }
}

