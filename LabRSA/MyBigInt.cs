using System;
using System.Collections.Generic;
using System.Text;

namespace LabRSA
{
    public class MyBigInt
    {
        public const int elementLength = 9;
        public const int maxSizeElement = 1000000000;
        private List<int> numberElements;
        public bool isNegative = false;
        public static void DeleteZeros(List<int> a)
        {
            while (a.Count > 1 && a[a.Count - 1] == 0)
                a.RemoveAt(a.Count - 1);
        }

        public MyBigInt(List<int> i)
        {
            numberElements = i;
        }

        public MyBigInt(MyBigInt i)
        {
            numberElements = i.numberElements;
            isNegative = i.isNegative;
        }

        public MyBigInt(string s)
        {
            FillElementsList(s);
        }

        public MyBigInt(long x)
        {
            isNegative = x < 0;
            x = Math.Abs(x);
            var s = new StringBuilder();
            while (x > 0)
            {
                s.Insert(0, (char)(sbyte)(x % 10 + '0'));
                x /= 10;
            }
            FillElementsList(s.ToString());
        }

        private void FillElementsList(string s)
        {
            numberElements = new List<int>();
            if (s.Length == 0)
            {
                numberElements.Add(0);
                return;
            }
            if (s[0] == '-' && s.Length > 1)
            {
                isNegative = true;
                s = s.Remove(0, 1);
            }
            while (s.Length % elementLength != 0)
                s = '0' + s;

            for (var i = 0; i < s.Length; i += elementLength)
            {
                var v = 0;
                for (var j = i; j < i + elementLength; j++)
                    v = v * 10 + (s[j] - '0');
                numberElements.Insert(0, v);
            }
            DeleteZeros(numberElements);
        }

        public override string ToString()
        {
            StringBuilder sr = new StringBuilder();
            sr.Append(numberElements[numberElements.Count - 1].ToString());
            for (int i = numberElements.Count - 2; i >= 0; i--)
            {
                string current = numberElements[i].ToString();
                int zeroCount = elementLength - current.Length;
                for (int j = 0; j < zeroCount; j++) { sr.Append('0'); }
                sr.Append(current);
            }

            return isNegative && numberElements.Count > 0 && !(numberElements.Count == 1 && numberElements[0] == 0) ? "-" + sr.ToString() : sr.ToString();
        }


        public static bool operator <(MyBigInt a, MyBigInt b)
        {
            if (a.isNegative && !b.isNegative)
                return true;
            if (!a.isNegative && b.isNegative)
                return false;
            DeleteZeros(a.numberElements);
            DeleteZeros(b.numberElements);
            if (a.numberElements.Count != b.numberElements.Count)
                return a.isNegative && b.isNegative ? (a.numberElements.Count > b.numberElements.Count) : (a.numberElements.Count < b.numberElements.Count);

            for (var i = a.numberElements.Count - 1; i >= 0; i--)
                if (a.numberElements[i] != b.numberElements[i])
                    return a.isNegative && b.isNegative ? (b.numberElements[i] < a.numberElements[i]) : (a.numberElements[i] < b.numberElements[i]);
            return false;
        }

        public static bool operator >(MyBigInt a, MyBigInt b)
        {
            return b < a;
        }
        public static bool operator ==(MyBigInt a, MyBigInt b)
        {
            return !(a < b) && !(b < a);
        }

        public static long ToLong(MyBigInt a)
        {
            long res = 0;
            long mul = 1;
            for (int i = 0; i < a.numberElements.Count; i++)
            {
                res += a.numberElements[i] * mul;
                mul *= maxSizeElement;
            }

            return a.isNegative ? (-1) * res : res;
        }

        public static bool operator !=(MyBigInt a, MyBigInt b)
        {
            return !(a == b);
        }

        public static bool operator <=(MyBigInt a, MyBigInt b)
        {
            return a < b || a == b;
        }

        public static bool operator >=(MyBigInt a, MyBigInt b)
        {
            return b < a || b == a;
        }

