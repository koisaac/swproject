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

public class graph : MonoBehaviour
{
    public GameObject node;
    public GameObject road;
    public GameObject grid;

    public float R;
    // Start is called before the first frame update

    public void make_graph(GraphSet set)
    {
        Transform node_chi = transform.GetChild(0);
        Transform edge_chi = transform.GetChild(1);
        Transform road_chi = edge_chi.GetChild(0);
        Transform grid_chi = edge_chi.GetChild(1);

        List<Vector3> node_pos = new List<Vector3>();
        List<NodeSet> set_node = set.nodes;
        foreach (NodeSet n in set_node)
        {
            Vector3 pos = new Vector3(n.pos.x, n.pos.y, n.pos.z);
            GameObject temp = Instantiate(node, pos, Quaternion.identity);
            Debug.Log(temp);
            temp.GetComponent<node>().node_name = n.node_name;
            temp.GetComponent<node>().man_min_value = n.man_min_value;
            temp.GetComponent<node>().man_max_value = n.man_max_value;
            temp.GetComponent<node>().economy_max_value = n.economy_max_value;
            temp.GetComponent<node>().economy_min_value = n.economy_min_value;

            temp.GetComponent<node>().init();
            temp.transform.SetParent(node_chi);


            node_pos.Add(pos);
        }

        foreach (EdgeSet n in set.edges)
        {
            float delta = 0.5f;
            Vector3 start = node_pos[n.node1_num];
            Vector3 end = node_pos[n.node2_num];

            Vector3 direction = end - start;
            float length = direction.magnitude;

            // 중간 위치
            Vector3 position = start + (direction) / 2.0f;

            // 원기둥 생성
            GameObject Rcylinder = Instantiate(road, position - new Vector3(0,delta,0), Quaternion.identity);

            // 원기둥 스케일 조정 (Y축 방향으로 길게)
            Rcylinder.transform.localScale = new Vector3(R, length / 2.0f, R); // Y축이 height 기준임

            // 방향 맞추기
            Rcylinder.transform.up = direction.normalized;

            Rcylinder.transform.SetParent(road_chi);

            Debug.Log(n.distance);

            GameObject Gcylinder = Instantiate(grid, position + new Vector3(0, delta, 0), Quaternion.identity);

            // 원기둥 스케일 조정 (Y축 방향으로 길게)
            Gcylinder.transform.localScale = new Vector3(R, length / 2.0f, R); // Y축이 height 기준임

            // 방향 맞추기
            Gcylinder.transform.up = direction.normalized;
            Gcylinder.transform.SetParent(grid_chi);

        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
