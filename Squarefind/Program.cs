using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
// latest update: desktop 18/10

//list.length == list.Count !!!!!!
//cast is (type)
//Write("{0}+{1}",a,b) == "a+b"
//√
namespace Squarefind
{
    class Program
    {
        /// <summary>
        /// input Sumup(), output readable string
        /// <examples>
        /// SumupToReadable(Sumup(Factors(132)))=[2^2]*3*11
        /// </examples>
        /// </summary>
        /// <param name="sumup"></param>
        /// <returns></returns>
        public static string SumupToReadable(List<List<int>> sumup)
        {
            string bruh = "";
            int i = 0;
            foreach (var power in sumup)
            {
                if (i == 0)
                {
                    i = 1;
                }
                else
                {
                    bruh += "*";
                }
                if (power[1] == 1)
                {
                    bruh += power[0].ToString();
                }
                else
                {
                    bruh += "[" + power[0].ToString() + "^" + power[1].ToString() + "]";
                }

            }
            return bruh;
        }



        /// <summary>
        /// uses Factors and checks if first factor equals the value
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool IsPrime(int a)
        {
            return Factors(a)[0] == a;
        }


        public static void CommonRoots(int i1, int i2, int root)
        {
            for (int n = i1; n <= i2; n++)
            {
                var factors = Sumup(Factors(n));
                var newfactors = PopulateListt(factors.Count, 3);
                int finalin = 1;
                int finalout = 1;
                for (int i = 0; i < factors.Count; i++)
                {
                    newfactors[i] =
                        new List<int>
                        {
                        factors[i][1] - root * (int)Math.Floor((double)(factors[i][1] / root)),
                        (int)Math.Floor((double)(factors[i][1] / root)),
                        factors[i][0]
                        };


                    finalin *= (int)Math.Pow(newfactors[i][2], newfactors[i][0]);
                    finalout *= (int)Math.Pow(newfactors[i][2], newfactors[i][1]);


                }
                Console.Write("\n\n{0}√({1}) = ", root, n);
                if (finalout != 1)
                {
                    Console.Write("{0}", finalout);
                }
                if (finalout != 1 && finalin != 1)
                {
                    Console.Write("*");
                }
                if (finalin != 1)
                {
                    if (IsPrime(finalin))
                    {
                        Console.Write("{0}√({1})", root, finalin);
                    }
                    else if (finalout == 1)
                    {
                        Console.Write("{0}√({1})", root, SumupToReadable(Sumup(Factors(finalin))));
                    }
                    else
                    {
                        Console.Write("{0}√({1} == {2})", root, SumupToReadable(Sumup(Factors(finalin))), finalin);
                    }

                }
            }
        }





        /// <summary>
        /// Input Count, output int list of len COUNT (all 0)
        /// </summary>
        /// <param name="count"></param>
        /// <returns>List[0,0,0...Count]</returns>
        public static List<int> PopulateList(int count)
        {
            var _List = new List<int>();
            for (int i = 0; i < count; i++) { _List.Add(0); }
            return _List;
        }
        /// <summary>
        /// Input Count and Count2, output list (len COUNT) of int lists of len COUNT2 (all 0)
        /// </summary>
        /// <param name="count"></param>
        /// <param name="count2"></param>
        /// <returns>List[List[0,0,0...Count2]...Count]</returns>
        public static List<List<int>> PopulateListt(int count, int count2)
        {
            var _List = new List<List<int>>();
            for (int i = 0; i < count; i++) { _List.Add(new List<int>(new int[count2])); }
            return _List;
        }
        /// <summary>
        /// For i(0 to k), do (n^2-i^2)/4=ac, only outputs integers.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns>AC</returns>
        public static List<int> DiscriminantAC(int n, int k)
        {
            var disclist = new List<int>();
            double n2 = Math.Pow(n, 2);
            for (int i = 0; i < k; i++)
            {
                double i2 = (int)Math.Pow(i, 2);
                double diff = n2 - i2;

                if (diff / 4 == Math.Round(diff / 4))
                {
                    disclist.Add((int)diff / -4);

                }
            }
            return disclist;
        }
        /// <summary>
        /// Input DiscriminantAC, output to console all the A and C possible using NCRcomb
        /// </summary>
        /// <remarks>
        /// NOTE: Make sure first arg of DiscriminantAC and the second arg of this method are equal, or the script asynchronizes and outputs negative squares (pain)
        /// </remarks>
        /// <param name="AC"></param>
        public static void Discrimconsole(List<int> AC, int n)
        {
            var ACfactors = new List<List<int>>();
            foreach (var ac in AC)
            {
                var ACfactor = NCRcomb(Math.Abs(ac));
                int sign = Math.Sign(ac);
                var _templist = new List<int>();
                for (int i = 0; i < ACfactor.Count; i++)
                {
                    var _temp = 1;
                    for (int i1 = 0; i1 < ACfactor[i].Count; i1++)
                    {
                        _temp *= ACfactor[i][i1];
                    }
                    _templist.Add(_temp * sign);
                }
                ACfactors.Add(_templist);
            }
            Console.WriteLine("{0}-I^2=-4ac\n\n", Math.Pow(n, 2));
            for (int _ac = 0; _ac < AC.Count; _ac++)
            {
                int I = (int)Math.Pow(n, 2) + 4 * AC[_ac];
                Console.WriteLine("\n({0}^2)-{1}=-4*{2}", n, I, AC[_ac]);
                Console.WriteLine("i={0}", Math.Sqrt(I));
                foreach (var _a in ACfactors[_ac])
                {
                    if (AC[_ac] != 0)
                    {
                        var c = AC[_ac] / _a;
                        Console.WriteLine("({0},{1})", _a, c);
                    }
                    else
                    {
                        Console.WriteLine("(0,any) or (any,0)");
                    }


                }
            }

        }

