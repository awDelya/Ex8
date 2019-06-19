using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My_methods;

namespace DerevoLi
{
    class Program
    {
        private static Random rnd = new Random();
        private static List<int> fTop, sTop;
        private static void PrintTable(int kolTop)
        {
            Color.Print(" ---------------------------------\n", ConsoleColor.Green);
            Color.Print(" | Ребра  | Вершина 1 | Вершина 2|\n", ConsoleColor.Green);
            for (int i = 0; i < kolTop; i++)
            {
                Color.Print(" ---------------------------------\n", ConsoleColor.Cyan);
                Color.Print(" |   " + (i + 1) + "\t  |     " + fTop[i] + "     |     " + sTop[i] + "    |\n", ConsoleColor.Cyan);
            }
            Color.Print(" ---------------------------------\n", ConsoleColor.Cyan);

        }
        private static void DoTable(int kolTop)
        {
            switch(Text.HowAdd())
            {
                case 1:
                    Console.Clear();
                    PrintTable(kolTop);
                    for (int i = 0; i < kolTop; i++)
                    {
                        Color.Print("\n\n Введите 1 вершину у " + (i + 1) + " ребра: ");
                        fTop[i] = Number.Check(1, kolTop);
                        Console.Clear();
                        PrintTable(kolTop);
                        Color.Print("\n\n Введите 2 вершину у " + (i + 1) + " ребра: ");
                        sTop[i] = Number.Check(1, kolTop);
                        Console.Clear();
                        PrintTable(kolTop);
                    }
                    break;
                case 2:
                    Console.Clear();
                    for (int i = 0; i < kolTop; i++)
                    {
                        fTop[i] = rnd.Next(1, kolTop+1);
                        sTop[i] = rnd.Next(1, kolTop+1);
                    }
                    PrintTable(kolTop);
                    break;
            }
            
        }
        private static int[,] GetIncidenceMatrix(List<Tuple<int, int>> edges, bool oriented = false)
        {
            var maxEdgeNumber = edges.Select(t => Math.Max(t.Item1, t.Item2)).Max();
            var result = new int[maxEdgeNumber, edges.Count];

            for (int i = 0; i < edges.Count; i++)
            {
                var edge = edges[i];
                result[edge.Item1 - 1, i] = oriented ? -1 : 1;
                result[edge.Item2 - 1, i] = 1;
            }

            return result;
        }
        static void Main()
        {
            Color.Print("\n Введите колличество ребер (от 2 до 9): ", ConsoleColor.Yellow);
            int kolTop = Number.Check(2, 9);
            fTop = new List<int>();
            sTop = new List<int>();
            for(int i=0; i<kolTop; i++)
            {
                fTop.Add(0);
                sTop.Add(0);
            }
            DoTable(kolTop);            
            Text.GoBackMenu();
        }
    }
}