        private static MyBigInt SumPositive(MyBigInt a, MyBigInt b)
        {
            MyBigInt newInt = new MyBigInt(new List<int>());
            var carry = 0;
            for (var i = 0; i <= Math.Max(a.numberElements.Count, b.numberElements.Count) - 1; i++)
            {
                if (i < a.numberElements.Count)
                    carry += a.numberElements[i];
                if (i < b.numberElements.Count)
                    carry += b.numberElements[i];
                newInt.numberElements.Add((carry % maxSizeElement));
                carry /= maxSizeElement;
            }
            if (carry != 0)
                newInt.numberElements.Add(carry);
            DeleteZeros(newInt.numberElements);
            return newInt;
        }


        public static MyBigInt operator +(MyBigInt a, MyBigInt b)
        {
            if (!a.isNegative && b.isNegative)
            {
                MyBigInt copyB = new MyBigInt(b) { isNegative = false };
                return a - copyB;
            }
            else if (a.isNegative && !b.isNegative)
            {
                MyBigInt copyA = new MyBigInt(a) { isNegative = false };
                return b - copyA;
            }

            else if (a.isNegative && b.isNegative)
            {
                MyBigInt newInt = SumPositive(a, b);
                newInt.isNegative = true;
                return newInt;
            }
            else
                return SumPositive(a, b);
        }

        public static MyBigInt operator +(MyBigInt a, int b)
        {
            return a + new MyBigInt(b);
        }

        public static MyBigInt operator ++(MyBigInt a)
        {
            return a + 1;
        }


        public static MyBigInt operator -(MyBigInt a, MyBigInt b)
        {
            if (!a.isNegative && !b.isNegative)
            {
                if (a > b)
                    return MinusPositive(a, b);
                else
                    return new MyBigInt(MinusPositive(b, a)) { isNegative = true };
            }

            if (a.isNegative && !b.isNegative)
                return new MyBigInt(SumPositive(a, b)) { isNegative = true };

            if (!a.isNegative && b.isNegative)
                return SumPositive(a, b);

            else
            {
                if (a > b)
                    return MinusPositive(b, a);
                else
                    return new MyBigInt(MinusPositive(a, b)) { isNegative = true };
            }
        }

        private static MyBigInt MinusPositive(MyBigInt a, MyBigInt b)
        {
            var newInt = new MyBigInt(new List<int>());
            var carry = 0;
            for (var i = 0; i <= a.numberElements.Count - 1; i++)
            {
                carry += a.numberElements[i] - (i < b.numberElements.Count ? b.numberElements[i] : 0);
                if (carry < 0)
                {
                    newInt.numberElements.Add((carry + maxSizeElement));
                    carry = -1;
                }
                else
                {
                    newInt.numberElements.Add(carry);
                    carry = 0;
                }
            }
            DeleteZeros(newInt.numberElements);
            return newInt;
        }



        public static MyBigInt operator -(MyBigInt a, int b)
        {
            return a - new MyBigInt(b);
        }

        public static MyBigInt operator --(MyBigInt a)
        {
            return a - 1;
        }

        public static MyBigInt operator *(MyBigInt a, MyBigInt b)
        {
            MyBigInt newInt = new MyBigInt(new List<int>());
            for (var i = 0; i < a.numberElements.Count + b.numberElements.Count; i++)
                newInt.numberElements.Add(0);
            for (var i = 0; i <= a.numberElements.Count - 1; i++)
            {
                long carry = 0;
                for (var j = 0; j < b.numberElements.Count || carry > 0; j++)
                {
                    long s = (long)newInt.numberElements[i + j] + carry + (long)a.numberElements[i] * (long)(j < b.numberElements.Count ? b.numberElements[j] : 0);
                    newInt.numberElements[i + j] = (int)(s % maxSizeElement);
                    carry = s / maxSizeElement;
                }
            }
            DeleteZeros(newInt.numberElements);
            if (a.isNegative != b.isNegative)
                newInt.isNegative = true;
            return newInt;
        }

        public static MyBigInt operator *(MyBigInt a, int b)
        {
            return a * new MyBigInt(b);
        }


