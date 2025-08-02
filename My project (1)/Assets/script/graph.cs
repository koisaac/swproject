using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]

public class postion
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]

public class NodeSet
{
    public postion pos;
    public string node_name;
    public float man_min_value;
    public float man_max_value;

    public float economy_min_value;
    public float economy_max_value;
}

[System.Serializable]

public class EdgeSet
{
    public int node1_num;
    public int node2_num;
    public float distance;
}


[System.Serializable]
public class GraphSet
{
    public List<NodeSet> nodes;
    public List<EdgeSet> edges;
}

public struct graph_info
{
    public int node_num;
    public int edge_num;
}

public class graph : MonoBehaviour
{
    public GameObject node;
    public GameObject road;
    public GameObject grid;

    public float R;
    // Start is called before the first frame update

    public float[,] adj_road_matrix;
    public float[,] adj_grid_matrix;

    public List<node> nodes;

    public List<int> deactivate_nodes;
    public List<int> activate_nodes;


    
    public Vector3 ESSpoint;
    public int Essp;
    

    private GameObject make_edge_object(Vector3 position, Vector3 direction, Transform parent, EdgeSet e, GameObject instance)
    {
        float length = direction.magnitude;

        GameObject cylinder = Instantiate(instance, position, Quaternion.identity); // 원기둥 생성
        cylinder.transform.localScale = new Vector3(R, length / 2.0f, R); // 원기둥 스케일 조정 (Y축 방향으로 길게) Y축이 height 기준임
        cylinder.transform.up = direction.normalized;// 방향 맞추기
        cylinder.transform.SetParent(parent);
        cylinder.GetComponent<edge>().first_node = e.node1_num;
        cylinder.GetComponent<edge>().second_node = e.node2_num;

        return cylinder;
    }

    public void print_matrix(float[,] matrix)
    {
        string s = "";
        for (int a = 0; a < matrix.GetLength(0); a++)
        {
            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                s += matrix[a, j].ToString() + " ";
            }
            s += "\n";
        }
        Debug.Log(s);
    }

    public graph_info make_graph(GraphSet set)
    {
        Transform node_chi = transform.GetChild(0);
        foreach(Transform chil in node_chi)
        {
            Destroy(chil.gameObject);
        }
        Transform edge_chi = transform.GetChild(1);
        Transform road_chi = edge_chi.GetChild(0);
        foreach (Transform chil in road_chi)
        {
            Destroy(chil.gameObject);
        }
        Transform grid_chi = edge_chi.GetChild(1);
        foreach (Transform chil in grid_chi)
        {
            Destroy(chil.gameObject);
        }

        List<Vector3> node_pos = new List<Vector3>();
        List<NodeSet> set_node = set.nodes;

        graph_info info = new graph_info();
        info.node_num = set.nodes.Count;
        info.edge_num = set.edges.Count;



        adj_road_matrix = new float[set_node.Count, set_node.Count];
        adj_grid_matrix = new float[set_node.Count, set_node.Count];

        int p = 0;
        foreach (NodeSet n in set_node)
        {

            Vector3 pos = new Vector3(n.pos.x, n.pos.y, n.pos.z);
            GameObject temp = Instantiate(node, pos, Quaternion.identity);


            if (n.node_name == "ESS_point")
            {
                car.Instance.pos = p - 1;

                ESSpoint = pos;
            }


            temp.GetComponent<node>().node_name = n.node_name;
            temp.GetComponent<node>().man_min_value = n.man_min_value;
            temp.GetComponent<node>().man_max_value = n.man_max_value;
            temp.GetComponent<node>().economy_max_value = n.economy_max_value;
            temp.GetComponent<node>().economy_min_value = n.economy_min_value;
            temp.GetComponent<node>().init();
            temp.transform.SetParent(node_chi);

            nodes.Add(temp.GetComponent<node>());

            p++;
            node_pos.Add(pos);
        }



        foreach (EdgeSet n in set.edges)
        {
            float delta = 0.5f;
            Vector3 start = node_pos[n.node1_num];
            Vector3 end = node_pos[n.node2_num];

            Vector3 direction = end - start;
            Vector3 position = start + (direction) / 2.0f; // 중간 위치



            GameObject Rcylinder = make_edge_object(position - new Vector3(0, delta, 0), direction, road_chi, n, road);
            GameObject Gcylinder = make_edge_object(position + new Vector3(0, delta + 0.2f, 0), direction, grid_chi, n, grid);



            adj_road_matrix[n.node1_num, n.node2_num] = n.distance;
            adj_grid_matrix[n.node1_num, n.node2_num] = 1;
            adj_road_matrix[n.node2_num, n.node1_num] = n.distance; 
            adj_grid_matrix[n.node2_num, n.node1_num] = 1;
        }


     

        return info;
    }

    public void deactivate_node(List<int> nodes)
    {
        Transform node_chi = transform.GetChild(0);

        deactivate_nodes = nodes;

        foreach (int node in nodes) {
            Transform node_temp = node_chi.GetChild(node);
            node_temp.gameObject.GetComponent<node>().Deactivate();
            this.nodes[node].is_activate = false;
            activate_nodes.Remove(node);
        }
    }

    public void deactivate_road(List<int> roads)
    {
        Transform edge_chi = transform.GetChild(1);
        Transform road_chi = edge_chi.GetChild(0);

        string s = "road ";
        foreach (int edge in roads)
        {
            s += edge.ToString() + ", ";
            Transform edge_temp = road_chi.GetChild(edge);
            edge_temp.gameObject.GetComponent<edge>().setDeactivate();
            edge ed = edge_temp.gameObject.GetComponent<edge>();
            adj_road_matrix[ed.first_node, ed.second_node] = 0;
            adj_road_matrix[ed.second_node, ed.first_node] = 0;

        }
        Debug.Log(s);
        graph_manager graphManager = graph_manager.Instance;
    }

    public void deactivate_grid(List<int> girds)
    {
        Transform edge_chi = transform.GetChild(1);
        Transform grid_chi = edge_chi.GetChild(1);

        foreach (int edge in girds)
        {
            Transform edge_temp = grid_chi.GetChild(edge);
            edge_temp.gameObject.GetComponent<edge>().setDeactivate();
            edge ed = edge_temp.gameObject.GetComponent<edge>();

            adj_grid_matrix[ed.first_node, ed.second_node] = 0;
            adj_grid_matrix[ed.second_node, ed.first_node] = 0;
        }

    }

    private bool IsPathExists(int[,] adjMatrix, int start, int end)
    {
        int n = adjMatrix.GetLength(0);
        bool[] visited = new bool[n];
        Queue<int> queue = new Queue<int>();

        queue.Enqueue(start);
        visited[start] = true;

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            if (current == end)
                return true;

            for (int i = 0; i < n; i++)
            {
                // 0이 아닌 값이 있으면 연결된 것
                if (adjMatrix[current, i] != 0 && !visited[i])
                {
                    visited[i] = true;
                    queue.Enqueue(i);
                }
            }
        }

        return false;
    }

}
