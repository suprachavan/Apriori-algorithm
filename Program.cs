using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Apriori
{
    public static class MainClass
    {
        public const double minSupport = 0.05;
        public const double minConfidence = 0.5;

        public static int InitializeMembers()
        {
            int entriesCount = 0;
            int rowCount = 0;
            var reader = new StreamReader(File.OpenRead(@"C:\Study\531\Project\groceries.csv"));
            var line = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                while (line != null)
                {
                    //string[] columns = line.Split(null);
                    string[] columns = line.Split(',');
                    //string[] columns = line.Split(null);
                    foreach (string s in columns)
                    {
                        if (string.IsNullOrEmpty(s))
                            continue;
                        entriesCount++;
                        //TempList1.Add(s);
                    }
                    line = reader.ReadLine();
                    rowCount++;
                    //Console.WriteLine("No of items for Transaction"+rowCount+" is :"+entriesCount);
                    entriesCount = 0;
                }
            }
            reader.Close();
            //Console.WriteLine("total number of transactions from the dataset: " + rowCount);
            return rowCount;
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            int RowCount = MainClass.InitializeMembers();
            List<KeyValuePair<double, string>> List1 = new List<KeyValuePair<double, string>>();
            Console.WriteLine("Generated sets of large itemsets:");
            Console.WriteLine();
            List<string> listA = GenerateFrequentItemset1(RowCount, MainClass.minSupport, MainClass.minConfidence, ref List1);
            List<KeyValuePair<double, string>> List2 = GenerateFrequentItemset2(listA, List1, RowCount, MainClass.minSupport, MainClass.minConfidence);
            GenerateFrequentItemset3(listA, List2, RowCount, MainClass.minSupport, MainClass.minConfidence);
            GenerateFrequentItemset4(listA, RowCount, MainClass.minSupport, MainClass.minConfidence);
            //CSV_Read();
            Console.ReadLine();
        }
        static List<string> GenerateFrequentItemset1(int rowCount, double minSupport, double minConfidence, ref List<KeyValuePair<double, string>> list1)
        {
            list1 = new List<KeyValuePair<double, string>>();
            var reader = new StreamReader(File.OpenRead(@"C:\Study\531\Project\groceries.csv"));
            var line = reader.ReadLine();
            int patternCount = 0;

            List<string> TempList1 = new List<string>();
            List<string> listB = new List<string>();
            while (!reader.EndOfStream)
            {
                while (line != null)
                {
                    //string[] columns = line.Split(null);
                    string[] columns = line.Split(',');
                    //string[] columns = line.Split(null);
                    foreach (string s in columns)
                    {
                        if (string.IsNullOrEmpty(s))
                            continue;
                        //entriesCount++;
                        TempList1.Add(s);
                    }
                    line = reader.ReadLine();
                    //rowCount++;
                    //Console.WriteLine("No of items for Transaction"+rowCount+" is :"+entriesCount);
                    //entriesCount = 0;

                }
                //foreach(string s in listA)
                //    Console.WriteLine(s);
            }
            reader.Close();
            //Console.WriteLine(rowCount);
            // Console.WriteLine(entriesCount);
            Console.WriteLine();
            var group1 = TempList1.GroupBy(i => i);
            //StreamWriter writer = new StreamWriter(@"C:\Study\531\Project\patterns.txt", true);

            foreach (var g in group1)
            {
                double support = (double)g.Count() / (double)rowCount; //caculate support of each item
                support = Math.Round(support, 3);
                //Console.WriteLine("The support of item "+g.Key+" is: "+support);
                if (support >= minSupport)
                {
                    patternCount++;
                    listB.Add(g.Key);
                    Console.WriteLine("support :" + support + " for frequent itemset : " + g.Key);
                    //writer.WriteLine("support :" + support + " for frequent itemset : " + g.Key);
                    list1.Add(new KeyValuePair<double, string>(support, g.Key));
                }

                //support = 0.0;

            }
            Console.WriteLine("Size of set of large itemsets L(1): "+patternCount);
            Console.WriteLine();
            patternCount = 0;
            //foreach (string s in listB)
            // Console.WriteLine("Frequnt itemset is:" + s);
            //string frequentItemset1 = string.Join(",", listB.ToArray());
            //Console.WriteLine("Frequent patterns of length 1 are : { "+ frequentItemset1+" }");
            Console.WriteLine();
            return listB;
        }
        static List<KeyValuePair<double, string>> GenerateFrequentItemset2(List<string> listB, List<KeyValuePair<double, string>> list1, int rowCount, double minSupport, double minConfidence)
        {
            //candidate generation of length 2
            int patternCount = 0;
            string st1, st2;
            //listA.Clear();
            List<string> listC = new List<string>();
            for (int i = 0; i < listB.Count; i++)
            {
                st1 = listB.ElementAt(i);
                for (int j = i + 1; j < listB.Count; j++)
                {
                    st2 = listB.ElementAt(j);
                    listC.Add(st1 + " " + st2);
                }
            }
            string candidateItemset2 = string.Join(",", listC.ToArray());
            //Console.WriteLine("Candidate patterns of length 2 are : { " + candidateItemset2 + " }");
            Console.WriteLine();
            //foreach (string s in listA)
            //    Console.WriteLine(s);
            //calculate frequent itemset 2
            //listB.Clear();
            var group2 = listC.GroupBy(i => i);

            /*26th October*/

            //var reader1 = new StreamReader(File.OpenRead(@"C:\Study\531\Project\practice.txt"));
            //var line1 = reader1.ReadLine();
            int f2Count = 0;
            //entriesCount = 0;
            //rowCount = 0;
            List<string> FrequentItemset2 = new List<string>();
            List<KeyValuePair<double, string>> list2 = new List<KeyValuePair<double, string>>();
            for (int i = 0; i < listB.Count; i++)
            {
                st1 = listB.ElementAt(i);
                for (int j = i + 1; j < listB.Count; j++)
                {
                    var reader1 = new StreamReader(File.OpenRead(@"C:\Study\531\Project\groceries.csv"));
                    //var reader1 = new StreamReader(File.OpenRead(@"C:\Study\531\Project\retail.dat"));
                    var line1 = reader1.ReadLine();
                    st2 = listB.ElementAt(j);
                    while (!reader1.EndOfStream)
                    {
                        while (line1 != null)
                        {
                            if (line1.Contains(st1) && line1.Contains(st2))
                            {
                                f2Count++;
                            }
                            line1 = reader1.ReadLine();
                            //rowCount++;
                        }
                        double support = (double)f2Count / (double)rowCount;
                        support = Math.Round(support, 3);
                        //Console.WriteLine("support of " + st1 + "," + st2 + "is : " + support);
                        if (support >= minSupport)
                        {
                            patternCount++;
                            FrequentItemset2.Add(st1 + " " + st2);
                            list2.Add(new KeyValuePair<double, string>(support, st1 + " " + st2));

                            foreach (KeyValuePair<double, string> kv in list1)
                            {
                                if (st1 == kv.Value)
                                {
                                    double confidence = support / kv.Key;
                                    if (confidence >= minConfidence)
                                    {
                                        Console.WriteLine("Confidence for itemset " + st1 + " ==> " + st2 + " is : " + confidence);
                                        Console.WriteLine("this rule is selected!!");
                                        Console.WriteLine();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Confidence for itemset " + st1 + " ==> " + st2 + " is : " + confidence);
                                        Console.WriteLine("this rule is rejected!!");
                                        Console.WriteLine();
                                    }
                                }
                            }
                        }

                    }
                    reader1.Close();
                    f2Count = 0;
                    //rowCount = 0;
                }
            }
            Console.WriteLine("Size of set of large itemsets L(2): "+patternCount);
            //foreach (string s in FrequentItemset2)
            //    Console.WriteLine("Frequnt itemset is:" + s);
            patternCount = 0;
            return list2;
        }
        static void GenerateFrequentItemset3(List<string> listB, List<KeyValuePair<double, string>> list2, int rowCount, double minSupport, double minConfidence)
        {
            //28th October - frequent patterns of length 3
            int f3Count = 0;
            int patternCount = 0;
            string st1, st2, st3;
            List<string> FrequentItemset3 = new List<string>();
            for (int i = 0; i < listB.Count; i++)
            {
                st1 = listB.ElementAt(i);
                for (int j = i + 1; j < listB.Count; j++)
                {
                    st2 = listB.ElementAt(j);
                    for (int k = j + 1; k < listB.Count; k++)
                    {
                        var reader1 = new StreamReader(File.OpenRead(@"C:\Study\531\Project\groceries.csv"));
                        //var reader1 = new StreamReader(File.OpenRead(@"C:\Study\531\Project\retail.dat"));
                        var line1 = reader1.ReadLine();
                        st3 = listB.ElementAt(k);
                        while (!reader1.EndOfStream)
                        {
                            while (line1 != null)
                            {
                                if (line1.Contains(st1) && line1.Contains(st2) && line1.Contains(st3))
                                {
                                    f3Count++;
                                }
                                line1 = reader1.ReadLine();
                                //rowCount++;
                            }
                            double support = (double)f3Count / (double)rowCount;
                            support = Math.Round(support, 3);
                            //Console.WriteLine("support of " + st1 + "," + st2 + "," + st3 + " is : " + support);
                            if (support >= minSupport)
                            {
                                FrequentItemset3.Add(st1 + " " + st2 + " " + st3);
                                patternCount++;
                                foreach (KeyValuePair<double, string> kv in list2)
                                {
                                    if (st1 + " " + st2 == kv.Value)
                                    {
                                        double confidence = support / kv.Key;
                                        //Console.WriteLine("Confidence for itemset " + st1 + " " + st2 + " " + st3 +" is : " + confidence);
                                        if (confidence >= minConfidence)
                                        {
                                            Console.WriteLine("Confidence for itemset " + st1 + " " + st2 + "==>" + st3 + " is : " + confidence);
                                            Console.WriteLine("this rule is selected!!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Confidence for itemset " + st1 + " " + st2 + "==>" + st3 + " is : " + confidence);
                                            Console.WriteLine("this rule is rejected!!");
                                        }
                                    }
                                    if (st1 + " " + st3 == kv.Value)
                                    {
                                        double confidence = support / kv.Key;
                                        //Console.WriteLine("Confidence for itemset " + st1 + " " + st2 + " " + st3 + " is : " + confidence);
                                        if (confidence >= minConfidence)
                                        {
                                            Console.WriteLine("Confidence for itemset " + st1 + " " + st3 + "==>" + st2 + " is : " + confidence);
                                            Console.WriteLine("this rule is selected!!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Confidence for itemset " + st1 + " " + st3 + "==>" + st2 + " is : " + confidence);
                                            Console.WriteLine("this rule is rejected!!");
                                        }
                                    }
                                    if (st2 + " " + st3 == kv.Value)
                                    {
                                        double confidence = support / kv.Key;
                                        //Console.WriteLine("Confidence for itemset " + st1 + " " + st2 + " " + st3 + " is : " + confidence);
                                        if (confidence >= minConfidence)
                                        {
                                            Console.WriteLine("Confidence for itemset " + st2 + " " + st3 + "==>" + st1 + " is : " + confidence);
                                            Console.WriteLine("this rule is selected!!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Confidence for itemset " + st2 + " " + st3 + "==>" + st1 + " is : " + confidence);
                                            Console.WriteLine("this rule is rejected!!");
                                        }
                                    }
                                }
                            }
                        }
                        reader1.Close();
                        f3Count = 0;
                        //rowCount = 0;
                    }
                }
            }
            //Console.WriteLine("Size of set of large itemsets L(3): "+patternCount);
            //foreach (string s in FrequentItemset3)
               // Console.WriteLine("Frequnt itemset is:" + s);

        }
        static void GenerateFrequentItemset4(List<string> listB, int rowCount, double minSupport, double minConfidence)
        {
            //28th October - frequent patterns of length 4
            int f4Count = 0;
            int patternCount = 0;
            string st1, st2, st3, st4;
            List<string> FrequentItemset4 = new List<string>();
            for (int i = 0; i < listB.Count; i++)
            {
                st1 = listB.ElementAt(i);
                for (int j = i + 1; j < listB.Count; j++)
                {
                    st2 = listB.ElementAt(j);
                    for (int k = j + 1; k < listB.Count; k++)
                    {
                        st3 = listB.ElementAt(k);
                        for (int l = k + 1; l < listB.Count; l++)
                        {
                            var reader1 = new StreamReader(File.OpenRead(@"C:\Study\531\Project\groceries.csv"));
                            //var reader1 = new StreamReader(File.OpenRead(@"C:\Study\531\Project\retail.dat"));
                            var line1 = reader1.ReadLine();
                            st4 = listB.ElementAt(l);
                            while (!reader1.EndOfStream)
                            {
                                while (line1 != null)
                                {
                                    if (line1.Contains(st1) && line1.Contains(st2) && line1.Contains(st3) && line1.Contains(st4))
                                    {
                                        f4Count++;
                                    }
                                    line1 = reader1.ReadLine();
                                    //rowCount++;
                                }
                                double support = (double)f4Count / (double)rowCount;
                                support = Math.Round(support, 3);
                                //Console.WriteLine("support of " + st1 + "," + st2 + "," + st3 + "," + st4 + " is : " + support);
                                if (support >= minSupport)
                                {
                                    FrequentItemset4.Add(st1 + " " + st2 + " " + st3 + " " + st4);
                                }
                            }
                            reader1.Close();
                            f4Count = 0;
                            //rowCount = 0;
                        }
                    }
                }
            }
            Console.WriteLine("Size of set of large itemsets L(4): "+patternCount);
            foreach (string s in FrequentItemset4)
            {
                Console.WriteLine("Frequnt itemset is:" + s);
            }
            patternCount = 0;
        }
    }
}