        /// <summary>
        /// Output: List(int)[factors] Factors(8)=[2,2,2], Factors(26)=[2,13]
        /// Output: List(int)[factors]
        /// <examples>
        /// Factors(8)=[2,2,2], Factors(26)=[2,13]
        /// </examples>
        /// NOTE: sorts
        /// </summary>
        /// <remarks>
        /// NOTE: Factors are sorted
        /// </remarks>
        /// <param name="n"></param>
        /// <returns></returns>
        public static List<int> Factors(int n)
        {
            List<int> factors = new List<int>();
            int x = n;
            bool found = false;
            int k = 2;
            bool more = false;
            while (!found)
            {
                more = false;
                for (int i = k; i <= (int)Math.Floor(Math.Sqrt(x)); i++)
                {

                    if (x % i == 0)
                    {
                        factors.Add(i);
                        x /= i;
                        k = i;
                        more = true;
                        break;
                    }
                    k = i;
                }
                if (!more && k >= (int)Math.Floor(Math.Sqrt(x)))
                {
                    found = true;
                    factors.Add(x);
                }
            }

            return factors;
        }

        /// <summary>
        /// Output: Sumup(list(int))[ list( list(int)[no repeats], num of repeats ) ], Sumup(2,2,4,3):[(2,2),(4,1),(3,1)],Sumup(Factors(98)):[(2,1),(7,2)] 
        /// Output: Sumup(list(int))[ list( list(int)[no repeats], num of repeats ) ]
        /// <examples>
        /// Sumup(2,2,4,3):[(2,2),(4,1),(3,1)],Sumup(Factors(98)):[(2,1),(7,2)]
        /// </examples>
        /// NOTE: does not sort. 
        /// </summary>
        /// <remarks>
        /// NOTE: Factors appear in the order each unique factor appeared originally
        /// </remarks>
        /// <param name="List"></param>
        /// <returns></returns>

        public static List<List<int>> Sumup(List<int> List)
        {
            List<int> newList = new List<int>();
            List<List<int>> fullList = new List<List<int>>();
            foreach (int a in List)
            {
                if (!newList.Contains(a))
                {
                    List<int> temp = new List<int>();
                    newList.Add(a);
                    temp.Add(a);
                    temp.Add(1);
                    fullList.Add(temp);
                }
                else
                {
                    fullList[newList.IndexOf(a)][1]++;
                }
            }

            return fullList;
        }


        /// <summary>
        /// For n get n^2, find a^2 and b^2 where a^2-b^2=n^2, output List[List[a,b]], Example: SquareNegate(3)=[[5,4]], SquareNegate(12)=[[13,5],[15,9],[20,16],[37,35]]
        /// NOTE: no negatives are included. Bruh moment
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static List<List<int>> SquareNegate(int n)
        {
            int n2 = (int)Math.Pow(n, 2);
            var Final = new List<List<int>>();
            for (int a = n + 1; 2 * a - 1 <= n2; a++)
            {
                int a2 = (int)Math.Pow(a, 2);
                double dif = Math.Sqrt(a2 - n2);

                if (dif % 1 == 0)
                {

                    List<int> yes = new List<int>();
                    yes.Add(a);
                    yes.Add((int)dif);
                    Final.Add(yes);
                }

            }
            return Final;
        }

