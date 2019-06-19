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
        private static int[,] matrix;
        private static int kol = 0;
        private static bool Repite = false, find = true;
        private static bool[] number;
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
                        fTop[i] = Number.Check(1, kolTop + 1);
                        Console.Clear();
                        PrintTable(kolTop);
                        Color.Print("\n\n Введите 2 вершину у " + (i + 1) + " ребра: ");
                        sTop[i] = Number.Check(1, kolTop + 1);
                        Console.Clear();
                        PrintTable(kolTop);
                    }
                    break;
                case 2:
                    Console.Clear();
                    for (int i = 0; i < kolTop; i++)
                    {
                        fTop[i] = rnd.Next(1, kolTop + 1);
                        sTop[i] = rnd.Next(1, kolTop + 1);
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
        private static void Matrix(int kolTop)
        {
            var edges = new List<Tuple<int, int>>();
            for (int i = 0; i < kolTop; i++)
            {
                edges.Add(new Tuple<int, int>(fTop[i], sTop[i]));
            }
            matrix = GetIncidenceMatrix(edges);
        }
        private static void CheckTable(int kolTop)
        {
            /********************************************************///Блок для find(проверка на наличие всех вершин)
            number = new bool[kolTop];
            for (int i = 1; i < kolTop + 1; i++)
            {
                if (fTop.Contains(i) || sTop.Contains(i))
                    number[i - 1] = true;
            }
            for (int i = 0; i < number.Length; i++)
            {
                find = find && number[i];
            }
            /********************************************************///Блок Repite(проверка повторюшек (+циклы?))
            for (int i = 0; i < fTop.Count; i++)
            {
                string last, now, rev;
                last = fTop[i] + " " + sTop[i];
                try
                {
                    now = fTop[i + 1] + " " + sTop[i + 1];
                    rev = fTop[i + 1] + " " + sTop[i + 1];
                    rev = new string(rev.ToCharArray().Reverse().ToArray());
                    if (last == now || last == rev)
                    {
                        Repite = true;
                        break;
                    }
                }
                catch (ArgumentOutOfRangeException) { }
            }
            /********************************************************///Проверка дерева на связность(Все ли вершины связанны ребрами)
            if (!Repite && find)
            {
                Matrix(kolTop);
                Queue<int> q = new Queue<int>();    //Это очередь, хранящая номера вершин
                Queue<int> q2 = new Queue<int>();
                for (int i = 0; i < kolTop + 1; i++)
                    for (int j = 0; j < kolTop; j++)
                        q2.Enqueue(matrix[i, j]);
                int u = kolTop;
                bool[] used = new bool[u + 1];  //массив отмечающий посещённые вершины
                int[][] g = new int[u + 1][];   //массив содержащий записи смежных вершин
                for (int i = 0; i < u + 1; i++)
                {
                    g[i] = new int[u];
                    for (int j = 0; j < u; j++)
                    {
                        g[i][j] = q2.Dequeue();
                    }
                }
                used[u] = true;     //массив, хранящий состояние вершины(посещали мы её или нет)
                q.Enqueue(u);
                while (q.Count != 0)
                {
                    u = q.Peek();
                    q.Dequeue();
                    kol++;
                    for (int i = 0; i < g.Length - 1; i++)
                    {
                        if (Convert.ToBoolean(g[u][i]))
                        {
                            if (!used[i])
                            {
                                used[i] = true;
                                q.Enqueue(i);
                            }
                        }
                    }
                }
            }
            /********************************************************/
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
            CheckTable(kolTop);
            if (kol == kolTop + 1)
                Color.Print("\n Дерево существует!\n", ConsoleColor.Green);
            else
                Color.Print("\n Дерево не существует\n", ConsoleColor.Red);
            Text.GoBackMenu();
        }
    }
}