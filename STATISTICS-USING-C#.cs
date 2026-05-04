using System;


    class Program
    {
        static void Main(string[] args)
        {
            double[] data = {
                115, 182, 191, 31, 196, 1099, 5, 172, 10, 179,
                83, 21, 20, 21, 186, 177, 195, 193, 188, 199,
                62, 109, 105, 183, 110
            };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("============================================");
                Console.WriteLine("         Statistical Analysis System        ");
                Console.WriteLine("============================================");
                Console.WriteLine("1. Run Statistical Calculations");
                Console.WriteLine("2. Run Outlier Detection");
                Console.WriteLine("3. Exit");
                Console.Write("\nPlease select an option (1-3): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        RunStatistics(data);
                        Console.WriteLine("\nPress any key to return to the main menu...");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Clear();
                        RunOutliers(data);
                        Console.WriteLine("\nPress any key to return to the main menu...");
                        Console.ReadKey();
                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("Invalid option. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void RunStatistics(double[] data)
        {
            Console.WriteLine("--- Statistical Analysis ---\n");
            Console.WriteLine($"Total values: {data.Length}\n");
            Console.WriteLine($"(i) Mean: {CalculateMean(data):F4}");
            Console.WriteLine($"(ii) Mode: {CalculateMode(data)}");
            Console.WriteLine($"(iii) Median: {CalculateMedian(data):F4}");
            Console.WriteLine($"(iv) Variance: {CalculateVariance(data):F4}");
            Console.WriteLine($"(v) P20: {CalculatePercentile(data, 20):F4}");
            Console.WriteLine($"(vi) P50: {CalculatePercentile(data, 50):F4}");
            Console.WriteLine($"(vii) Third Quartile: {CalculatePercentile(data, 75):F4}");
            Console.WriteLine($"(viii) Second Quartile: {CalculatePercentile(data, 50):F4}");
            Console.WriteLine($"(ix) Third Quartile: {CalculatePercentile(data, 75):F4}");
            Console.WriteLine($"(x) Range: {CalculateRange(data):F4}");
            Console.WriteLine($"(xi) Interquartile Range: {CalculateIQR(data):F4}");
            Console.WriteLine($"(xii) Standard Deviation: {CalculateStandardDeviation(data):F4}");
            Console.WriteLine($"(xiii) Summation of Deviations: {CalculateSummationOfDeviations(data):F4}");
        }

        static void RunOutliers(double[] data)
        {
            Console.WriteLine("--- Outlier Analysis ---\n");
            double q1 = CalculatePercentile(data, 25);
            double q3 = CalculatePercentile(data, 75);
            double iqr = q3 - q1;

            double lowerBound = q1 - (1.5 * iqr);
            double upperBound = q3 + (1.5 * iqr);

            Console.WriteLine($"Q1: {q1:F4}");
            Console.WriteLine($"Q3: {q3:F4}");
            Console.WriteLine($"IQR: {iqr:F4}");
            Console.WriteLine($"Lower Bound: {lowerBound:F4}");
            Console.WriteLine($"Upper Bound: {upperBound:F4}\n");

            Console.WriteLine("--- Checking each value for outliers ---");
            for (int i = 0; i < data.Length; i++)
            {
                bool isOutlier = data[i] < lowerBound || data[i] > upperBound;
                string status = isOutlier ? "OUTLIER" : "Normal";
                Console.WriteLine($"Value: {data[i],5} | Status: {status}");
            }
        }

        static double Power(double baseValue, double exponent)
        {
            double result = 1;
            for (int i = 0; i < exponent; i++)
            {
                result *= baseValue;
            }
            return result;
        }

        static double CustomSqrt(double value)
        {
            if (value < 0) return double.NaN;
            if (value == 0) return 0;
            double root = value / 2;
            double temp = 0;
            while (root != temp)
            {
                temp = root;
                root = (value / temp + temp) / 2;
            }
            return root;
        }

        static double[] SortArray(double[] array)
        {
            double[] sorted = new double[array.Length];
            Array.Copy(array, sorted, array.Length);
            for (int i = 0; i < sorted.Length - 1; i++)
            {
                for (int j = 0; j < sorted.Length - 1 - i; j++)
                {
                    if (sorted[j] > sorted[j + 1])
                    {
                        double temp = sorted[j];
                        sorted[j] = sorted[j + 1];
                        sorted[j + 1] = temp;
                    }
                }
            }
            return sorted;
        }

        static double CalculateMean(double[] data)
        {
            double sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }
            return sum / data.Length;
        }

        static string CalculateMode(double[] data)
        {
            int maxCount = 0;
            string modeValues = "";
            for (int i = 0; i < data.Length; i++)
            {
                int count = 0;
                for (int j = 0; j < data.Length; j++)
                {
                    if (data[j] == data[i])
                    {
                        count++;
                    }
                }
                if (count > maxCount)
                {
                    maxCount = count;
                    modeValues = data[i].ToString();
                }
                else if (count == maxCount && !modeValues.Contains(data[i].ToString()))
                {
                    modeValues += ", " + data[i].ToString();
                }
            }
            if (maxCount == 1) return "No single mode";
            return modeValues;
        }

        static double CalculateMedian(double[] data)
        {
            double[] sorted = SortArray(data);
            int length = sorted.Length;
            if (length % 2 != 0) return sorted[length / 2];
            return (sorted[(length / 2) - 1] + sorted[length / 2]) / 2.0;
        }

        static double CalculateVariance(double[] data)
        {
            double mean = CalculateMean(data);
            double sumOfSquares = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sumOfSquares += Power(data[i] - mean, 2);
            }
            return sumOfSquares / (data.Length - 1);
        }

        static double CalculatePercentile(double[] data, double percentile)
        {
            double[] sorted = SortArray(data);
            double n = sorted.Length;
            double rank = (percentile / 100.0) * (n - 1) + 1;
            if (rank == 1) return sorted[0];
            if (rank == n) return sorted[(int)n - 1];
            int k = (int)rank;
            double d = rank - k;
            return sorted[k - 1] + d * (sorted[k] - sorted[k - 1]);
        }

        static double CalculateRange(double[] data)
        {
            double[] sorted = SortArray(data);
            return sorted[sorted.Length - 1] - sorted[0];
        }

        static double CalculateIQR(double[] data)
        {
            double q3 = CalculatePercentile(data, 75);
            double q1 = CalculatePercentile(data, 25);
            return q3 - q1;
        }

        static double CalculateStandardDeviation(double[] data)
        {
            double variance = CalculateVariance(data);
            return CustomSqrt(variance);
        }

        static double CalculateSummationOfDeviations(double[] data)
        {
            double mean = CalculateMean(data);
            double sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += (data[i] - mean);
            }
            return sum;
        }
    }
