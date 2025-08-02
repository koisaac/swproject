using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graph_manager : MonoBehaviour
{
    public graph Graph;
    public GameObject graphSetSorce;

    public GraphSet graphSet;
    public graph_info info;
    private static graph_manager instance = null;

    public List<edge_info> edge_infos;

    // Start is called before the first frame update
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static graph_manager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void make()
    {
        graphSet = graphSetSorce.GetComponent<graph_set_sorce>().make_GraphSet();
        info = Graph.make_graph(graphSet);

    }
    public void Deactivate_node(List<int> nodes)
    {
        Graph.deactivate_node(nodes);
    }

    public void Deactivate_road(List<int> road)
    {
        Graph.deactivate_road(road);
    }

    public void Deactivate_grid(List<int> grid)
    {
        Graph.deactivate_grid(grid);
    }

    public Vector3 getESSpoint()
    {
        return Graph.ESSpoint;
    }

    public void print_road_mat()
    {
        Graph.print_matrix(Graph.adj_road_matrix);
    }
 
}
