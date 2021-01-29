using System;
using System.Collections.Generic;
using System.Linq;


namespace Assignment1_Samarpan
{
    class Program
    {
        static void Main(string[] args)
        {
            //Question 1
            Console.WriteLine("Q1 : Enter the number of rows for the traingle:");
            int n = Convert.ToInt32(Console.ReadLine());
            printTriangle(n);
            Console.WriteLine();

            //Question 2:
            Console.WriteLine("Q2 : Enter the number of terms in the Pell Series:");
            int n2 = Convert.ToInt32(Console.ReadLine());
            printPellSeries(n2);
            Console.WriteLine();

            //Question 3:
            Console.WriteLine("Q3 : Enter the number to check if squareSums exist:");
            int n3 = Convert.ToInt32(Console.ReadLine());
            bool flag = squareSums(n3);
            if (flag)
            {
                Console.WriteLine("Yes, the number can be expressed as a sum of squares of 2 integers");
            }
            else
            {
                Console.WriteLine("No, the number cannot be expressed as a sum of squares of 2 integers");
            }
            Console.WriteLine();

            //Question 4:
            int[] arr = { 3, 1, 4, 1, 5 };
            Console.WriteLine("Q4: Enter the absolute difference to check");
            int k = Convert.ToInt32(Console.ReadLine());
            int n4 = diffPairs(arr, k);
            Console.WriteLine("There exists {0} pairs with the given difference", n4);
            Console.WriteLine();

            //Question 5:
            List<string> emails = new List<string>();
            emails.Add("dis.email + bull@usf.com");
            emails.Add("dis.e.mail+bob.cathy@usf.com");
            emails.Add("disemail+david@us.f.com");
            int ans5 = UniqueEmails(emails);
            Console.WriteLine("Q5");
            Console.WriteLine("The number of unique emails is " + ans5);
            Console.WriteLine();

            //Quesiton 6:
            string[,] paths = new string[,] { { "London", "New York" }, { "New York", "Tampa" },
                                        { "Delhi", "London" } };
            string destination = DestCity(paths);
            Console.WriteLine("Q6");
            Console.WriteLine("Destination city is " + destination);
            Console.WriteLine();
            Console.ReadLine();
        }

