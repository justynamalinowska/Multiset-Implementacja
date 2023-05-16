using MultiSet;
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
        char[] znaki = new char[] { 'a', 'a', 'b', 'c'};
        char[] znaki2 = new char[] { 'a', 'b', 'c', 'd', 'e' };
        var mz = new MultiSet<char>(znaki);
        var mz2 = new MultiSet<char>(znaki2);
        Console.WriteLine(mz);
        Console.WriteLine(mz.ToStringExpanded());

        foreach (var item in mz)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine(mz.Count());
        mz.UnionWith(mz2);
        Console.WriteLine(mz.ToStringExpanded());


        mz.Add('d', 5);
        Console.WriteLine("+5*d:");
        Console.WriteLine(mz.ToStringExpanded());

        mz.Remove('d', 2);
        Console.WriteLine(mz.ToStringExpanded());

        mz.RemoveAll('d');
        Console.WriteLine(mz.ToStringExpanded());

        char[] chars = new char[znaki.Length];

        znaki.CopyTo(chars, 0);
        var charss = new MultiSet<char>(chars);
        Console.WriteLine(charss.ToStringExpanded());


        mz.Clear();
        
    }
}