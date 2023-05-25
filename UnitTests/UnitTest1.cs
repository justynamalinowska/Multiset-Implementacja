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
    }
}