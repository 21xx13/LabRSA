using NUnit.Framework;

namespace LabRSA
{
    [TestFixture]
    class TestsMyBigInt
    {
        [Test]
        public void TestVeryLongPovitive()
        {
            var valueString = new MyBigInt("4388327487582347938491834832782738437");
            var valueLong = new MyBigInt(2384427463283775745);
            Assert.AreEqual("4388327487582347938491834832782738437", valueString.ToString());
            Assert.AreEqual("2384427463283775745", valueLong.ToString());
        }

        [Test]
        public void TestVeryLongNegative()
        {
            var value = new MyBigInt(-141253465);
            Assert.AreEqual("-141253465", value.ToString());

            value = new MyBigInt(-1412534462467356465);
            Assert.AreEqual("-1412534462467356465", value.ToString());

            value = new MyBigInt("-987654321123456789987654321123456789");
            Assert.AreEqual("-987654321123456789987654321123456789", value.ToString());

            value = new MyBigInt("-1");
            Assert.AreEqual("-1", value.ToString());
        }

        [Test]
        public void TestAddition()
        {
            var valuePositive1 = new MyBigInt("500");
            var valuePositive2 = new MyBigInt(451);
            var valueNegative1 = new MyBigInt("-500");
            var valueNegative2 = new MyBigInt(-451);
            Assert.AreEqual("951", (valuePositive1 + valuePositive2).ToString());
            Assert.AreEqual("951", (valuePositive2 + valuePositive1).ToString());
            Assert.AreEqual("-951", (valueNegative1 + valueNegative2).ToString());
            Assert.AreEqual("-951", (valueNegative2 + valueNegative1).ToString());

            Assert.AreEqual("49", (valuePositive1 + valueNegative2).ToString());
            Assert.AreEqual("-49", (valuePositive2 + valueNegative1).ToString());
            Assert.AreEqual("-49", (valueNegative1 + valuePositive2).ToString());
            Assert.AreEqual("49", (valueNegative2 + valuePositive1).ToString());
        }

        [Test]
        public void TestSubtraction()
        {
            var valuePositive1 = new MyBigInt("500");
            var valuePositive2 = new MyBigInt(451);
            var valueNegative1 = new MyBigInt("-500");
            var valueNegative2 = new MyBigInt(-451);

            Assert.AreEqual("49", (valuePositive1 - valuePositive2).ToString());
            Assert.AreEqual("-49", (valuePositive2 - valuePositive1).ToString());
            Assert.AreEqual("-49", (valueNegative1 - valueNegative2).ToString());
            Assert.AreEqual("49", (valueNegative2 - valueNegative1).ToString());

            Assert.AreEqual("951", (valuePositive1 - valueNegative2).ToString());
            Assert.AreEqual("951", (valuePositive2 - valueNegative1).ToString());
            Assert.AreEqual("-951", (valueNegative1 - valuePositive2).ToString());
            Assert.AreEqual("-951", (valueNegative2 - valuePositive1).ToString());
        }

        [Test]
        public void TestMult()
        {
            var valuePositive1 = new MyBigInt("5");
            var valuePositive2 = new MyBigInt(7);
            var valueNegative1 = new MyBigInt("-7");
            var valueNegative2 = new MyBigInt(-5);

            Assert.AreEqual("35", (valuePositive1 * valuePositive2).ToString());
            Assert.AreEqual("35", (valuePositive2 * valuePositive1).ToString());
            Assert.AreEqual("35", (valueNegative1 * valueNegative2).ToString());
            Assert.AreEqual("35", (valueNegative2 * valueNegative1).ToString());

            Assert.AreEqual("-25", (valuePositive1 * valueNegative2).ToString());
            Assert.AreEqual("-49", (valuePositive2 * valueNegative1).ToString());
            Assert.AreEqual("-49", (valueNegative1 * valuePositive2).ToString());
            Assert.AreEqual("-25", (valueNegative2 * valuePositive1).ToString());
        }

