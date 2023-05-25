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
            var list = new List<char> { ch1, ch2, ch3 };
            var actual = new MultiSet<char>(list);
            Assert.AreEqual(actual.Contains(ch1), true);
            Assert.AreEqual(actual.Contains(ch2), true);
            Assert.AreEqual(actual.Contains(ch3), true);
            Assert.AreEqual(actual.Contains(ch4), false);
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
}