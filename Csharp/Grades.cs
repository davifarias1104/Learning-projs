using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    //2
    internal class Program
    {
        static void Main(string[] args)
        {
            int Approved = 0, Reproved = 0;

            Console.WriteLine("Calculador de Grades: \n");

            Console.Write("Quantidade de alunos: ");
            int.TryParse(Console.ReadLine(), out int Students);
            if (Students <= 0)
            {
                while (Students <= 0)
                {
                    Console.Write("Valor Inválido: ");
                    int.TryParse(Console.ReadLine(), out Students);
                }
            }

            Console.Write("Quantidade de notas: ");
            int.TryParse(Console.ReadLine(), out int Amount);
            if (Amount <= 0)
            {
                Console.WriteLine(Amount);
                while (Amount <= 0)
                {
                    Console.Write("Valor Inválido: ");
                    int.TryParse(Console.ReadLine(), out Amount);
                }
            }
            
            string[] names = new string[Students];
            double[][] grades = new double[Students][];

            for (int i = 0; i < Students; i++)
            {
                Console.Write("\nNome do aluno: ");
                names[i] = Console.ReadLine();
                if (names[i] != "")
                {
                    for (int n = 0; n < Amount; n++)
                    {
                        grades[i] = new double[Amount];
                        Console.Write($"{n + 1}ª Nota do aluno: ");
                        if (!double.TryParse(Console.ReadLine(), out double grade) ^ grade < 0 ^ grade > 10)
                        {
                            Console.Write("Error");
                            i--;
                            break;
                        }
                        grades[i][n] = grade;
                    }
                    if (i >= Students - 1)
                    {
                        Console.WriteLine();
                        break;
                    }
                }
                else
                {
                    Console.Write("Error\n");
                    i--;
                }
            }
            for (int i = 0; i < Students; i++)
            {
                
            }
            Console.ReadKey();
        }
    }
}
