/*
 * NAME: �������� ������ ���������
 * TITL: ���������� ���������� �������� ���� ����� ��������� �����
 * DATE: 24.03.2019
 */

using System;
using System.Collections.Generic;

namespace Kursach_1371
{
    class �Program
    {
        // ����� � �������
        public static double DFS(�Node node, �Node parent)
        {
            node.m_routes_out = 1;
            double result = 0;

            foreach (var i in node.m_branches)
            {
                var item_2 = i.Item2;
                if (item_2 == parent) continue;
                result += DFS(item_2, node);
                node.m_routes_out += item_2.m_routes_out;
            }

            foreach (var i in node.m_branches)
            {
                var item_1 = i.Item1;
                var item_2 = i.Item2;
                if (item_2 == parent) continue;
                node.m_routes_in += item_2.m_routes_in + item_1 * item_2.m_routes_out;
                result += (item_2.m_routes_in + item_1 * item_2.m_routes_out) * (node.m_routes_out - item_2.m_routes_out);
            }

            return result;
        }

        // ���� � ��������� ������� ��������; ���������� � ����� ��������� ������
        public static void Main()
        {
            int number_of_points = int.Parse(Console.ReadLine());
            �Node[] nodes = new �Node[50001];
            for (int i = 0; i < 50001; i++) nodes[i] = new �Node();

            for (int i = 0; i < number_of_points - 1; i++)
            {
                var entry = Console.ReadLine().Split(' ');
                int point_1 = int.Parse(entry[0]);
                int point_2 = int.Parse(entry[1]);
                int time = int.Parse(entry[2]);

                var tuple_1 = new Tuple<int, �Node>(time, nodes[point_2]);
                var tuple_2 = new Tuple<int, �Node>(time, nodes[point_1]);
                nodes[point_1].m_branches.Add(tuple_1);
                nodes[point_2].m_branches.Add(tuple_2);
            }

            double sum = DFS(nodes[1], nodes[0]);
            double answer = 2 * sum;
            answer /= number_of_points;
            answer /= (number_of_points - 1);
            Console.Write(answer);
        }
    }

    // ���� �� �������� �������� ���� � ������ �����
    public class �Node
    {
        public double m_routes_in;
        public double m_routes_out;
        public List<Tuple<int, �Node>> m_branches = new List<Tuple<int, �Node>>();
    }
}