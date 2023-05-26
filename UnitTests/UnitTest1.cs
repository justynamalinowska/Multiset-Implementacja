using System.Collections;
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
using km.Collections.MultiZbior;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Tests
{
    [TestClass]
    public class EmptyConstructors
    {
        [TestMethod]
        public void Char()
        {
            var test = new MultiSet<char>();
            CollectionAssert.AreEqual(test.mset, new Dictionary<char, int>());
        }
        [TestMethod]
        public void String()
        {
            var test = new MultiSet<string>();
            CollectionAssert.AreEqual(test.mset, new Dictionary<string, int>());
        }
    }

    [TestClass]
    public class ConstructorsWithData
    {
        [DataTestMethod]
        [DataRow('a', 'b', 'c', 'd')]
        [DataRow('e', 'f', 'g', 'h')]
        [DataRow('i', 'j', 'k', 'l')]
        public void Char(char ch1, char ch2, char ch3, char ch4)
        {
            var list = new List<char> { ch1, ch2, ch3, ch4 };
            var actual = new MultiSet<char>(list);
            Assert.AreEqual(actual.Contains(ch1), true);
            Assert.AreEqual(actual.Contains(ch2), true);
            Assert.AreEqual(actual.Contains(ch3), true);
            Assert.AreEqual(actual.Contains(ch4), true);
        }

        [DataTestMethod]
        [DataRow("aaa", "bbb", "ccc", "ddd")]
        [DataRow("aafda", "bdfvsdbb", "ccaffvc", "ddasdcd")]
        [DataRow("avsdaa", "bczbb", "cdeccc", "ddjnfd")]
        public void String(string s1, string s2, string s3, string s4)
        {
            var list = new List<string> { s1, s2, s3, s4 };
            var actual = new MultiSet<string>(list);
            Assert.AreEqual(actual.Contains(s1), true);
            Assert.AreEqual(actual.Contains(s2), true);
            Assert.AreEqual(actual.Contains(s3), true);
            Assert.AreEqual(actual.Contains(s4), true);
        }
    }

    [TestClass]
    public class MethodsFromICollection
    {

        [DataTestMethod]
        [DataRow('a', 'b', 'c', 'd', 4)]
        public void CharCount(char ch1, char ch2, char ch3, char ch4, int length)
        {
            var list = new List<char> { ch1, ch2, ch3, ch4 };
            var set = new MultiSet<char>(list);
            Assert.AreEqual(set.Count(), length);
        }

        [DataTestMethod]
        [DataRow("aaa", "bbb", "ccc", "ddd", 4)]
        public void StringCount(string s1, string s2, string s3, string s4, int length)
        {
            var list = new List<string> { s1, s2, s3, s4 };
            var set = new MultiSet<string>(list);
            Assert.AreEqual(set.Count(), length);
        }

        [DataTestMethod]
        public void IsEmpty()
        {
            var set = new MultiSet<string>();
            Assert.AreEqual(set.IsEmpty, true);
        }

        [DataTestMethod]
        [DataRow("aaa", "bbb", "ccc", "ddd")]
        public void IsNotEmpty(string s1, string s2, string s3, string s4)
        {
            var list = new List<string> { s1, s2, s3, s4 };
            var set = new MultiSet<string>(list);
            Assert.AreEqual(set.IsEmpty, false);
        }

        [DataTestMethod]
        public void AddStrings()
        {
            var set = new MultiSet<string>();

            set.Add("aaa");
            set.Add("aaa");
            set.Add("bbb");

            Assert.AreEqual(2, set["aaa"]);
            Assert.AreEqual(1, set["bbb"]);
        }

        [DataTestMethod]
        public void AddChars()
        {
            var set = new MultiSet<char>();

            set.Add('a');
            set.Add('a');
            set.Add('b');

            Assert.AreEqual(2, set['a']);
            Assert.AreEqual(1, set['b']);
        }

        [DataTestMethod]
        public void RemoveStrings()
        {
            var list = new List<string> { "aaa", "aaa", "ccc", "bbb" };
            var set = new MultiSet<string>(list);

            set.Remove("aaa");
            set.Remove("aaa");
            set.Remove("bbb");

            Assert.AreEqual(set.Count(), 1);
            Assert.AreEqual(0, set["aaa"]);
            Assert.AreEqual(0, set["bbb"]);
            Assert.AreEqual(1, set["ccc"]);
        }


        [DataTestMethod]
        public void RemoveChars()
        {
            var list = new List<char> { 'a', 'a', 'c', 'd' };
            var set = new MultiSet<char>(list);

            set.Remove('a');
            set.Remove('a');
            set.Remove('d');

            Assert.AreEqual(set.Count(), 1);
            Assert.AreEqual(0, set['a']);
            Assert.AreEqual(0, set['b']);
            Assert.AreEqual(1, set['c']);
        }

        [DataTestMethod]
        [DataRow('a', 'b', 'c', 'd')]
        [DataRow('e', 'f', 'g', 'h')]
        [DataRow('i', 'j', 'k', 'l')]
        public void ContainsChars(char ch1, char ch2, char ch3, char ch4)
        {
            var list = new List<char> { ch1, ch2, ch3, ch4 };
            var actual = new MultiSet<char>(list);
            Assert.AreEqual(actual.Contains(ch1), true);
            Assert.AreEqual(actual.Contains(ch2), true);
            Assert.AreEqual(actual.Contains(ch3), true);
            Assert.AreEqual(actual.Contains(ch4), true);
        }

        [DataTestMethod]
        [DataRow("aaa", "bbb", "ccc", "ddd")]
        [DataRow("aafda", "bdfvsdbb", "ccaffvc", "ddasdcd")]
        [DataRow("avsdaa", "bczbb", "cdeccc", "ddjnfd")]
        public void ContainsStrings(string s1, string s2, string s3, string s4)
        {
            var list = new List<string> { s1, s2, s3, s4 };
            var actual = new MultiSet<string>(list);
            Assert.AreEqual(actual.Contains(s1), true);
            Assert.AreEqual(actual.Contains(s2), true);
            Assert.AreEqual(actual.Contains(s3), true);
            Assert.AreEqual(actual.Contains(s4), true);
        }

        [DataTestMethod]
        [DataRow(new char[] { 'b', 'c' }, 0, new char[] { 'a', 'a', 'a' }, new char[] { 'b', 'c', 'a' })]
        [DataRow(new char[] { 'b', 'c' }, 1, new char[] { 'a', 'a', 'a' }, new char[] { 'a', 'b', 'c' })]
        public void CopyToForChars(char[] list, int index, char[] array, char[] newArray)
        {
            var actual = new MultiSet<char>(list);
            actual.CopyTo(array, index);

            CollectionAssert.AreEqual(array, newArray);
        }

        [DataTestMethod]
        [DataRow(new string[] { "efg", "kgbs" }, 0, new string[] { "abc", "abc", "abc" }, new string[] { "efg", "kgbs", "abc" })]
        [DataRow(new string[] { "efg", "kgbs" }, 1, new string[] { "abc", "abc", "abc" }, new string[] { "abc", "efg", "kgbs" })]
        public void CopyToForStrings(string[] list, int index, string[] array, string[] newArray)
        {
            var actual = new MultiSet<string>(list);
            actual.CopyTo(array, index);

            CollectionAssert.AreEqual(array, newArray);
        }
    }

    [TestClass]
    public class MethodsFromSetTheory
    {
        [DataTestMethod]
        [DataRow(new char[] { 'b', 'c' }, new char[] { 'a' }, new char[] { 'b', 'c', 'a' })]
        [DataRow(new char[] { 'b', 'c' }, new char[] { 'a', 'a', 'a' }, new char[] { 'b', 'c', 'a', 'a', 'a' })]
        public void UnionWithForChars(char[] list, char[] list2, char[] charsUnion)
        {
            var list1 = new MultiSet<char>(list);
            var union = new MultiSet<char>(charsUnion);
            list1.UnionWith(list2);

            Assert.IsTrue(list1.MultiSetEquals(union));
        }

        [DataTestMethod]
        [DataRow(new string[] { "aaa" }, new string[] { "bbb", "bbb" }, new string[] { "aaa", "bbb", "ccc" })]
        [DataRow(new string[] { }, new string[] { "aaa", "bbb", "ccc" }, new string[] { "aaa", "bbb", "ccc" })]
        public void UnionWithForStrings(string[] list, string[] list2, string[] charsUnion)
        {
            var list1 = new MultiSet<string>(list);
            var union = new MultiSet<string>(charsUnion);
            list1.UnionWith(list2);

            Assert.IsTrue(list1.MultiSetEquals(union));
        }

        [DataTestMethod]
        [DataRow(new char[] { 'b', 'c' }, new char[] { 'a', 'b', 'c' }, new char[] { 'b', 'c' })]
        [DataRow(new char[] { 'a', 'a', 'b', 'c', 'd', 'd' }, new char[] { 'a', 'a', 'a', 'c', 'd' }, new char[] { 'a', 'a', 'c', 'd' })]
        public void IntersectWithForChars(char[] list, char[] list2, char[] charsIntersection)
        {
            var list1 = new MultiSet<char>(list);
            var intersection = new MultiSet<char>(charsIntersection);
            list1.IntersectWith(list2);

            Assert.IsTrue(list1.MultiSetEquals(intersection));
        }

        [DataTestMethod]
        [DataRow(new string[] { "aaa", "bbb", "bbb" }, new string[] { "aaa", "bbb" }, new string[] { "aaa", "bbb" })]
        [DataRow(new string[] { "aaa", "aaa", "bbb", "bbb", "ccc" }, new string[] { "aaa", "bbb", "ccc" }, new string[] { "aaa", "bbb", "ccc" })]
        public void IntersectWithForStrings(string[] list, string[] list2, string[] stringIntersection)
        {
            var list1 = new MultiSet<string>(list);
            var intersection = new MultiSet<string>(stringIntersection);
            list1.IntersectWith(list2);

            Assert.IsTrue(list1.MultiSetEquals(intersection));
        }

        [DataTestMethod]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'b', 'c' }, new char[] { 'a' })]
       // [DataRow(new char[] { 'a', 'a', 'b', 'c', 'd', 'd' }, new char[] { 'a', 'c', 'd' }, new char[] { 'a', 'b', 'd' })]
        public void ExceptWithForChars(char[] list, char[] list2, char[] charsException)
        {
            var list1 = new MultiSet<char>(list);
            var exc = new MultiSet<char>(charsException);
            list1.ExceptWith(list2);

            Assert.IsTrue(list1.MultiSetEquals(exc));
        }

        [DataTestMethod]
        [DataRow(new string[] { "aaa", "bbb", "bbb" }, new string[] { "aaa", "bbb" }, new string[] { "bbb" })]
        [DataRow(new string[] { "aaa", "aaa", "bbb", "bbb", "ccc" }, new string[] { "aaa", "bbb", "ccc" }, new string[] { "aaa", "bbb"})]
        public void ExceptWithForStrings(string[] list, string[] list2, string[] stringException)
        {
            var list1 = new MultiSet<string>(list);
            var exc = new MultiSet<string>(stringException);
            list1.ExceptWith(list2);

            Assert.IsTrue(list1.MultiSetEquals(exc));
        }

        [DataTestMethod]
        [DataRow(new char[] { 'b', 'c' }, new char[] { 'a', 'b', 'c' }, new char[] { 'b', 'c' })]
        [DataRow(new char[] { 'a', 'a', 'b', 'c', 'd', 'd' }, new char[] { 'a', 'a', 'a', 'c', 'd' }, new char[] { 'a', 'a', 'c', 'd' })]
        public void SymmetricExceptWithForChars(char[] list, char[] list2, char[] expected)
        {
            var multiset1 = new MultiSet<char>(list);
            var multiset2 = new MultiSet<char>(list2);
            var expectedMultiset = new MultiSet<char>(expected);

            multiset1.SymmetricExceptWith(multiset2);

            Assert.IsTrue(multiset1.MultiSetEquals(expectedMultiset));
        }

        [DataTestMethod]
        [DataRow(new string[] { "aaa", "bbb", "bbb" }, new string[] { "aaa", "bbb" }, new string[] { "aaa", "bbb" })]
        [DataRow(new string[] { "aaa", "aaa", "bbb", "bbb", "ccc" }, new string[] { "aaa", "bbb", "ccc" }, new string[] { "aaa", "bbb", "ccc" })]
        public void SymmetricExceptWithForStrings(string[] list, string[] list2, string[] expected)
        {
            var multiset1 = new MultiSet<string>(list);
            var multiset2 = new MultiSet<string>(list2);
            var expectedMultiset = new MultiSet<string>(expected);

            multiset1.SymmetricExceptWith(multiset2);

            Assert.IsTrue(multiset1.MultiSetEquals(expectedMultiset));
        }

        [DataTestMethod]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'a', 'b', 'c', 'd', 'e' }, true)]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'a', 'b', 'd', 'e' }, false)]
        [DataRow(new char[] { }, new char[] { 'a', 'b', 'c' }, true)]
        [DataRow(new char[] { }, new char[] { }, true)]
        public void IsSubsetOfForChars(char[] subset, char[] superset, bool expected)
        {
            var subsetMultiset = new MultiSet<char>(subset);
            var supersetMultiset = new MultiSet<char>(superset);

            var result = subsetMultiset.IsSubsetOf(supersetMultiset);

            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(new string[] { "aaa", "bbb", "ccc" }, new string[] { "aaa", "bbb", "ccc", "ddd", "eee" }, true)]
        [DataRow(new string[] { "aaa", "bbb", "ccc" }, new string[] { "aaa", "bbb", "ddd", "eee" }, false)]
        [DataRow(new string[] { }, new string[] { "aaa", "bbb", "ccc" }, true)]
        [DataRow(new string[] { }, new string[] { }, true)]
        public void IsSubsetOfForStrings(string[] subset, string[] superset, bool expected)
        {
            var subsetMultiset = new MultiSet<string>(subset);
            var supersetMultiset = new MultiSet<string>(superset);

            var result = subsetMultiset.IsSubsetOf(supersetMultiset);

            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'a', 'b', 'c', 'd', 'e' }, true)]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'a', 'b', 'c' }, false)]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'a', 'b', 'd', 'e' }, false)]
        public void IsProperSubsetOfForChars(char[] subset, char[] superset, bool expected)
        {
            var subsetMultiset = new MultiSet<char>(subset);
            var supersetMultiset = new MultiSet<char>(superset);

            var result = subsetMultiset.IsProperSubsetOf(supersetMultiset);

            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(new string[] { "aaa", "bbb", "ccc" }, new string[] { "aaa", "bbb", "ccc", "ddd", "eee" }, true)]
        [DataRow(new string[] { "aaa", "bbb", "ccc" }, new string[] { "aaa", "bbb", "ccc" }, false)]
        [DataRow(new string[] { "aaa", "bbb", "ccc" }, new string[] { "aaa", "bbb", "ddd", "eee" }, false)]
        public void IsProperSubsetOfForStrings(string[] subset, string[] superset, bool expected)
        {
            var subsetMultiset = new MultiSet<string>(subset);
            var supersetMultiset = new MultiSet<string>(superset);

            var result = subsetMultiset.IsProperSubsetOf(supersetMultiset);

            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(new char[] { 'a', 'b', 'c', 'd', 'e' }, new char[] { 'a', 'b', 'c' }, true)]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'a', 'b', 'c', 'd', 'e' }, false)]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'a', 'b', 'c' }, true)]
        public void IsSupersetOfForChars(char[] superset, char[] subset, bool expected)
        {
            var supersetMultiset = new MultiSet<char>(superset);
            var subsetMultiset = new MultiSet<char>(subset);

            var result = supersetMultiset.IsSupersetOf(subsetMultiset);

            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(new string[] { "aaa", "bbb", "ccc", "ddd", "eee" }, new string[] { "aaa", "bbb", "ccc" }, true)]
        [DataRow(new string[] { "aaa", "bbb", "ccc" }, new string[] { "aaa", "bbb", "ccc", "ddd", "eee" }, false)]
        [DataRow(new string[] { "aaa", "bbb", "ccc" }, new string[] { "aaa", "bbb", "ccc" }, true)]
        public void IsSupersetOfForStrings(string[] superset, string[] subset, bool expected)
        {
            var supersetMultiset = new MultiSet<string>(superset);
            var subsetMultiset = new MultiSet<string>(subset);

            var result = supersetMultiset.IsSupersetOf(subsetMultiset);

            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(new char[] { 'a', 'b', 'c', 'd', 'e' }, new char[] { 'a', 'b', 'c' }, true)]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'a', 'b', 'c', 'd', 'e' }, false)]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'a', 'b', 'c' }, false)]
        public void IsProperSupersetOfForChars(char[] superset, char[] subset, bool expected)
        {
            var supersetMultiset = new MultiSet<char>(superset);
            var subsetMultiset = new MultiSet<char>(subset);

            var result = supersetMultiset.IsProperSupersetOf(subsetMultiset);

            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(new string[] { "aaa", "bbb", "ccc", "ddd", "eee" }, new string[] { "aaa", "bbb", "ccc" }, true)]
        [DataRow(new string[] { "aaa", "bbb", "ccc" }, new string[] { "aaa", "bbb", "ccc", "ddd", "eee" }, false)]
        [DataRow(new string[] { "aaa", "bbb", "ccc" }, new string[] { "aaa", "bbb", "ccc" }, false)]
        public void IsProperSupersetOfForStrings(string[] superset, string[] subset, bool expected)
        {
            var supersetMultiset = new MultiSet<string>(superset);
            var subsetMultiset = new MultiSet<string>(subset);

            var result = supersetMultiset.IsProperSupersetOf(subsetMultiset);

            Assert.AreEqual(expected, result);
        }




    }


}