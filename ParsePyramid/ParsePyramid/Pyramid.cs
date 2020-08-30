using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ParsePyramid
{
    public  class Pyramid: PathAndValues
    {
        public string inputString;
        public int _maxRows;
        public int _maxColumns;
        public int _maxSum;
        public static List<PathAndValues> AllPaths = new List<PathAndValues>();
        public static PathAndValues temp;
        public string ProcessContent(string input)
        {           
            var temp = input.Split('\n').Select(y => y).ToList();
            _maxRows = temp.ToList().Count();
            _maxColumns = temp[_maxRows - 2].Split(' ').Select(z => z).ToList().Count();            
            int[,] list = new int[_maxRows-1, _maxColumns];
            var charArray = input.Split('\n');
            bool InvalidEntryFlag = false;            
            try
            {
                for (int i = 0; i < charArray.Length; i++)
                {                   
                    var numArr = charArray[i].Trim().Split(' ');
                    var numbersList = numArr.ToList().Where(y => !string.Equals(y, ""));
                    numArr = numbersList.ToArray();
                    if (!InvalidEntryFlag)
                    {
                        for (int j = 0; j < numArr.Length; j++)
                        {
                            if (!string.IsNullOrWhiteSpace(numArr[j]) && !string.IsNullOrEmpty(numArr[j]))
                            {
                                int number = Convert.ToInt32(numArr[j]);
                                list[i, j] = number;
                                if (j > i)
                                {
                                    Console.WriteLine("Invalid Number of elements in row {0}", i);
                                    InvalidEntryFlag = true;
                                    break;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (!InvalidEntryFlag)
                {
                    AllPaths = new List<PathAndValues>();
                    var result = TraverseThroughOddEvenNodes(charArray, list, AllPaths, 0, 0, "", 0);

                    if (result.Count > 0)
                    {
                        int maxpath = result.Max(y => y.MaxVal);
                        result = result.Where(y => y.MaxVal == maxpath).ToList();
                        Console.WriteLine("\nPath(s) with maximum sum of the numbers:");
                        foreach (var t in result)
                        {
                            Console.WriteLine(t.PathString.TrimStart('-').TrimStart('>') + "   Value: " + t.MaxVal);
                        }

                        return "";
                    }
                    else
                    {
                        return "No odd Even Paths possible from root to bottom of the Pyramid";                        
                    }
                }
                else
                {
                    return "Invalid Pyramid\\Binary tree";
                }
            }
            catch (Exception)
            {                
                return "Invalid Pyramid\\Binary tree";
            }
        }


        // Calling the TraverseThroughOddEvenNodes in recursion to traverse through odd even nodes in left and right nodes
        public static List<PathAndValues> TraverseThroughOddEvenNodes(string[] arrayOfRowsByNewlines, int[,] tableHolder, List<PathAndValues> pathAndValues, int rows = 0, int columns = 0, string path = "", int maxSum = 0)
        {       
          
            int _maxLevels = arrayOfRowsByNewlines.Length - 2;
            int i = rows;
            int j = columns;
            maxSum = maxSum + tableHolder[i, j];
            path = path + "->" + tableHolder[i, j].ToString();
            if (rows < _maxLevels)
            {
                if ((tableHolder[i + 1, j] + tableHolder[i, j]) % 2 != 0) // parse left node incase of odd-even or vice versa pair of parent-node and chile-node
                {
                    TraverseThroughOddEvenNodes(arrayOfRowsByNewlines, tableHolder, pathAndValues, i + 1, j, path, maxSum);
                }
                if ((tableHolder[i + 1, j + 1] + tableHolder[i, j]) % 2 != 0) // parse right node incase of odd-even or vice versa pair of parent-node and chile-node
                {
                    TraverseThroughOddEvenNodes(arrayOfRowsByNewlines, tableHolder, pathAndValues, i + 1, j + 1, path, maxSum);
                }
            }
            else
            {
                temp = new PathAndValues
                {
                    MaxVal = maxSum,
                    PathString = path
                };
                AllPaths.Add(temp); // add path and values to the list
            }
            return AllPaths;
        }

        //Validate the Input
        public bool IsValidBinaryTree(string str)
        {
            Regex regex = new Regex("^-[0-9]+$");
            var check = str.Split('\n').ToList();
            int TotalRows = check.Count - 1;
            string errorMessage = "Invalid Pyramid\\BinaryTree \nSmallest Binary tree should have 2 level with 1 root and 2 element at last level";
            if (TotalRows == 1) // Pyramid\binary tree with only root element
            {
                Console.WriteLine(errorMessage);
                return false;
            }
            else if (TotalRows == 2 && check[1].Split(' ').ToList().Where(z => !string.Equals(z, ' ')).Count() < 2)// Pyramid\binary tree with only root element and one child
            {
                Console.WriteLine(errorMessage);
                return false;
            }
            else if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str) || str.ToLower().Equals("end"))// invalid spaces
            {
                Console.WriteLine(errorMessage);
                return false;
            }
            else
            {
                return true;
            }           
        }
    }

    public class PathAndValues
    {
        public string PathString;
        public int MaxVal;
    }
}
