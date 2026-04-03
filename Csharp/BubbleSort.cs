
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Sorter
{
    public static void Main()
    {
        Console.Write("Enter the amount of values: ");
        int.TryParse(Console.ReadLine(), out int Amount);
        int[] ints = new int[Amount];
        Console.WriteLine();
        for (int i = 0; i < ints.Length; i++)
        {
            Console.Write($"Enter the {i+1}º value: ");
            int.TryParse(Console.ReadLine(), out ints[i]);
            Console.WriteLine();
        }
        Array.Sort(ints);
        Console.Write("Enter a value to search for: ");
        int.TryParse(Console.ReadLine(), out int value); // Get the value to search

        int result = BinarySearch.Search(ints, value); // Call BinarySearch with the sorted array

        if (result != -1)
            Console.Write("Value found at index: " + result);
        else
            Console.Write("Value not found.");
        Console.ReadKey();
    }
}
public class BinarySearch
{
    public static int Search(int[] sortedArray, int value)
    {
        int start = 0;
        int end = sortedArray.Length - 1;
        int middle;

        while (start <= end)
        {
            middle = (start + end) / 2;

            // Check if value is present at mid
            if (sortedArray[middle] == value)
            {
                return middle;
            }
            if (sortedArray[middle] < value)
            {
                start = middle + 1;
            }
            else
            {
                end = middle - 1;
            }
        }
        return -1;
    }
}

