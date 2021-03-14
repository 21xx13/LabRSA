using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace LabRSA
{
    [TestFixture]
    class TestsRSA
    {
        [Test]
        public void TestInputOutput()
        {
            var input = ReadFile("D:/c#/LabRSA/LabRSA/bin/Debug/in.txt");
            var output = ReadFile("D:/c#/LabRSA/LabRSA/bin/Debug/out2.txt");
            Assert.AreEqual(input, output);
        }

        [Test]
        public void TestInputOutputWithEncode()
        {
            var input = ReadFile("in.txt");
            var coder = new RSA();
            coder.Encrypt(new MyBigInt(11), new MyBigInt(13));
            coder.Decipher(new MyBigInt(17), new MyBigInt(143));
            var output = ReadFile("out2.txt");
            Assert.AreEqual(input, output);
        }

        [Test]
        public void TestCalculateE()
        {
            var coder = new RSA();
            coder.Encrypt(new MyBigInt(11), new MyBigInt(13));
            Assert.AreEqual(coder.PublicKey.Item1.ToString(), new MyBigInt(113).ToString());
            coder.Encrypt(new MyBigInt(31), new MyBigInt(13));
            Assert.AreEqual(coder.PublicKey.Item1.ToString(), new MyBigInt(359).ToString());
        }

        [Test]
        public void TestCalculateD()
        {
            var coder = new RSA();
            coder.Encrypt(new MyBigInt(11), new MyBigInt(13));
            Assert.AreEqual(coder.PrivateKey.Item1.ToString(), new MyBigInt(17).ToString());
            coder.Encrypt(new MyBigInt(31), new MyBigInt(13));
            Assert.AreEqual(coder.PrivateKey.Item1.ToString(), new MyBigInt(719).ToString());
        }
        [Test]
        public void TestCalculateN()
        {
            var coder = new RSA();
            coder.Encrypt(new MyBigInt(11), new MyBigInt(13));
            Assert.AreEqual(coder.PrivateKey.Item2.ToString(), new MyBigInt(143).ToString());
            coder.Encrypt(new MyBigInt(31), new MyBigInt(13));
            Assert.AreEqual(coder.PrivateKey.Item2.ToString(), new MyBigInt(403).ToString());
        }

        [Test]
        public void TestEncode()
        {
            var coder = new RSA();
            coder.Encrypt(new MyBigInt(11), new MyBigInt(13));
            string output = "";
            StreamReader sr = new StreamReader("out1.txt");
            while (!sr.EndOfStream)
                output += ("\n" + sr.ReadLine()); ;
            var res = new List<string>() {"28", "4", "4", "107", "125", "117",  "42", "10", "117", "45", "74", "78", "45", "3", 
                "34", "7", "10", "39", "29", "45", "74", "78", "45", "62", "90", "10", "82", "117", "14", "118", "45", "60",
                "10", "90", "71"};
            string resStr = "";
            foreach (var e in res) {
                resStr += "\n" + e;
            }
            Assert.AreEqual(output, resStr);
        }



        public string ReadFile(string name)
        {
            string s = "";
            StreamReader sr = new StreamReader(name);
            while (!sr.EndOfStream)
                s += sr.ReadLine();
            return s;
        }
    }
}
