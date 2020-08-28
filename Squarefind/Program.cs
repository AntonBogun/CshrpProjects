using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Squarefind
{
    class Program
    {
        public static List<int> factors(int n)
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
                   
                    if (x%i==0)
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

        public static List<List<int>> sumup(List<int> List)
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


        public static List<List<int>> square(int n)
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
        public static void fullsquare(int n)
        {
            int n2 = (int)Math.Pow(n, 2);
            foreach (List<int> sol in square(n))
            {
                var factors1 = new List<int>();
                var factors2 = new List<int>();
                var factors3 = new List<int>();
                Console.WriteLine($"\n\n{n}={sol[0]}-{sol[1]}:   ({n2}={(int)Math.Pow(sol[0], 2)}-{(int)Math.Pow(sol[1], 2)})");
                Console.Write(n + " = ");
                foreach (List<int> factor in sumup(factors(n)))
                {
                    Console.Write(factor[0] + "^" + factor[1] + ", ");
                    factors1.Add(factor[0]);
                }
                Console.Write("\n" + sol[0] + " = ");
                foreach (List<int> factor in sumup(factors(sol[0])))
                {
                    Console.Write(factor[0] + "^" + factor[1] + ", ");
                    factors2.Add(factor[0]);
                }
                Console.Write("\n" + sol[1] + " = ");
                foreach (List<int> factor in sumup(factors(sol[1])))
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
                        int y = Math.Min(Math.Min(sumup(factors(n))[temp][1], sumup(factors(sol[0]))[factors2.IndexOf(factor)][1]), sumup(factors(sol[1]))[factors3.IndexOf(factor)][1]);
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
        static void Main(string[] args)
        {
            
            for (int n = 1; n <200; n++)
            {
                fullsquare(n);
            }
            
        }
    }
}