        /// <summary>
        /// Print a pattern with n rows given n as input
        /// n – number of rows for the pattern, integer (int)
        /// This method prints a triangle pattern.
        /// For example n = 5 will display the output as: 
        ///     *
        ///    ***
        ///   *****
        ///   *******
        ///  *********
        /// returns      : N/A
        /// return type  : void
        /// </summary>
        /// <param name="n"></param>
        private static void printTriangle(int n)
        {
            try
            {
                // outer loop
                for(int i = 1; i <= n; i++)
                {
                    // print space (for line i there will be n-i spaces)
                    for(int j = 1; j <= n - i; j++)
                    {
                        Console.Write(" ");
                    }
                    // print starts (for line i there will be 2*i-1 stars)
                    for(int j = 1; j <= 2 * i - 1; j++)
                    {
                        Console.Write("*");
                    }
                    // print the new line
                    Console.Write("\n");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// <para>
        /// In mathematics, the Pell numbers are an infinite sequence of integers.
        /// The sequence of Pell numbers starts with 0 and 1, and then each Pell number is the sum of twice of the previous Pell number and 
        /// the Pell number before that.:thus, 70 is the companion to 29, and 70 = 2 × 29 + 12 = 58 + 12. The first few terms of the sequence are :
        /// 0, 1, 2, 5, 12, 29, 70, 169, 408, 985, 2378, 5741, 13860,… 
        /// Write a method that prints first n numbers of the Pell series
        /// Returns : N/A
        /// Return type: void
        /// </para>
        /// </summary>
        /// <param name="n2"></param>
        private static void printPellSeries(int n2)
        {
            try
            {
                // first number
                int a = 0;

                // second number
                int b = 1;

                // print first and second number
                Console.Write(a + " " + b + " ");

                // print third number onwards
                for(int i = 3; i <= n2; i++)
                {
                    /* space optimization technique: instead of storing the whole
                     series in an array we can see that every time only the last two
                     members are required. Hence the most optimized approach would be 
                    to calculate a and then swap a and b. */
                    a = 2 * b + a;
                    Console.Write(a + " ");
                    int temp = a;
                    a = b;
                    b = temp;
                }
                Console.Write("\n");
            }
            catch (Exception)
            {
                throw;
            }

        }


        /// <summary>
        /// Given a non-negative integer c, decide whether there're two integers a and b such that a^2 + b^2 = c.
        /// For example:
        /// Input: C = 5 will return the output: true (1*1 + 2*2 = 5)
        /// Input: A = 3 will return the output : false
        /// Input: A = 4 will return the output: true
        /// Input: A = 1 will return the output : true
        /// Note: You cannot use inbuilt Math Class functions.
        /// </summary>
        /// <param name="n3"></param>
        /// <returns>True or False</returns>
        /// 


        /* approach:
            let a run from 0 to c
            calculate b = c - a^2 for each a
            check if a is a perfect square
            
            now we can optimize the last step. The square_root(c-a^2) will be in the interval (0,c-a^2).
            We can tweak our regular binary search approch to find whether the square root exists in log(c-a^2) time.
         */

        // This is the tweaked binary search function. Instead of checking whether target==mid, we are checking
        // whether target==mid^2
        public static bool bsearch(long low, long high, long t2)
        {
            if (low <= high)
            {
                long mid = (low + high) / 2;
                //the trick
                if (t2 == mid * mid)
                {
                    return true;
                }
                if (t2 > mid * mid)
                {
                    return bsearch(mid + 1, high, t2);
                }
                return bsearch(low, mid - 1, t2);
            }
            return false;
        }

        // This is the function returning the desired result
        private static bool squareSums(int c)
        {
            try
            {
                // run the loop until i^2 is greater than c 
                long i = 0;
                while (c >= i * i)
                {
                    long b = c - i * i;

                    // call the binary search function to determine whether b is a perfect square
                    if (bsearch(0, b, b))
                    {
                        return true;
                    }
                    // We have to manually increment the counter as it is a while loop.
                    i++;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Given an array of integers and an integer n, you need to find the number of unique
        /// n-diff pairs in the array.Here a n-diff pair is defined as an integer pair (i, j),
        /// where i and j are both numbers in the array and their absolute difference is n.
        /// Example 1:
        /// Input: [3, 1, 4, 1, 5], k = 2
        /// Output: 2
        /// Explanation: There are two 2-diff pairs in the array, (1, 3) and(3, 5).
        /// Although we have two 1s in the input, we should only return the number of unique   
        /// pairs.
        /// Example 2:
        /// Input:[1, 2, 3, 4, 5], k = 1
        /// Output: 4
        /// Explanation: There are four 1-diff pairs in the array, (1, 2), (2, 3), (3, 4) and
        /// (4, 5).
        /// Example 3:
        /// Input: [1, 3, 1, 5, 4], k = 0
        /// Output: 1
        /// Explanation: There is one 0-diff pair in the array, (1, 1).
        /// Note : The pairs(i, j) and(j, i) count as same.
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns>Number of pairs in the array with the given number as difference</returns>

        /* 
         * General Approach:
         * add one by one element to the List while simultaneously checking for duplication.
         * if we assume |a-b|=k then either a-k or a+k or both should contribute to the final count.
         * But this approach will not work for k=0
         * This needs to be considered as separate corner case, where every element having more than one 
         * occurence will contribute to the count 
         */
        private static int diffPairs(int[] nums, int k)
        {
            try
            {
                int count = 0;

                // corner case when there is only one or no element in the array
                if (nums.Length <= 1)
                {
                    return 0;
                }
                List<int> a = new List<int>();
                Array.Sort(nums);

                // corner case when k=0;
                if (k == 0)
                {
                    int nocc = 1;
                    for (int i = 1; i < nums.Length; i++)
                    {
                        if (nums[i - 1] != nums[i])
                        {
                            if (nocc > 1)
                            {
                                count += 1;
                            }
                            nocc = 1;
                        }
                        else
                        {
                            nocc++;
                        }
                    }
                    if (nocc > 1)
                    {
                        count += 1;
                    }
                    return count;
                }

                // regular case to detect determine count
                a.Add(nums[0]);
                for (int i = 1; i < nums.Length; i++)
                {
                    // checking for duplication while inserting
                    if (!a.Contains(nums[i]))
                    {
                        a.Add(nums[i]);
                    }
                    if (i > 0 && nums[i] == nums[i - 1])
                    {
                        continue;
                    }
                    // if b+k is there in the hashset
                    if (a.Contains(nums[i] + k))
                    {
                        count++;
                    }

                    // if b-k is there in the hashset 
                    if (a.Contains(nums[i] - k))
                    {
                        count++;
                    }
                }
                return count;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured: " + e.Message);
                throw;
            }
        }

        /* Approach:
            add one by one element to the hashset.
            if we assume |a-b|=k then either a-k or a+k or both should contribute to the final count.
            But this approach will not work for k=0
            This needs to be considered as separate corner case, where every element having more than one 
            occurence will contribute to the count
         */

        //private static int diffPairs(int[] nums, int k)
        //{
        //    try
        //    {
        //        int count = 0;

        //        // corner case when there is only one or no element in the array
        //        if (nums.Length <= 1)
        //        {
        //            return 0;
        //        }
        //        HashSet<int> a = new HashSet<int>();
        //        Array.Sort(nums);

        //        // corner case when k=0;
        //        if (k == 0)
        //        {
        //            int nocc = 1;
        //            for (int i = 1; i < nums.Length; i++)
        //            {
        //                if (nums[i - 1] != nums[i])
        //                {
        //                    if (nocc > 1)
        //                    {
        //                        count += 1;
        //                    }
        //                    nocc = 1;
        //                }
        //                else
        //                {
        //                    nocc++;
        //                }
        //            }
        //            if (nocc > 1)
        //            {
        //                count += 1;
        //            }
        //            return count;
        //        }

        //        // regular case to detect determine count
        //        a.Add(nums[0]);
        //        for (int i = 1; i < nums.Length; i++)
        //        {
        //            a.Add(nums[i]);
        //            if (i > 0 && nums[i] == nums[i - 1])
        //            {
        //                continue;
        //            }
        //            // if b+k is there in the hashset
        //            if (a.Contains(nums[i] + k))
        //            {
        //                count++;
        //            }

        //            // if b-k is there in the hashset 
        //            if (a.Contains(nums[i] - k))
        //            {
        //                count++;
        //            }
        //        }
        //        return count;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("An error occured: " + e.Message);
        //        throw;
        //    }
        //}

        /// <summary>
        /// An Email has two parts, local name and domain name. 
        /// Eg: rocky @usf.edu – local name : rocky, domain name : usf.edu
        /// Besides lowercase letters, these emails may contain '.'s or '+'s.
        /// If you add periods ('.') between some characters in the local name part of an email address, mail sent there will be forwarded to the same address without dots in the local name.
        /// For example, "bulls.z@usf.com" and "bullsz@leetcode.com" forward to the same email address.  (Note that this rule does not apply for domain names.)
        /// If you add a plus('+') in the local name, everything after the first plus sign will be ignored.This allows certain emails to be filtered, for example ro.cky+bulls @usf.com will be forwarded to rocky@email.com.  (Again, this rule does not apply for domain names.)
        /// It is possible to use both of these rules at the same time.
        /// Given a list of emails, we send one email to each address in the list.Return, how many different addresses actually receive mails?
        /// Eg:
        /// Input: ["dis.email+bull@usf.com","dis.e.mail+bob.cathy@usf.com","disemail+david@us.f.com"]
        /// Output: 2
        /// Explanation: "disemail@usf.com" and "disemail@us.f.com" actually receive mails
        /// 

        /* Ordinary approach:
         * Manipulate the string based on the formula specified and then add it to a list.
         * While adding check whether the element is already there is the list. 
         * If yes then do not add it to prevent duplication
         * return the length of the list
         */

        private static int UniqueEmails(List<string> emails)
        {
            try
            {
                List<string> r = new List<string>();
                foreach (string email in emails)
                {
                    // split in local name and domain name
                    string[] m = email.Split('@');

                    // In the local name section fetch the first substring before the first occurence of '+' 
                    string lnamedot = (m[0].Split('+'))[0];

                    // replace all the dots(.) of lnamedot with empty string
                    string lname = lnamedot.Replace(".", "");

                    // insert the element to the hashset. while inserting check for duplication.
                    string f = lname + "@" + m[1];
                    if (!r.Contains(f))
                    {
                        r.Add(f);
                    }
                }
                // the count of total number of elements must be returned as desired output
                return r.Count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }


        /* Efficient approach:
         * We will manipulate the string based on the formula specified in the problem and add it to the hashset
         * so that possible duplications can be eliminated.
         */

        //private static int UniqueEmails(List<string> emails)
        //{
        //    try
        //    {
        //        HashSet<string> r = new HashSet<string>();
        //        foreach (string email in emails)
        //        {
        //            // split in local name and domain name
        //            string[] m = email.Split('@');

        //            // In the local name section fetch the first substring before the first occurence of '+' 
        //            string lnamedot = (m[0].Split('+'))[0];

        //            // replace all the dots(.) of lnamedot with empty string
        //            string lname = lnamedot.Replace(".", "");

        //            // insert the element to the hashset. duplication problem is taken care of.
        //            r.Add(lname + '@' + m[1]);
        //        }
        //        // the count of total number of elements must be returned as desired output
        //        return r.Count;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw;
        //    }
        //}

        /// <summary>
        /// You are given the array paths, where paths[i] = [cityAi, cityBi] means there exists 
        /// a direct path going from cityAi to cityBi. Return the destination city, 
        /// that is, the city without any path outgoing to another city.
        /// It is guaranteed that the graph of paths forms a line without any loop, 
        /// therefore, there will be exactly one destination city.
        /// 
        /// Example 1:
        /// 
        /// Input: paths = [["London", "New York"], ["New York","Tampa"], ["Delhi","London"]]
        /// Output: "Tampa" 
        /// Explanation: Starting at "Delhi" city you will reach "Tampa" city which is the destination city.
        /// Your trip consist of: "Delhi" -> "London" -> "New York" -> "Tampa".
        /// 
        /// Example 2:
        /// 
        /// Input: paths = [["B","C"],["D","B"],["C","A"]]
        /// Output: "A"
        /// Explanation: All possible trips are: 
        /// "D" -> "B" -> "C" -> "A". 
        /// "B" -> "C" -> "A". 
        /// "C" -> "A". 
        /// "A". 
        /// Clearly the destination city is "A".
        /// 

        /* General approach
         * Create two array, one having all the source cities and another having all the detination cities
         * find the city which is there in destination but not in source
         */

        private static string DestCity(string[,] paths)
        {
            try
            {
                int r = paths.GetLength(0);
                string result = "";
                // create two array, one for all the source cities and another for all the destination cities
                string[] source = new string[r];
                string[] dest = new string[r];

                // populating source and destination arrays with appropriate values
                for(int i = 0; i < r; i++)
                {
                    source[i] = paths[i, 0];
                    dest[i] = paths[i, 1];
                }

                // find the city which is there in the destination array but not in the source array
                for (int i = 0; i < r; i++)
                {
                    if (!IsThereInSource(dest[i], source))
                    {
                        result = dest[i];
                        break;
                    }
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // simple linear search to look up if a particular city is there in the source array.
        private static bool IsThereInSource(string city, string[] source)
        {
            for(int i = 0; i < source.Length; i++)
            {
                if (source[i] == city)
                {
                    return true;
                }
            }
            return false;
        }

        /* Time Optimized Approach:
         * Build two hashset, one for source and another for destination
         * add the values in the hashset. 
         * duplication problem will be taken care of.
         * find the city which is there in destination but not in source.
         */

        //private static string DestCity(string[,] paths)
        //{
        //    try
        //    {
        //        HashSet<string> source = new HashSet<string>();
        //        HashSet<string> destination = new HashSet<string>();
        //        String result = "";
        //        int r = paths.GetLength(0);
        //        int c = paths.GetLength(1);

        //        // inserting the values in the appropriate hashset
        //        for(int i = 0; i < r; i++)
        //        {
        //            source.Add(paths[i, 0]);
        //            destination.Add(paths[i, 1]);
        //        }

        //        // finding the city which is there in destination but not in source.
        //        foreach (string city in destination)
        //        {
        //            if (!source.Contains(city))
        //            {
        //                result = city;
        //                break;
        //            }
        //        }

        //        // returning the result back to the caller
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
