using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_gen_cal : MonoBehaviour, generate_calamity
{
    public int node_num;
    public int edge_num;
    // Start is called before the first frame update
    public 
    void generate(graph_info info)
    {
        node_num = info.node_num;
        edge_num = info.edge_num;
    }
    public List<int> Deactivate_nodes()
    {
        return new List<int>() { 0, 1, 2 };
    }
    public List<int> Deactivate_roads()
    {
        return new List<int>() {  3,5,6 };
    }
    public List<int> Deactivate_grids()
    {
        return new List<int>() { 1, 0,4,7 };
    }
}
