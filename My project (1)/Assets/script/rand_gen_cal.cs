using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rand_gen_cal : MonoBehaviour, generate_calamity
{
    public int node_num;
    public int edge_num;
    // Start is called before the first frame update

    public int deact_node_num;
    public int deact_road_num;
    public int deact_grid_num;
    private List<int> PickRandomUnique(int n, int k)
    {
        if (k > n)
            throw new ArgumentException("k�� n���� Ŭ �� �����ϴ�.");

        // 0 ~ n-1 ���ڸ� ����Ʈ�� ��´�
        List<int> numbers = new List<int>();
        for (int i = 0; i < n; i++)
        {
            numbers.Add(i);
        }

        // Fisher-Yates Shuffle�� ����Ͽ� �տ��� k���� ��������
        System.Random rand = new System.Random();
        for (int i = 0; i < k; i++)
        {
            int j = rand.Next(i, n);  // i���� n-1 ����
            // swap numbers[i] <-> numbers[j]
            int temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
        }

        // �տ��� k���� ����� ��ȯ
        return numbers.GetRange(0, k);
    }
    public void generate(graph_info info)
    {
        node_num = info.node_num;
        edge_num = info.edge_num;
    }
    public List<int> Deactivate_nodes()
    {
        return PickRandomUnique(node_num,deact_node_num);
    }
    public List<int> Deactivate_roads()
    {
        return PickRandomUnique(edge_num, deact_road_num);
    }
    public List<int> Deactivate_grids()
    {
        return PickRandomUnique(edge_num, deact_grid_num);
    }
}