        public static MyBigInt operator /(MyBigInt a, MyBigInt b)
        {
            var newInt = new MyBigInt(new List<int>());
            var copyB = new MyBigInt(b) { isNegative = false };

            if (a.isNegative != b.isNegative)
                newInt.isNegative = true;

            if (copyB == new MyBigInt("0"))
                return new MyBigInt("-1");
            var cur = new MyBigInt(new List<int>());
            for (var i = a.numberElements.Count - 1; i >= 0; i--)
            {
                int x = NextBinSearch(a, copyB, cur, i);
                cur -= new MyBigInt(x) * copyB;
                newInt.numberElements.Insert(0, x);
            }
            DeleteZeros(newInt.numberElements);
            return newInt;
        }

        private static int NextBinSearch(MyBigInt a, MyBigInt copyB, MyBigInt cur, int i)
        {
            cur.isNegative = false;
            cur.numberElements.Insert(0, a.numberElements[i]);
            int x = 0, L = 0, R = maxSizeElement;

            while (L <= R)
            {
                int mid = (L + R) >> 1;
                if (copyB * new MyBigInt(mid) > cur)
                {
                    x = mid;
                    R = mid - 1;
                }
                else
                    L = mid + 1;
            }
            return x  -1 ;
        }

        public static MyBigInt operator /(MyBigInt a, int b)
        {
            return a / new MyBigInt(b);
        }


        public static MyBigInt operator %(MyBigInt a, MyBigInt b)
        {
            var copyB = new MyBigInt(b) { isNegative = false };
            if (copyB == new MyBigInt("0"))
                return new MyBigInt("-1");
            MyBigInt ans = new MyBigInt(new List<int>());

            for (var i = a.numberElements.Count - 1; i >= 0; i--)
            {
                int x = NextBinSearch(a, copyB, ans, i);
                ans -= new MyBigInt(x) * copyB;
            }

            DeleteZeros(ans.numberElements);
            ans.isNegative = a.isNegative;
            return ans;
        }

        public static MyBigInt operator %(MyBigInt a, int b)
        {
            return a % new MyBigInt(b);
        }
        public bool IsPrimeNumber()
        {
            if (this < new MyBigInt("2"))
                return false;
            if (this == new MyBigInt("2"))
                return true;

            for (long i = 2; i < ToLong(this); i++)
                if ((this % new MyBigInt(i)).ToString() == "0")
                    return false;
            return true;
        }

        public static Tuple<MyBigInt, MyBigInt, MyBigInt> ExtendedEuclid(MyBigInt a, MyBigInt b)
        {
            MyBigInt x;
            MyBigInt y;
            MyBigInt q;
            MyBigInt r;

            if (b == new MyBigInt("0"))
                return Tuple.Create(a, new MyBigInt("1"), new MyBigInt("0"));

            var x2 = new MyBigInt("1");
            var x1 = new MyBigInt("0");
            var y2 = new MyBigInt("0");
            var y1 = new MyBigInt("1");
            while (!b.isNegative)
            {
                q = a / b;
                r = a - q * b;
                x = x2 - q * x1;
                y = y2 - q * y1;
                a = b;
                b = r;
                x2 = x1;
                x1 = x;
                y2 = y1;
                y1 = y;
            }

            return Tuple.Create(a, x2, y2);
        }

        public static MyBigInt Inverse(MyBigInt a, MyBigInt n)
        {
            var e = ExtendedEuclid(a, n);

            if (e.Item1 == new MyBigInt("1"))
            {
                DeleteZeros(e.Item2.numberElements);
                return e.Item2.isNegative ? e.Item2 + n : e.Item2;
            }

            return new MyBigInt("0");
        }

        public MyBigInt Pow(MyBigInt power)
        {
            var pow = new MyBigInt(power);
            if (this == new MyBigInt("0") || this == new MyBigInt("1")) return this;
            MyBigInt r = new MyBigInt("1");
            var thisNumber = this;
            while (pow > new MyBigInt("0"))
            {
                if (pow % 2 == new MyBigInt("1"))
                    r *= thisNumber;
                thisNumber *= thisNumber;
                pow /= new MyBigInt("2");
            }
            DeleteZeros(r.numberElements);
            return r;
        }
    }
}