        /// <summary>
        /// does the same as SquareNegate but n^2=a^2+b^2. NOTE: no negatives are included. Bruh moment
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static List<List<int>> SquareAdd(int n)
        {
            int n2 = (int)Math.Pow(n, 2);
            var Final = new List<List<int>>();
            for (int a = 1; a < n; a++)
            {
                int a2 = (int)Math.Pow(a, 2);
                double dif = Math.Sqrt(n2 - a2);

                if (dif % 1 == 0)
                {

                    List<int> yes = new List<int>();
                    yes.Add(a);
                    yes.Add((int)dif);
                    Final.Add(yes);
                }

            }
            return Final;
        }

        /// <summary>
        /// Prettify of SquareNegate() into an easily readable version. Also checks for repeats (9,12,15==3,4,5)
        /// </summary>
        /// <param name="n"></param>
        public static void Fullsquare(int n)
        {
            int n2 = (int)Math.Pow(n, 2);
            foreach (List<int> sol in SquareNegate(n))
            {
                var factors1 = new List<int>();
                var factors2 = new List<int>();
                var factors3 = new List<int>();
                Console.WriteLine($"\n\n{n}={sol[0]}-{sol[1]}:   ({n2}={(int)Math.Pow(sol[0], 2)}-{(int)Math.Pow(sol[1], 2)})");
                Console.Write(n + " = ");
                foreach (List<int> factor in Sumup(Factors(n)))
                {
                    Console.Write(factor[0] + "^" + factor[1] + ", ");
                    factors1.Add(factor[0]);
                }
                Console.Write("\n" + sol[0] + " = ");
                foreach (List<int> factor in Sumup(Factors(sol[0])))
                {
                    Console.Write(factor[0] + "^" + factor[1] + ", ");
                    factors2.Add(factor[0]);
                }
                Console.Write("\n" + sol[1] + " = ");
                foreach (List<int> factor in Sumup(Factors(sol[1])))
                {
                    Console.Write(factor[0] + "^" + factor[1] + ", ");
                    factors3.Add(factor[0]);
                }
                var temp = 0;
                var n0 = n;
                var a = sol[0];
                var b = sol[1];
                var hasbeen = false;
                foreach (int factor in factors1)
                {

                    if (factors1.Contains(factor) && factors2.Contains(factor))
                    {
                        int y = Math.Min(Math.Min(Sumup(Factors(n))[temp][1], Sumup(Factors(sol[0]))[factors2.IndexOf(factor)][1]), Sumup(Factors(sol[1]))[factors3.IndexOf(factor)][1]);
                        n0 /= (int)Math.Pow(factor, y);
                        a /= (int)Math.Pow(factor, y);
                        b /= (int)Math.Pow(factor, y);
                        hasbeen = true;
                    }
                    temp++;
                }
                if (hasbeen)
                {
                    Console.Write($"\n OR {n0} = {a} - {b}");
                }
            }
            Console.Write("\n------------------------------------------");

        }

        /// <summary>
        /// relies on SUMUP and FACTORS (in this case, can be removed) to output a complete list of all possible combinations of a LIST of LIST of ints: (6)=3, (12)=5, (4)=2...
        /// </summary>
        /// <param name="_n"></param>
        /// <returns></returns>

        public static int NCRleng(int _n)
        {
            var _list = Sumup(Factors(_n));
            var _leng = _list.Count;
            int _total = 1;
            for (int i = 0; i < _leng; i++)
            {
                _total = _total * (_list[i][1] + 1);
            }
            _total -= 1;
            return _total;
        }

