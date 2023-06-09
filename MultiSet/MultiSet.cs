﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace km.Collections.MultiZbior
{
    public class MultiSet<T> : ICollection<T>, IMultiSet<T>
    {
        public Dictionary<T, int> mset = new Dictionary<T, int>();

        public MultiSet() { mset = new Dictionary<T, int>(); }

        public MultiSet(IEnumerable<T> data)
        {
            foreach (var element in data) { this.Add(element); }
        }

        public bool IsReadOnly => false;

        public int Count()
        {
            int counter = 0;
            foreach (var (item, multiplicity) in mset)
            {
                for (int i = 0; i < multiplicity; i++) counter++;
            }
            return counter;
        }

        int ICollection<T>.Count => Count();

        int IMultiSet<T>.Count => Count();

        public bool IsEmpty { get => this.Count() == 0; }

        public IEqualityComparer<T> Comparer => mset.Comparer;

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var (item, multiplicity) in mset)
            {
                for (int i = 0; i < multiplicity; i++) yield return item;  
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            foreach (var (item, multiplicity) in mset) { output.Append($"{item}: {multiplicity}, "); }

            if (output.Length > 2) return output.ToString(0, output.Length - 2);
            else return output.ToString();
        }

        public string ToStringExpanded()
        {
            StringBuilder output = new StringBuilder();
            foreach (var (item, multiplicity) in mset)
            {
                for (int i = 0; i < multiplicity; i++) output.Append($"{item}, ");
            }
            if (output.Length > 2) return output.ToString(0, output.Length - 2);
            else return output.ToString();
        }

        public void Add(T item)
        {
            if (IsReadOnly) throw new NotSupportedException();
            if (!mset.ContainsKey(item)) mset.Add(item, 1);
            else mset[item]++;
        }

        public bool Remove(T item)
        {
            if (IsReadOnly) throw new NotSupportedException();
            if (!mset.ContainsKey(item)) return false;
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

            int index = arrayIndex;
            foreach (KeyValuePair<T, int> item in mset)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    array[index] = item.Key;
                    index++;
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
            if (Contains(item)) mset.Remove(item);
            return this;
        }

        public MultiSet<T> UnionWith(IEnumerable<T> other)
        {
            if (other is null) throw new ArgumentNullException();
            else if (IsReadOnly) throw new NotSupportedException();

            foreach (var item in other) this.Add(item);
            return this;
        }

        public MultiSet<T> IntersectWith(IEnumerable<T> other) 
        {
            if (other is null) throw new ArgumentNullException();
            else if (IsReadOnly) throw new NotSupportedException();
            MultiSet<T> otherMset = new MultiSet<T>(other);

            foreach (var entry in mset)
            {
                if (otherMset.Contains(entry.Key))
                    mset[entry.Key] = Math.Min(entry.Value, otherMset.mset[entry.Key]);
                else
                    mset.Remove(entry.Key);
            }
            return this;
        }

        public MultiSet<T> ExceptWith(IEnumerable<T> other)
        {
            if (other is null) throw new ArgumentNullException();
            else if (IsReadOnly) throw new NotSupportedException();
            MultiSet<T> otherMset = new MultiSet<T>(other);
            foreach (var entry in otherMset)
            {
                if (mset.ContainsKey(entry))
                {
                    mset[entry] -= otherMset[entry];
                    if (mset[entry] <= 0) mset.Remove(entry);
                }
            }
            return this;
        }


        public MultiSet<T> SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other is null) throw new ArgumentNullException();
            else if (IsReadOnly) throw new NotSupportedException();
            MultiSet<T> otherMset = new MultiSet<T>(other);

            return this.UnionWith(otherMset).ExceptWith(this.IntersectWith(otherMset));
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            if (other is null) throw new ArgumentNullException();
            int counter = this.Count();
            this.IntersectWith(other);

            return (counter == this.Count());
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other is null) throw new ArgumentNullException();
            return (this.IsSubsetOf(other) && other.Count() > this.Count());
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            if (other is null) throw new ArgumentNullException();
            MultiSet<T> otherMultiSet = new MultiSet<T>(other);
            return otherMultiSet.IsSubsetOf(this);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other is null) throw new ArgumentNullException();
            MultiSet<T> otherMultiSet = new MultiSet<T>(other);
            return (otherMultiSet.IsSubsetOf(this) && other.Count() < this.Count());

        }

        public bool Overlaps(IEnumerable<T> other)
        {
            if (other is null) throw new ArgumentNullException();
            this.IntersectWith(other);
            return this.Count() > 0;
        }

        public bool MultiSetEquals(IEnumerable<T> other)
        {
            if (other is null) throw new ArgumentNullException();
            MultiSet<T> otherMultiSet = new MultiSet<T>(other);

            int counter = 0;
            if (this.Count() == other.Count())
            { 
                foreach (var entry in this)
                {
                    if (other.Contains(entry)) counter++;
                }
            }
            return counter == this.Count();
        }

        public int this[T item]
        {
            get
            {
                if (!this.Contains(item)) return 0;
                return mset[item];
            }
        }

        public IReadOnlyDictionary<T, int> AsDictionary()
        {
            Dictionary<T, int> mSetDictionary = new Dictionary<T, int>();

            foreach (T entry in this)
            {
                if (!mSetDictionary.ContainsKey(entry)) mSetDictionary.Add(entry, 1);
                else mSetDictionary[entry]++;
            }
            return mSetDictionary;
        }

        public IReadOnlySet<T> AsSet()
        {
            foreach (var item in mset) mset[item.Key] = 1;
            HashSet<T> msSortedset = new HashSet<T>(this);

            return msSortedset;
        }

        public static IMultiSet<T> Empty { get => new MultiSet<T>(); }

        public MultiSet(IEqualityComparer<T> comparer) { MultiSet<T> mset = new MultiSet<T>(comparer); }

        public MultiSet(IEnumerable<T> sequence, IEqualityComparer<T> comparer)
        {
            MultiSet<T> mset = new MultiSet<T>(comparer);
            foreach (var item in sequence) this.Add(item);
        }

        public static MultiSet<T> operator +(MultiSet<T> first, MultiSet<T> second)
        {
            if (first is null || second is null) throw new ArgumentNullException();
            return first.UnionWith(second);
        }

        public static MultiSet<T> operator -(MultiSet<T> first, MultiSet<T> second)
        {
            if (first is null || second is null) throw new ArgumentNullException();
            return first.ExceptWith(second);
        }

        public static MultiSet<T> operator *(MultiSet<T> first, MultiSet<T> second)
        {
            if (first is null || second is null) throw new ArgumentNullException();
            return first.IntersectWith(second);
        }
    }
}

