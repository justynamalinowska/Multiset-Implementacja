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
		public MultiSet()
		{
		}

        private Dictionary<T, int> mset = new Dictionary<T, int>();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => false;

        public IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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

        public MultiSet( IEnumerable<T> data)
        {
			foreach (var element in data)
			{
				this.Add();
			}
        }
    }
}