        [Test]
        public void TestDivision()
        {
            var valuePositive1 = new MyBigInt("5");
            var valuePositive2 = new MyBigInt(10);
            var valueNegative1 = new MyBigInt("-10");
            var valueNegative2 = new MyBigInt(-5);

            Assert.AreEqual("0", (valuePositive1 / valuePositive2).ToString());
            Assert.AreEqual("2", (valuePositive2 / valuePositive1).ToString());
            Assert.AreEqual("2", (valueNegative1 / valueNegative2).ToString());
            Assert.AreEqual("0", (valueNegative2 / valueNegative1).ToString());

            Assert.AreEqual("-1", (valuePositive1 / valueNegative2).ToString());
            Assert.AreEqual("-1", (valuePositive2 / valueNegative1).ToString());
            Assert.AreEqual("-1", (valueNegative1 / valuePositive2).ToString());
            Assert.AreEqual("-1", (valueNegative2 / valuePositive1).ToString());

            Assert.AreEqual("4785717377780040235609639974645642", (new MyBigInt("167500108222301408246337399112597504") / 35).ToString());
        }

        [Test]
        public void TestPow()
        {
            var valuePositive1 = new MyBigInt(4);
            var valuePositive2 = new MyBigInt(7);
            var valueNegative1 = new MyBigInt("-7");

            Assert.AreEqual("112109763052329492462373924058515033811659000095239858763076604943862126505628591526845808763910755415889269318882151764705506265513744484970964975616",
                new MyBigInt("45478").Pow(new MyBigInt("32")).ToString());
            Assert.AreEqual("16384", valuePositive1.Pow(valuePositive2).ToString());
            Assert.AreEqual("2401", valuePositive2.Pow(valuePositive1).ToString());
            Assert.AreEqual("-823543", valueNegative1.Pow(valuePositive2).ToString());
            Assert.AreEqual("2401", valueNegative1.Pow(valuePositive1).ToString());
        }

        [Test]
        public void TestModule()
        {
            var valuePositive1 = new MyBigInt("12");
            var valuePositive2 = new MyBigInt(5);
            var valueNegative1 = new MyBigInt("-12");
            var valueNegative2 = new MyBigInt(-5);

            Assert.AreEqual("2", (valuePositive1 % valuePositive2).ToString());
            Assert.AreEqual("2", (valuePositive1 % valueNegative2).ToString());
            Assert.AreEqual("-2", (valueNegative1 % valuePositive2).ToString());
            Assert.AreEqual("-2", (valueNegative1 % valueNegative2).ToString());

            Assert.AreEqual("34", (new MyBigInt("167500108222301408246337399112597504") % 35).ToString());
            Assert.AreEqual("21", (new MyBigInt("2576580875108218291929075869661") % 35).ToString());
        }

        [Test]
        public void TestComparison()
        {
            var valuePositive1 = new MyBigInt("5");
            var valuePositive2 = new MyBigInt(10);
            var valueNegative1 = new MyBigInt("-10");
            var valueNegative2 = new MyBigInt(-5);

            Assert.AreEqual(true, valuePositive1 < valuePositive2);
            Assert.AreEqual(false, valuePositive2 < valuePositive1);
            Assert.AreEqual(false, valueNegative1 > valueNegative2);
            Assert.AreEqual(true, valueNegative2 > valueNegative1);

            Assert.AreEqual(true, valuePositive1 > valueNegative2);
            Assert.AreEqual(false, valuePositive2 < valueNegative1);

        }

        [Test]
        public void TestEquals()
        {
            var valuePositive1 = new MyBigInt("5");
            var valuePositive2 = new MyBigInt(10);
            var valuePositive3 = new MyBigInt("10");
            var valueNegative1 = new MyBigInt("-10");
            var valueNegative2 = new MyBigInt(-5);
            var valueNegative3 = new MyBigInt("-5");

            Assert.AreEqual(false, valuePositive1 == valuePositive2);
            Assert.AreEqual(false, valuePositive2 == valueNegative1);
            Assert.AreEqual(false, valueNegative1 == valueNegative2);
            Assert.AreEqual(false, valueNegative2 == valueNegative1);

            Assert.AreEqual(true, valuePositive2 == valuePositive2);
            Assert.AreEqual(true, valueNegative3 == valueNegative3);
            Assert.AreEqual(true, valuePositive2 == valuePositive3);
            Assert.AreEqual(true, valueNegative2 == valueNegative3);
        }

        [Test]
        public void TestInverse()
        {
            var value1 = new MyBigInt("11");
            var value2 = new MyBigInt(15);
            var value3 = new MyBigInt(3);
            var value4 = new MyBigInt(26);

            Assert.AreEqual("11", MyBigInt.Inverse(value1, value2).ToString());
            Assert.AreEqual("3", MyBigInt.Inverse(value2, value1).ToString());
            Assert.AreEqual("9", MyBigInt.Inverse(value3, value4).ToString());
            Assert.AreEqual("2", MyBigInt.Inverse(value4, value3).ToString());
        }
    }
}
