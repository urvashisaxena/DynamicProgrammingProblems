/*
 * Author: Urvashi Saxena
 * Date: 02/15/2022
 * Description: C# Implementation for 0-1 Knapsack Problem 
 */


using System;

namespace DynamicProgramming
{
    class MainClass
    {
        #region Public Variables
        public static int[,] t;
        public static int[,] dp;
        #endregion

        #region Main
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello! We will do Knapsack!");

            #region Fields
            int[] wt = new int[] { 10, 20, 30 };
            int[] val = new int[] { 60,100,120 };
            int w = 50;
            int n = 3;
            #endregion

            #region Declarations
            //declaring a 2D array for the memoization & top down
            t = new int[n + 1, w + 1];
            dp = new int[n + 1, w + 1];
            #endregion

            #region Base Conditions (Memoization)
            //initializing the matrix, t to -1
            //Base Conditions
            for (int i = 0; i < n+1; i++)
            {
                for (int j = 0; j < w+1; j++)
                {
                    t[i,j] = -1;
                }
            }
            #endregion

            #region Function Calls
            int maxProfit = RecKnapsack(wt, val, w, n);

            Console.WriteLine("The Max Profit (recursive) = " + maxProfit);

            maxProfit = MemoKnapsack(wt, val, w, n);

            Console.WriteLine("The Max Profit (memoization) = " + maxProfit);

            maxProfit = TopDownKnapsack(wt, val, w, n);

            Console.WriteLine("The Max Profit (memoization) = " + maxProfit);
            #endregion

        }
        #endregion



        #region RecKnapsack
        /// <summary>
        /// Recursive Method for 0-1 Knapsack problem.
        /// This naive recursive solution has a complexity of exponential O(2^n).
        /// </summary>
        /// <param name="wt"> The weight values of the items</param>
        /// <param name="val">The values of the items</param>
        /// <param name="w">The capacity of the bag</param>
        /// <param name="n">The number of items</param>
        /// <returns>Returns the max profit</returns>
        public static int RecKnapsack (int[] wt, int[] val, int w, int n)
        { 
            //1. Base condition - think of the smallest valid input
            if (n == 0 || w == 0)
            {
                return 0;
            }
            //2. Choice diagram - if the wt[n-1] is less than or equal to weight of the bag
            if(wt[n-1] <= w) //it is a valid choice, we can pick the max value
            {
                return Math.Max((val[n - 1] + RecKnapsack(wt, val, w - wt[n - 1], n - 1)),
                    RecKnapsack(wt, val, w, n - 1));
            }
            else // it is not a valid choice
            {
                return RecKnapsack(wt, val, w, n - 1);
            }
        }
        #endregion

        #region MemoKnapsack
        /// <summary>
        /// Memoization for 0-1 Knapsack problem
        /// This solution has a complexity of linear O(n*w).
        /// </summary>
        /// <param name="wt"> The weight values of the items</param>
        /// <param name="val">The values of the items</param>
        /// <param name="w">The capacity of the bag</param>
        /// <param name="n">The number of items</param>
        /// <returns>Returns the max profit</returns>
        public static int MemoKnapsack (int[] wt, int[] val, int w, int n)
        {
            //1. Base Condition - think of the smallest valid input
            if (n == 0 || w == 0)
            {
                return t[n,w]=0;
            }
            //2. Choice Diagram - if the item's weight is less than capacity, then we have a choice to add
            if( wt[n-1] <= w)
            {
                return t[n, w] = Math.Max((val[n - 1] + MemoKnapsack(wt, val, w - wt[n - 1], n - 1)),
                    MemoKnapsack(wt, val, w, n - 1));
            }
            else
            {
                return t[n, w] = MemoKnapsack(wt, val, w, n - 1);
            }
        }
        #endregion

        #region TopDownKnapsack
        /// <summary>
        /// TopDown Implementation of 0-1 Knapsack Problem.
        /// This solution has a complexity of linear O(n*w).
        /// </summary>
        /// <param name="wt"> The weight values of the items</param>
        /// <param name="val">The values of the items</param>
        /// <param name="w">The capacity of the bag</param>
        /// <param name="n">The number of items</param>
        /// <returns>Returns the max profit</returns>
        public static int TopDownKnapsack (int[] wt, int[] val, int w, int n)
        {
            for (int i =0; i<n+1; i++)
            {
                for (int j = 0; j < w + 1; j++)
                {
                    //1. Base Condition - Initialization; for the first row and column, the values = 0
                    if (i == 0 || j==0)
                    {
                        dp[i, j] = 0;
                    }
                    //2. Choice Diagram - if the item's weight is less than the capacity, then we have a choice to add it
                    else if (wt[i - 1] <= j)
                    {
                        dp[i, j] = Math.Max((val[i - 1] + dp[i - 1, j - wt[i - 1]]), dp[i - 1, j]);
                    }
                    else
                        dp[i, j] = dp[i - 1, j];
                }
            }
            //return the max
            return dp[n, w];
            
        }
        #endregion
    }
}