        /// <summary>
        /// NCR combination output from int input. List[List[int]]. Example: NCRcomb(6)=[[2],[3],[2,3]], NCRcomb(36)=[[2],[2,2],[3],[2,3],[2,2,3],[3,3],[2,3,3],[2,2,3,3]]
        /// </summary>
        /// <param name="_n"></param>
        /// <returns></returns>
        public static List<List<int>> NCRcomb(int _n)
        {
            var _list = Sumup(Factors(_n));

            var _leng = _list.Count;
            var _lenglist = new List<int>();
            int _total = 1;
            var _baselist = PopulateListt(_leng, 2);

            for (int i = 0; i < _leng; i++)
            {
                _baselist[i][0] = _total;
                _lenglist.Add(_list[i][1]);
                _total = _total * (_list[i][1] + 1);
                //PrintValue(i,_total);
            }
            var _done = new List<List<int>>();
            for (int n = 1; n < _total; n++)
            {
                var _prefinal = new List<int>();
                for (int _base = 0; _base < _leng; _base++)
                {
                    _baselist[_base][1] = (int)(Math.Floor((decimal)n / _baselist[_base][0]) % (_lenglist[_base] + 1));
                    //cast example
                    for (int i = 0; i < _baselist[_base][1]; i++)
                    {
                        _prefinal.Add(_list[_base][0]);
                    }
                }
                _done.Add(_prefinal);

            }
            return _done;
        }

        /// <summary>
        /// print list[list] (Arr)
        /// </summary>
        /// <param name="Arr"></param>
        public static void PrintValuess(List<List<int>> Arr)
        {

            for (int i = 0; i < Arr.Count; i++)
            {
                Console.Write("\n{0}:", i + 1);
                for (int i1 = 0; i1 < Arr[i].Count; i1++)
                {
                    Console.Write("\n  {0} = {1}", i1 + 1, Arr[i][i1]);
                }

            }
        }
        /// <summary>
        /// print list (int)
        /// </summary>
        /// <param name="Arr"></param>
        public static void PrintValues(List<int> Arr)
        {
            var a = 1;
            foreach (Object i in Arr)
            {
                Console.Write("\n{0} = {1}", a, i);
                a++;
            }
            Console.WriteLine();
        }
        /// <summary>
        /// print int n, or n=n1
        /// </summary>
        /// <param name="n"></param>
        /// <param name="n1"></param>
        public static void PrintValue(int n, int n1 = -69)
        {
            if (n1 != -69)
            {
                Console.Write("\n{0} = {1}", n, n1);
            }
            else { Console.Write("\n{0}", n); }

        }


        /// <summary>
        /// does nothing lmao
        /// </summary>
        public static void Pass() { }
        /// <summary>
        /// do the func n to n2 times, print out "i:" if yes!=0
        /// </summary>
        /// <example>
        /// 
        /// DoFromToWithN( (i) => func1( i,func2(i) ), 400, 800);
        /// mmm yes (action)delegates
        /// 
        /// </example>
        /// <param name="func"></param>
        /// <param name="n"></param>
        /// <param name="n2"></param>
        /// <param name="yes"></param>
        public static void DoFromToWithN(Action<int> func, int n, int n2, int yes = 0)
        {
            for (int i = n; i < n2; i++)
            {
                if (yes != 0)
                {
                    Console.Write("\n\n{0}:\n", i);
                }
                func(i);
            }
        }
        /// <summary>
        /// Outputs Desmos copy-pasteable version of List of points
        /// </summary>
        /// <param name="n"></param>
        /// <param name="n1"></param>
        public static void Desmosprint(int n, int n1)
        {
            Console.Write("({0},{1}),", n, n1);
        }

        static void Main(string[] args)
        {

            //PrintValuess(SquareAdd(5));

            //DoFromToWithN((i) => Desmosprint(i,NCRleng(i)), 400, 800);
            //var _list = new List<int>(new int[2]);
            //var _list = new List<int>();
            //_list.Add(0);
            //for (int i = 0; i < 100000; i++)
            //{
            //    if (_list[_list.Count - 1] < NCRleng(i))
            //    {
            //        Desmosprint(i, NCRleng(i));
            //        _list.Add(NCRleng(i));
            //    }
            //}
            //Discrimconsole(DiscriminantAC(2020,666), 2020);
            //PrintValues(DiscriminantAC(3,100));

            //Discrimconsole(DiscriminantAC(69,100), 69);
            //Console.WriteLine(SumupToReadable(Sumup(Factors(132))));
            //Fullsquare(40);
            CommonRoots(8, 400, 2);
            //PrintValuess(NCRcomb(33));

            //NCRcomb(10);

            //Console.Write(Sumup(Factors(64))[0][1]);

            //Fullsquare(10);
            //PrintValues();
            //Console.Write(Factors(685).Count);

        }
    }
}
