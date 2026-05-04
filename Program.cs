using System;
using System.Collections.Generic;
using System.Linq;

namespace StatisticsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Input Data
            double[] data = { 115, 182, 191, 31, 196, 1099, 5, 172, 10, 179, 83, 21, 20, 21, 186, 177, 195, 193, 188, 199, 62, 109, 105, 183, 110 };

            // Sort data for median and percentiles
            double[] sortedData = (double[])data.Clone();
            Array.Sort(sortedData);
            int n = data.Length;

            // (i) Mean
            double mean = data.Average();

            // (ii) Mode
            var groups = data.GroupBy(v => v);
            int maxCount = groups.Max(g => g.Count());
            var modes = groups.Where(g => g.Count() == maxCount).Select(g => g.Key);

            // (iii) Median (P50)
            double median = CalculatePercentile(sortedData, 50);

            // (iv) Variance (Population Variance is used here as 'n given values')
            double variance = data.Select(v => Math.Pow(v - mean, 2)).Sum() / n;

            // (v) P20
            double p20 = CalculatePercentile(sortedData, 20);

            // (vi) P50
            double p50 = median;

            // (vii) Third Quartile (Q3 / P75)
            double q3 = CalculatePercentile(sortedData, 75);

            // (viii) Second Quartile (Q2 / P50)
            double q2 = median;

            // (ix) Third Quartile (Repeated in prompt)
            double q3_repeat = q3;

            // (x) Range
            double range = data.Max() - data.Min();

            // (xi) Interquartile Range (IQR = Q3 - Q1)
            double q1 = CalculatePercentile(sortedData, 25);
            double iqr = q3 - q1;

            // (xii) Standard Deviation
            double stdDev = Math.Sqrt(variance);

            // (xiii) Summation of Deviations (Sum of (x - mean))
            // Note: In statistics, the sum of deviations from the mean is always zero.
            double sumDeviations = data.Sum(v => v - mean);

            // Display Results
            Console.WriteLine("========================================");
            Console.WriteLine("       STATISTICS CALCULATOR REPORT      ");
            Console.WriteLine("========================================");
            Console.WriteLine($"Number of values (n): {n}");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"(i)    Mean:                      {mean:F4}");
            Console.WriteLine($"(ii)   Mode:                      {string.Join(", ", modes)} (Appears {maxCount} times)");
            Console.WriteLine($"(iii)  Median:                    {median:F4}");
            Console.WriteLine($"(iv)   Variance:                  {variance:F4}");
            Console.WriteLine($"(v)    P20:                       {p20:F4}");
            Console.WriteLine($"(vi)   P50:                       {p50:F4}");
            Console.WriteLine($"(vii)  Third Quartile (Q3):       {q3:F4}");
            Console.WriteLine($"(viii) Second Quartile (Q2):      {q2:F4}");
            Console.WriteLine($"(ix)   Third Quartile (Repeat):   {q3_repeat:F4}");
            Console.WriteLine($"(x)    Range:                     {range:F4}");
            Console.WriteLine($"(xi)   Interquartile Range (IQR): {iqr:F4}");
            Console.WriteLine($"(xii)  Standard Deviation:        {stdDev:F4}");
            Console.WriteLine($"(xiii) Summation of Deviations:   {sumDeviations:F4} (≈ 0)");
            Console.WriteLine("========================================");
        }

        /// <summary>
        /// Calculates the percentile of a sorted dataset using linear interpolation.
        /// </summary>
        static double CalculatePercentile(double[] sortedData, double percentile)
        {
            if (percentile < 0 || percentile > 100)
                throw new ArgumentException("Percentile must be between 0 and 100.");

            int n = sortedData.Length;
            if (n == 0) return 0;
            if (n == 1) return sortedData[0];

            // Method: (n - 1) * p
            double realIndex = (percentile / 100.0) * (n - 1);
            int index = (int)realIndex;
            double fraction = realIndex - index;

            if (index + 1 < n)
                return sortedData[index] + (fraction * (sortedData[index + 1] - sortedData[index]));
            else
                return sortedData[index];
        }
    }
}
