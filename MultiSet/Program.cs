using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System;
using System.Collections.Concurrent;

namespace km.Collections.MultiZbior;

class Program
{
    static void Main()
    {
        char[] znaki = new char[] { 'a', 'a', 'b', 'c', 'd', 'd' };
        char[] znaki2 = new char[] { 'a', 'b', 'c', 'd' };
        var mz = new MultiSet<char>(znaki);
        var mz2 = new MultiSet<char>(znaki2);
        var mz3 = new MultiSet<char>();
        //Console.WriteLine(mz);
        //Console.WriteLine(mz.ToStringExpanded());

        //foreach (var item in mz)
        //{
        //    Console.WriteLine(item);
        //}

        //Console.WriteLine(mz.Count());

        //mz.UnionWith(mz2);
        //Console.WriteLine(mz.ToStringExpanded());

        //mz.IntersectWith(mz2);
        //Console.WriteLine(mz.ToStringExpanded());

        //mz.Add('d', 5);
        //Console.WriteLine("+5*d:");
        //Console.WriteLine(mz.ToStringExpanded());

        //mz.Remove('d', 2);
        //Console.WriteLine(mz.ToStringExpanded());

        //mz.RemoveAll('d');
        //Console.WriteLine(mz.ToStringExpanded());

        //char[] chars = new char[znaki.Length];

        //znaki.CopyTo(chars, 0);
        //var charss = new MultiSet<char>(chars);
        //Console.WriteLine(charss.ToStringExpanded());

        //mz.ExceptWith(mz2);
        //Console.WriteLine(mz.ToStringExpanded());

        //mz.SymmetricExceptWith(mz2);
        //Console.WriteLine(mz.ToStringExpanded());

        //Console.WriteLine(mz2.IsSubsetOf(mz));

        //Console.WriteLine(mz2.IsProperSubsetOf(mz));

        //Console.WriteLine(mz2.IsSupersetOf(mz));

        //Console.WriteLine(mz.IsProperSupersetOf(mz2));

        //Console.WriteLine(mz2.Overlaps(mz));

        //Console.WriteLine(mz2.MultiSetEquals(mz));

        //Console.WriteLine(mz2.IsEmpty);

        //Console.WriteLine(mz['a']);

        //mz.AsDictionary().ToString();
        //Console.WriteLine(mz['a']);

        //mz.AsSet();
        //Console.WriteLine(mz.ToStringExpanded());

        //Console.WriteLine(MultiSet<char>.Empty.ToString());

        //Console.WriteLine(mz3.ToString());

        //Console.WriteLine((mz + mz2).ToStringExpanded());

        //Console.WriteLine((mz - mz2).ToStringExpanded());

        //Console.WriteLine((mz * mz2).ToStringExpanded());

        //mz.Clear();
        
    }
}