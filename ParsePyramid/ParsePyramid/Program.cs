using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ParsePyramid
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    string input;
        //    int _maxRows = 0;
        //    int _maxColumns = 0;
        //    string start = string.Empty;
        //    Console.WriteLine("Input your text (type EXIT to terminate): ");

        //    do
        //    {
        //        input = Console.ReadLine();
        //        if (!string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input))
        //        {
        //            if (input.ToUpper() != "EXIT")
        //            {
        //                start = start + input + "\n";
        //            }
        //        }
        //        else
        //        {
        //            continue;
        //        }
        //    } while (input.ToUpper() != "EXIT");

        //    Pyramid pyr = new Pyramid();
        //    Console.WriteLine(pyr.ReadContent(start));
        //    Console.ReadKey();
        //}
        static void Main(string[] args)
        {
            try
            {
                int choices;
                do
                {
                    Console.WriteLine("Enter your choice number");
                    Console.WriteLine("1 Enter data");
                    Console.WriteLine("2 Clear Console");
                    Console.WriteLine("3 or any other key to exit");
                    var m = Console.ReadLine().ToString();
                    choices = (string.IsNullOrEmpty(m) || string.IsNullOrWhiteSpace(m)) ? 0 : Convert.ToInt32(m);

                    switch (choices)
                    {
                        case 1:
                            ReadFromConsole();
                            break;
                        case 2:
                            Console.Clear();
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                        case 0:
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        default:
                            Environment.Exit(0);
                            break;
                    }

                } while (choices == 1 || choices == 2 || choices == 3 || choices == 0);
            }
            catch (Exception)
            {
                Console.ReadKey();
                Environment.Exit(0);
            }            
        }

        public static void ReadFromConsole()
        {
            string start = string.Empty;
            string input;
            Console.WriteLine("Input your text (type END to terminate): ");
            do
            {
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input))
                {
                    if (input.ToUpper() != "END")
                    {
                        start = start + input + "\n";
                    }
                }
                else
                {
                    continue;
                }
            } while (input.ToUpper() != "END");
            Pyramid pyr = new Pyramid();
            if (!pyr.IsValidBinaryTree(start))
            {
                // In case of Invalid binary tree - do processing required 
            }
            else
            {
                // Processing only valid Binary tree
                string result = pyr.ProcessContent(start);
                Console.WriteLine(result);
            }
            Console.WriteLine();
        }
    }
}

